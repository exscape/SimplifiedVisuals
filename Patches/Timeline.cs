using System.Reflection;
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
        if (!Config.FreezeBackgroundStars) return;

        var starsBg = __instance.GetNodeOrNull<GpuParticles2D>("StarsBg");
        var starsFg = __instance.GetNodeOrNull<GpuParticles2D>("StarsFg");

        if (starsBg != null) starsBg.SpeedScale = 0f;
        if (starsFg != null) starsFg.SpeedScale = 0f;
    }
}

// Remove the "expanding shockwave" effect on unlocked epochs; the border still pulsates
[HarmonyPatch(typeof(NEpochHighlightVfx), nameof(NEpochHighlightVfx.Create))]
public static class MuteEpochHighlightPatch
{
    public static void Postfix(ref NEpochHighlightVfx __result)
    {
        if (!Config.DisableUnlockShockwaves) return;
        __result.Visible = false;
    }
}

[HarmonyPatch(typeof(NEpochOffscreenVfx), nameof(NEpochOffscreenVfx.Create))]
public static class ReplaceOffscreenVfxPatch
{
    public static void Postfix(ref NEpochOffscreenVfx __result)
    {
        if (!Config.DisableUnlockShockwaves) return;

        // Hide the original effect
        __result.SelfModulate = Colors.Transparent;

        // Hide all original child nodes
        foreach (var child in __result.GetChildren())
        {
            if (child is CanvasItem canvasItem)
            {
                canvasItem.Visible = false;
            }
        }

        // Add an arrow to show the user that there are offscreen epochs.
        // Note that since this is attached to the parent VFX node, this handles the case of simultaneous left AND right
        // arrows without issue; multiple are created here, and multiple are updated below in the _Process hook.
        // If there are multiple offscreen Epochs on the same side, we'll draw multiple arrows, but in the exact same
        // location, so that won't be visible. A bit wasteful, but fine.
        var offscreenArrow = new Sprite2D
        {
            Texture = PreloadManager.Cache.GetTexture2D("res://images/ui/combat/targeting_arrow_head.png"),
            Name = "OffscreenEpochArrow",
            Modulate = new Color("EE82EE"),
            Visible = false, // Updated below in _Process
            TopLevel = true
        };

        __result.AddChild(offscreenArrow);
    }
}

// Update the location and rotation of the arrow(s) we just created above.
[HarmonyPatch(typeof(NEpochOffscreenVfx), nameof(NEpochOffscreenVfx._Process))]
public static class UpdateOffscreenArrowPatch
{
    public static void Postfix(NEpochOffscreenVfx __instance, bool ____showVfx)
    {
        if (!Config.DisableUnlockShockwaves) return;

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
        offscreenArrow.Visible = ____showVfx;
    }
}

[HarmonyPatch]
public static class ConfettiKillerPatch
{
    public static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(NUnlockCardsScreen), nameof(NUnlockCardsScreen.Open));
        yield return AccessTools.Method(typeof(NUnlockCharacterScreen), nameof(NUnlockCharacterScreen.Open));
        yield return AccessTools.Method(typeof(NUnlockMiscScreen), nameof(NUnlockMiscScreen.Open));
        yield return AccessTools.Method(typeof(NUnlockPotionsScreen), nameof(NUnlockPotionsScreen.Open));
        yield return AccessTools.Method(typeof(NUnlockRelicsScreen), nameof(NUnlockRelicsScreen.Open));
    }

    public static void Prefix(Node __instance)
    {
        if (!Config.HideConfetti) return;

        var confetti = __instance.GetNodeOrNull<GpuParticles2D>("GPUParticles2D");
        if (confetti == null) return;

        confetti.ProcessMode = Node.ProcessModeEnum.Disabled;
        confetti.Visible = false;
        confetti.Emitting = false;
    }
}