using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace SimplifiedVisuals;

[ModInitializer("Initialize")]
public static class ModInitializer
{
    public static Harmony? harmony;
    public static void Initialize()
    {
        ModSettings.Load();
        harmony = new Harmony("SimplifiedVisuals");
        harmony.PatchAll();
    }
}