using BaseLib.Config;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace SimplifiedVisuals;

[ModInitializer("Initialize")]
internal static class Main
{
    public const string ModId = "SimplifiedVisuals";
    public static MegaCrit.Sts2.Core.Logging.Logger Logger { get; } = new(ModId, MegaCrit.Sts2.Core.Logging.LogType.Generic);

    public static void Initialize()
    {
        ModConfigRegistry.Register(ModId, new Config());
        var harmony = new Harmony(ModId);
        harmony.PatchAll();
    }
}