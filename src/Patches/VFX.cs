using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;

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

// Disables the goldish glow around rare cards
[HarmonyPatch(typeof(NCardRareGlow), nameof(NCardRareGlow.Create))]
public static class NCardRareGlow_Create_Patch
{
    public static bool Prefix(ref NCardRareGlow? __result)
    {
        if (!ModSettings.DisableRareCardGlow) return true;
        __result = null;
        return false;
    }
}

// Disables the blueish glow around uncommon cards
[HarmonyPatch(typeof(NCardUncommonGlow), nameof(NCardUncommonGlow.Create))]
public static class NCardUncommonGlow_Create_Patch
{
    public static bool Prefix(ref NCardUncommonGlow? __result)
    {
        if (!ModSettings.DisableUncommonCardGlow) return true;
        __result = null;
        return false;
    }
}

// Disables the radial blur effect used by several encounters:
// Bygone Effigy, Ceremonial Beast, Mecha Knight, Shrinker Beetle, Vantom
[HarmonyPatch(typeof(NRadialBlurVfx), nameof(NRadialBlurVfx.Activate))]
public static class NRadialBlurVfx_Activate_Patch
{
    public static bool Prefix(NRadialBlurVfx __instance)
    {
        return !ModSettings.DisableRadialBlurEffect;
    }
}

// Used by Ceremonial Beast, Devoted Sculptor, Terror Eel, The Insatiable
[HarmonyPatch(typeof(NScreamVfx), nameof(NScreamVfx._Ready))]
public static class NScreamVfx__Ready_Patch
{
    public static bool Prefix(NScreamVfx __instance)
    {
        return !ModSettings.DisableScreamEffect;
    }
}

// Used by Haunted Ship, Soul Fysh
[HarmonyPatch(typeof(NSpookyScreamVfx), nameof(NSpookyScreamVfx._Ready))]
public static class NSpookyScreamVfx__Ready_Patch
{
    public static bool Prefix(NSpookyScreamVfx __instance)
    {
        return !ModSettings.DisableSpookyScreamEffect;
    }
}