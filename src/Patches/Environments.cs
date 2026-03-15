using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace SimplifiedVisuals.Patches;

// Hide The Insatiable "waterfalls" (sandfalls)
[HarmonyPatch(typeof(NCombatBackground), nameof(NCombatBackground.Create))]
public static class DisableWaterfallsPatch
{
    public static void Postfix(NCombatBackground __result)
    {
        if (!__result.SceneFilePath.Contains("the_insatiable_boss")) return;

        if (ModSettings.DisableInsatiableSandfalls)
        {
            for (var i = 1; i <= 9; i++)
            {
                var sandfall = __result.GetNodeOrNull<Node2D>($"gpu waterfall {i}");
                if (sandfall == null) continue;

                sandfall.Visible = false;
                sandfall.ProcessMode = Node.ProcessModeEnum.Disabled;
            }
        }

        if (!ModSettings.DisableOtherInsatiableSandEffects) return;

        foreach (var child in __result.FindChildren("*sand*"))
        {
            if (child is not GpuParticles2D gpuParticles) continue;
            gpuParticles.ProcessMode = Node.ProcessModeEnum.Disabled;
            gpuParticles.Visible = false;
        }
    }
}

// Freeze swirling sand around The Insatiable
[HarmonyPatch(typeof(NCreatureVisuals), nameof(NCreatureVisuals._Ready))]
public static class DisableSandCloudPatch
{
    public static void Postfix(NCreatureVisuals __instance)
    {
        if (!ModSettings.DisableOtherInsatiableSandEffects) return;
        var sandTransform = __instance.GetNodeOrNull<Node2D>("Visuals/SandSlotNode/GroundSandMasterTransform");
        if (sandTransform == null) return;
        sandTransform.ProcessMode = Node.ProcessModeEnum.Disabled;
    }
}

// Hides the rain used in the Slippery Bridge event
[HarmonyPatch(typeof(NRainVfx), nameof(NRainVfx.Create))]
public static class RemoveRainVfxPatch
{
    public static void Postfix(ref NRainVfx __result)
    {
        if (ModSettings.DisableRainEffect)
            __result.SelfModulate = Colors.Transparent;
    }
}