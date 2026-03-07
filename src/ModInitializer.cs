using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;

namespace SimplifiedAnimations;

[ModInitializer("Initialize")]
public static class ModInitializer
{
    public static void Initialize()
    {
        ModSettings.Load();
        var harmony = new Harmony("SimplifiedAnimations");
        harmony.PatchAll();
    }
}