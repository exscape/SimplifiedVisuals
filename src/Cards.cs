using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using System.Reflection.Emit;
using MegaCrit.Sts2.Core.Commands;

namespace SimplifiedAnimations;

[HarmonyPatch(typeof(NBigSlashVfx), nameof(NBigSlashVfx._Ready))]
public static class NBigSlashVfx__Ready_Patch
{
    public static bool Prefix(NBigSlashVfx __instance)
    {
        return !ModSettings.DisableBigSlashEffect;
    }
}

[HarmonyPatch(typeof(NBigSlashImpactVfx), nameof(NBigSlashImpactVfx._Ready))]
public static class NBigSlashImpactVfx__Ready_Patch
{
    public static bool Prefix(NBigSlashImpactVfx __instance)
    {
        return !ModSettings.DisableBigSlashEffect;
    }

}

// Reduce time spent between card draws; one card is drawn, then this delay is applied, then another is drawn, etc.
// Cards will essentially be drawn and move together.
[HarmonyPatch(typeof(CardPileCmd), "AppendPileLerpTween")]
public static class CardPileTween_Transpiler_Patch
{
    public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
    {
        if (!ModSettings.QuickerDraw) return instructions;

        var matcher = new CodeMatcher(instructions, generator);

        matcher.MatchStartForward(
            new CodeMatch(OpCodes.Ldc_R4, 0.25f),
            new CodeMatch(OpCodes.Stloc_2),
            new CodeMatch(OpCodes.Ldloc_2),
            new CodeMatch(OpCodes.Stloc_1)
        );

        if (matcher.IsInvalid)
        {
            Console.WriteLine("[SimplifiedAnimations] Transpiler match failed in AppendPileLerpTween!");
            return instructions;
        }

        // Move the cursor from the original Ldc_R4 to Ldloc_2, where we load a fixed value instead
        matcher.Advance(2);

        // Regardless of fast mode setting, use 0.02f (instant mode: 0.01f, fast: 0.1f, regular: 0.25f)
        var originalLabels = matcher.Instruction.ExtractLabels();
        var replacementInstruction = new CodeInstruction(OpCodes.Ldc_R4, 0.02f).WithLabels(originalLabels);
        matcher.SetInstruction(replacementInstruction);

        Console.WriteLine("[SimplifiedAnimations] AppendPileLerpTween successfully patched");
        return matcher.InstructionEnumeration();
    }
}