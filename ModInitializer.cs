using BaseLib.Config;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace SimplifiedVisuals;

[ModInitializer("Initialize")]
public static class ModInitializer
{
    public const string ModId = "SimplifiedVisuals";

    public static void Initialize()
    {
        ModConfigRegistry.Register(ModId, new Config());
        var harmony = new Harmony(ModId);
        harmony.PatchAll();
    }
}