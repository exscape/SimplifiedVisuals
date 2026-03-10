using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.Vfx.Cards;

namespace SimplifiedAnimations.Patches;

// Disables the goldish glow around rare cards -- also see the patch below
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

[HarmonyPatch(typeof(NCard), nameof(NCard.ActivateRewardScreenGlow))]
public static class NCard_ActivateRewardScreenGlow_Patch
{
    public static void Postfix(NCard __instance)
    {
        if (!ModSettings.DisableRareCardGlow) return;

        try
        {
            var sparkles = Traverse.Create(__instance).Field("_sparkles").GetValue<GpuParticles2D>();
            sparkles.Visible = false;
        }
        catch (Exception e)
        {
            Console.WriteLine("[SimplifiedAnimations] Failed to remove sparkles from rare card reward");
        }
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