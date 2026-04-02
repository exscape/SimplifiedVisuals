using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;

namespace SimplifiedVisuals.Patches;

// Disables the goldish glow around rare cards -- also see the patch below
[HarmonyPatch(typeof(NCardRareGlow), nameof(NCardRareGlow.Create))]
public static class NCardRareGlow_Create_Patch
{
    public static bool Prefix(ref NCardRareGlow? __result)
    {
        if (!Config.DisableRareCardGlow) return true;
        __result = null;
        return false;
    }
}

[HarmonyPatch(typeof(NCard), nameof(NCard.ActivateRewardScreenGlow))]
public static class NCard_ActivateRewardScreenGlow_Patch
{
    public static void Postfix(ref GpuParticles2D? ____sparkles)
    {
        if (!Config.DisableRareCardGlow) return;

        if (____sparkles != null)
            ____sparkles.Visible = false;
    }
}

// Disables the blueish glow around uncommon cards
[HarmonyPatch(typeof(NCardUncommonGlow), nameof(NCardUncommonGlow.Create))]
public static class NCardUncommonGlow_Create_Patch
{
    public static bool Prefix(ref NCardUncommonGlow? __result)
    {
        if (!Config.DisableRareCardGlow) return true;

        __result = null;
        return false;
    }
}