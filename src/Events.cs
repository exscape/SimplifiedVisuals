using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace SimplifiedAnimations;

// Hides the rain used in the Slippery Bridge event
[HarmonyPatch(typeof(NRainVfx), nameof(NRainVfx.Create))]
public static class RemoveRainVfxPatch
{
    public static void Postfix(ref NRainVfx __result)
    {
        __result.SelfModulate = Colors.Transparent;
    }
}