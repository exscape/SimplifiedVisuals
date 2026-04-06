using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Nodes.HoverTips;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace SimplifiedVisuals.Patches;

// Patch the Big Slash effect (used by Perfected Strike, Sovereign Blade and the Mecha Knight)
[HarmonyPatch(typeof(NBigSlashVfx), nameof(NBigSlashVfx._Ready))]
public static class NBigSlashVfx__Ready_Patch
{
    public static bool Prefix() => Config.BigSlashEffect != EffectIntensity.Disabled;

    public static void Postfix(NBigSlashVfx __instance)
    {
        if (Config.BigSlashEffect != EffectIntensity.Reduced) return;
        __instance.ModifyChild<GpuParticles2D>("vfx_slash_core",
            p => p.Modulate = new Color(1f, 1f, 1f, 0.3f));
    }
}

// Also patch the impact VFX from Big Slash
[HarmonyPatch(typeof(NBigSlashImpactVfx), nameof(NBigSlashImpactVfx._Ready))]
public static class NBigSlashImpactVfx__Ready_Patch
{
    public static bool Prefix() => Config.BigSlashEffect != EffectIntensity.Disabled;

    public static void Postfix(NBigSlashImpactVfx __instance)
    {
        __instance.ModifyChild<GpuParticles2D>("impact_core_pivot/vfx_big_slash_impact_side_smoke_left",
            p => p.Preprocess = 0.06f);
        __instance.ModifyChild<GpuParticles2D>("impact_core_pivot/vfx_big_slash_impact_side_smoke_right",
            p => p.Preprocess = 0.06f);

        __instance.ModifyChild<CanvasItem>("impact_core_pivot/vfx_big_slash_impact_core",
            c => c.Modulate = new Color(1f, 1f, 1f, 0.3f));
        __instance.ModifyChild<CanvasItem>("vfx_common_hit_flare",
            c => c.Modulate = new Color(1f, 1f, 1f, 0.3f));
    }
}

// Hide the purple screen overlay when an enemy dies to Doom (but not the void hole that draws the enemy down)
[HarmonyPatch(typeof(NDoomOverlayVfx), "PlayVfx")]
public static class NDoomOverlayVfx__Patch
{
    public static bool Prefix(NDoomOverlayVfx __instance)
    {
        if (!Config.DisablePurpleDoomOverlay) return true;

        // We need to keep this, but remove everything else
        Traverse.Create(__instance)
            .Method("OnTweenFinished")
            .GetValue();

        return false;
    }
}

// Disables the radial blur effect used by several encounters:
// Bygone Effigy, Ceremonial Beast, Mecha Knight, Shrinker Beetle, Vantom
[HarmonyPatch(typeof(NRadialBlurVfx), nameof(NRadialBlurVfx.Activate))]
public static class NRadialBlurVfx_Activate_Patch
{
    public static bool Prefix(NRadialBlurVfx __instance) => !Config.DisableRadialBlurEffect;
}

// Used by Ceremonial Beast, Devoted Sculptor, Terror Eel, The Insatiable
[HarmonyPatch(typeof(NScreamVfx), nameof(NScreamVfx._Ready))]
public static class NScreamVfx__Ready_Patch
{
    public static bool Prefix(NScreamVfx __instance) => !Config.DisableScreamEffect;
}

// Used by Haunted Ship, Soul Fysh
[HarmonyPatch(typeof(NSpookyScreamVfx), nameof(NSpookyScreamVfx._Ready))]
public static class NSpookyScreamVfx__Ready_Patch
{
    public static bool Prefix(NSpookyScreamVfx __instance) => !Config.DisableSpookyScreamEffect;
}

// Hide the swords/daggers above the Regent during some attacks (like Strike)
[HarmonyPatch(typeof(NRegentVfx), "Attack")]
public static class NRegentVfx_Attack_Patch
{
    public static bool Prefix(NRegentVfx __instance) => !Config.DisableRegentAttackEffect;
}

// Remove/counteract the sovereign blade orbit+bobbing animations
[HarmonyPatch(typeof(NSovereignBladeVfx), nameof(NSovereignBladeVfx._Process))]
public static class NSovereignBladeVfx__Process_Patch
{
    public static bool Prefix(
        NSovereignBladeVfx __instance,
        double delta,
        Path2D? ____orbitPath,
        NHoverTipSet? ____hoverTip,
        MegaSprite? ____animController)
    {
        if (!Config.DisableSovereignBladeMovement) return true;

        // Disable the up/down bobbing
        var currentTrack = ____animController?.GetAnimationState().GetCurrent(0);
        if (currentTrack?.GetAnimation().GetName() == "idle_loop")
            currentTrack.SetTimeScale(0f);

        // Disable the orbit
        if (____orbitPath == null) return true;
        var bakedLength = ____orbitPath.Curve.GetBakedLength();

        if (____hoverTip == null)
            __instance.OrbitProgress -= 60.0 * delta / bakedLength;

        return true;
    }
}

[HarmonyPatch(typeof(NStarryImpactVfx), nameof(NStarryImpactVfx._Ready))]
public static class MuteStarryImpactPatch
{
    public static bool Prefix(NStarryImpactVfx __instance)
    {
        if (Config.StarryImpactEffect != EffectIntensity.Disabled) return true;
        __instance.Visible = false;
        __instance.QueueFreeSafely();
        return false;

    }
    public static void Postfix(Node2D __instance)
    {
        if (Config.StarryImpactEffect == EffectIntensity.Default) return;
        var coreVisual = __instance.GetNodeOrNull<CanvasItem>("vfx_starry_impact_core");
        if (coreVisual != null)
            coreVisual.SelfModulate = new Color(1.0f, 1.0f, 1.0f, 0.2f);

        var ring = __instance.GetNodeOrNull<CanvasItem>("vfx_common_ring_polar_b");
        if (ring == null) return;
        ring.SelfModulate = new Color(1.0f, 1.0f, 1.0f, 0.4f);
    }
}