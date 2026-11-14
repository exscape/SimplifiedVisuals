using System.Reflection;
using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.Nodes.GodotExtensions;
using MegaCrit.Sts2.Core.Nodes.Screens.RunHistoryScreen;
using MegaCrit.Sts2.Core.Nodes.Screens.TreasureRoomRelic;

namespace SimplifiedVisuals.Patches;

[HarmonyPatch(typeof(NCard), nameof(NCard.ActivateRewardScreenGlow))]
public static class NCard_ActivateRewardScreenGlow_Patch
{
    public static bool Prefix(ref GpuParticles2D? ____sparkles)
    {
        if (!Config.DisableRareCardGlow) return true;

        if (____sparkles != null)
            ____sparkles.Visible = false;

        return false;
    }
}

[HarmonyPatch(typeof(NTreasureRoomRelicHolder), "CreateGlowVfx")]
public static class NTreasureRoomRelicHolder_CreateGlowVfx_Patch
{
    public static bool Prefix()
    {
        return !Config.DisableRareRelicGlow;
    }
}

[HarmonyPatch]
public static class RunScreenTransitionPatch
{
    public static IEnumerable<MethodBase> TargetMethods()
    {
        yield return AccessTools.Method(typeof(NRunHistory), "OnLeftButtonButtonReleased");
        yield return AccessTools.Method(typeof(NRunHistory), "OnRightButtonButtonReleased");
    }

    public static void Postfix(Tween ____screenTween, NScrollableContainer ____screenContents)
    {
        if (!Config.DisableRunHistoryScreenTransition) return;

        ____screenTween.Kill();
        ____screenContents.Position = Vector2.Zero;
        ____screenContents.Modulate = ____screenContents.Modulate with { A = 1f };
    }
}

// Temp bug fix/hack: disable scrolling in the run history screen, since it scrolls when it shouldn't
[HarmonyPatch(typeof(NScrollableContainer), "UpdateScrollLimitBottom")]
public static class NScrollableContainer_Bugfix_Patch
{
    public static void Postfix(NScrollableContainer __instance)
    {
        Node current = __instance;

        while (current != null)
        {
            if (current is NRunHistory)
            {
                __instance.Scrollbar.Visible = false;
                __instance.InstantlyScrollToTop();
                return;
            }
            current = current.GetParent();
        }
    }
}