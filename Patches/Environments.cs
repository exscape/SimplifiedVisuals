using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace SimplifiedVisuals.Patches;

// Hide The Insatiable "waterfalls" (sandfalls) and some actual waterfalls from Waterfall Giant
[HarmonyPatch(typeof(NCombatBackground), nameof(NCombatBackground.Create))]
public static class DisableWaterfallsPatch
{
    private static readonly string[] VerticalWaterfalls =
    [
        "gpu waterfall",
        "gpu waterfall 1",
        "gpu waterfall2",
        "gpu waterfall 2",
        "gpu waterfall 3",
        "WATERFALL TYPE 24",
        "waterfall sparkles2",
        "oil 1",
        "oil 2",
        "oil 7",
        "oil 8"
    ];

    private static readonly string[] WaterfallBottoms =
    [
        "waterfall bottom",
        "waterfall bottom2",
        "waterfall bottom3",
        "waterfall bottom4"
    ];

    public static void Postfix(NCombatBackground __result)
    {
        if (__result.SceneFilePath.Contains("waterfall_giant_boss")) PatchWaterfallGiant(__result);
        if (__result.SceneFilePath.Contains("the_insatiable_boss")) PatchTheInsatiable(__result);
    }

    private static void PatchWaterfallGiant(NCombatBackground __result)
    {
        if (Config.DisableWaterfallGiantWaterfalls)
        {
            foreach (var prefix in VerticalWaterfalls)
                __result.HideAndDisable($"{prefix}*", remainVisible: false);
            foreach (var prefix in WaterfallBottoms)
                __result.HideAndDisable($"{prefix}*", remainVisible: true);
        }

        if (!Config.FreezeWaterfallGiantBackground) return;

        __result.ProcessMode = Node.ProcessModeEnum.Disabled;

        // These aren't affected by ProcessMode, but hiding them is easy and doesn't have a huge impact.
        foreach (var prefix in VerticalWaterfalls)
            __result.HideAndDisable($"{prefix}*", remainVisible: false);
        __result.HideAndDisable("water_reflection*", remainVisible: false);
    }

    private static void PatchTheInsatiable(NCombatBackground __result)
    {
        if (Config.DisableInsatiableSandfalls)
            __result.HideAndDisable("gpu waterfall*", true);
        if (Config.DisableOtherInsatiableSandEffects)
            __result.HideAndDisable("*sand*", true);
    }
}

// Freeze swirling sand around The Insatiable
[HarmonyPatch(typeof(NCreatureVisuals), nameof(NCreatureVisuals._Ready))]
public static class DisableSandCloudPatch
{
    public static void Postfix(NCreatureVisuals __instance)
    {
        if (!Config.DisableOtherInsatiableSandEffects) return;
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
        if (Config.DisableRainEffect)
            __result.SelfModulate = Colors.Transparent;
    }
}