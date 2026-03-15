using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Assets;
using MegaCrit.Sts2.Core.Nodes.Screens.Timeline;
using MegaCrit.Sts2.Core.Nodes.Screens.Timeline.UnlockScreens;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace SimplifiedVisuals.Patches;

// Freeze the animated stars in the background (and apparently foreground) of the timeline
[HarmonyPatch(typeof(NTimelineScreen), nameof(NTimelineScreen._Ready))]
public static class TimelineFreezeStarsPatch
{
    public static void Postfix(NTimelineScreen __instance)
    {
        if (!ModSettings.FreezeBackgroundStars) return;

        var starsBg = __instance.GetNodeOrNull<GpuParticles2D>("StarsBg");
        var starsFg = __instance.GetNodeOrNull<GpuParticles2D>("StarsFg");
        if (starsBg != null)
        {
            starsBg.SpeedScale = 0f;
        }

        if (starsFg != null)
        {
            starsFg.SpeedScale = 0f;
        }
    }
}

// Remove the "expanding shockwave" effect on unlocked epochs; the border still pulsates
[HarmonyPatch(typeof(NEpochHighlightVfx), nameof(NEpochHighlightVfx.Create))]
public static class MuteEpochHighlightPatch
{
    public static void Postfix(ref NEpochHighlightVfx __result)
    {
        if (!ModSettings.DisableUnlockShockwaves) return;
        __result.Visible = false;
    }
}

[HarmonyPatch(typeof(NEpochOffscreenVfx), nameof(NEpochOffscreenVfx.Create))]
public static class ReplaceOffscreenVfxPatch
{
    public static void Postfix(ref NEpochOffscreenVfx __result)
    {
        if (!ModSettings.DisableUnlockShockwaves) return;

        // Hide the original effect
        __result.SelfModulate = Colors.Transparent;

        // Hide all original child nodes (if any)
        foreach (var child in __result.GetChildren())
        {
            if (child is CanvasItem canvasItem)
            {
                canvasItem.Visible = false;
            }
        }

        // Add an arrow to show the user that there are offscreen epochs.
        //
        // Note that since this is attached to the parent VFX node, this handles the case of simultaneous left AND right
        // arrows without issue; multiple are created here, and multiple are updated below in the _Process hook.
        // If there are multiple offscreen Epochs on the same side, we'll draw multiple arrows, but in the exact same
        // location, so that won't be visible. A bit wasteful, but fine.
        var offscreenArrow = new Sprite2D();
        offscreenArrow.Texture = PreloadManager.Cache.GetTexture2D("res://images/ui/combat/targeting_arrow_head.png");
        offscreenArrow.Name = "OffscreenEpochArrow";
        offscreenArrow.Modulate = new Color("EE82EE");

        offscreenArrow.TopLevel = true;
        __result.AddChild(offscreenArrow);
    }

    // Update the location and rotation of the arrow(s) we just created above
    [HarmonyPatch(typeof(NEpochOffscreenVfx), nameof(NEpochOffscreenVfx._Process))]
    public static class UpdateOffscreenArrowPatch
    {
        public static void Postfix(NEpochOffscreenVfx __instance)
        {
            if (!ModSettings.DisableUnlockShockwaves) return;

            var offscreenArrow = __instance.GetNodeOrNull<Sprite2D>("OffscreenEpochArrow");
            if (offscreenArrow?.Texture == null) return;

            var (screenWidth, screenHeight) = __instance.GetViewportRect().Size;

            var halfWidth = offscreenArrow.Texture.GetWidth() / 2f;
            const float margin = 20f;

            // Position on the actual screen (vs local or global coordinates)
            var screenPos = __instance.GetGlobalTransformWithCanvas().Origin;

            // We don't center on the Y axis to avoid blocking Epochs when they are partially visible
            float xPosition;
            float yPosition = screenHeight / 3f;

            if (screenPos.X < screenWidth / 2f)
            {
                // Left side
                xPosition = halfWidth + margin;
                offscreenArrow.RotationDegrees = -90f;
            }
            else
            {
                // Right side
                xPosition = screenWidth - halfWidth - margin;
                offscreenArrow.RotationDegrees = 90f;
            }

            offscreenArrow.GlobalPosition = new Vector2(xPosition, yPosition);
            var traverse = Traverse.Create(__instance);
            offscreenArrow.Visible = traverse.Field("_showVfx").GetValue<bool>();
        }
    }
}

// Using multiple [HarmonyPatch] on a single class does not work (issues with the type?), so we do this instead.
public static class ConfettiKiller
{
    public static void Mute(Node screen)
    {
        if (!ModSettings.HideConfetti) return;

        var confetti = screen.GetNodeOrNull<GpuParticles2D>("GPUParticles2D");
        if (confetti == null) return;

        confetti.ProcessMode = Node.ProcessModeEnum.Disabled;
        confetti.Visible = false;
        confetti.Emitting = false;
    }
}

[HarmonyPatch(typeof(NUnlockCardsScreen), nameof(NUnlockCardsScreen.Open))]
public static class Patch_CardsScreen
{
    public static void Prefix(NUnlockCardsScreen __instance) => ConfettiKiller.Mute(__instance);
}

[HarmonyPatch(typeof(NUnlockCharacterScreen), nameof(NUnlockCharacterScreen.Open))]
public static class Patch_CharacterScreen
{
    public static void Prefix(NUnlockCharacterScreen __instance) => ConfettiKiller.Mute(__instance);
}

[HarmonyPatch(typeof(NUnlockMiscScreen), nameof(NUnlockMiscScreen.Open))]
public static class Patch_MiscScreen
{
    public static void Prefix(NUnlockMiscScreen __instance) => ConfettiKiller.Mute(__instance);
}

[HarmonyPatch(typeof(NUnlockPotionsScreen), nameof(NUnlockPotionsScreen.Open))]
public static class Patch_PotionsScreen
{
    public static void Prefix(NUnlockPotionsScreen __instance) => ConfettiKiller.Mute(__instance);
}

[HarmonyPatch(typeof(NUnlockRelicsScreen), nameof(NUnlockRelicsScreen.Open))]
public static class Patch_RelicsScreen
{
    public static void Prefix(NUnlockRelicsScreen __instance) => ConfettiKiller.Mute(__instance);
}