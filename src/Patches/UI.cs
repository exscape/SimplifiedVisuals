using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;

namespace SimplifiedAnimations.Patches;

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