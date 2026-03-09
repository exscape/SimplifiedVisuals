using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace SimplifiedAnimations.Patches;

// Hide the Big Slash effect (used by Perfected Strike, Sovereign Blade and the Mecha Knight)
[HarmonyPatch(typeof(NBigSlashVfx), nameof(NBigSlashVfx._Ready))]
public static class NBigSlashVfx__Ready_Patch
{
    public static bool Prefix(NBigSlashVfx __instance)
    {
        return !ModSettings.DisableBigSlashEffect;
    }
}

// Also hide the impact VFX from Big Slash
[HarmonyPatch(typeof(NBigSlashImpactVfx), nameof(NBigSlashImpactVfx._Ready))]
public static class NBigSlashImpactVfx__Ready_Patch
{
    public static bool Prefix(NBigSlashImpactVfx __instance)
    {
        return !ModSettings.DisableBigSlashEffect;
    }

}

// Hide the purple screen overlay when an enemy dies to Doom (but not the void hole that draws the enemy down)
[HarmonyPatch(typeof(NDoomOverlayVfx), "PlayVfx")]
public static class NDoomOverlayVfx__Patch
{
    public static bool Prefix(NDoomOverlayVfx __instance)
    {
        if (!ModSettings.DisablePurpleDoomOverlay) return true;

        // We need to keep this, but remove everything else
        Traverse.Create(__instance)
            .Method("OnTweenFinished")
            .GetValue();

        return false;
    }
}