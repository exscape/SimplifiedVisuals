using BaseLib.Config;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace SimplifiedVisuals;

[ModInitializer("Initialize")]
public static class ModInitializer
{
    public static ModSettings? Settings;
    public const string ModId = "SimplifiedVisuals";

    public static void Initialize()
    {
        Settings = new ModSettings();
        ModConfigRegistry.Register(ModId, Settings);
        var harmony = new Harmony(ModId);
        harmony.PatchAll();
    }
}