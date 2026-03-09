using HarmonyLib;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Saves;
using MegaCrit.Sts2.Core.Settings;

namespace SimplifiedAnimations.Patches;

// Reduce time spent between card draws; one card is drawn, then this delay is applied, then another is drawn, etc.
// Cards will essentially be drawn and move together.
[HarmonyPatch(typeof(CardPileCmd), "AppendPileLerpTween")]
public static class CardPileTween_Patch
{
    private static FastModeType previousValue = FastModeType.Fast;

    public static void Prefix()
    {
        if (!ModSettings.QuickerDraw) return;
        previousValue = SaveManager.Instance.PrefsSave.FastMode;
        SaveManager.Instance.PrefsSave.FastMode = FastModeType.Instant;
    }

    public static void Postfix()
    {
        if (!ModSettings.QuickerDraw) return;
        SaveManager.Instance.PrefsSave.FastMode = previousValue;
    }
}