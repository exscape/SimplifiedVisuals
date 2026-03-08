using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace SimplifiedAnimations;

[ModInitializer("Initialize")]
public static class ModInitializer
{
    public static Harmony? harmony;
    public static void Initialize()
    {
        ModSettings.Load();
        harmony = new Harmony("SimplifiedAnimations");
        harmony.PatchAll();
    }
}