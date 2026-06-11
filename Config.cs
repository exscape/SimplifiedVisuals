using BaseLib.Config;
using Godot;

namespace SimplifiedVisuals;

public enum EffectIntensity
{
    Default,
    Reduced,
    Disabled
}

[ConfigHoverTipsByDefault]
public class Config : SimpleModConfig
{
    [ConfigSection("GlobalPresets")]
    [ConfigButton("ShowAll")]
    public static void ShowAllEffects(ModConfig config) => ToggleAll(config, EffectIntensity.Default);

    [ConfigButton("ReduceAll")]
    public static void ReduceAllEffects(ModConfig config) => ToggleAll(config, EffectIntensity.Reduced);

    [ConfigButton("DisableAll")]
    public static void DisableAllEffects(ModConfig config) => ToggleAll(config, EffectIntensity.Disabled);

    [ConfigSection("CombatEffects")]
    public static EffectIntensity BigSlashEffect { get; set; } = EffectIntensity.Reduced;
    public static bool DisablePurpleDoomOverlay { get; set; } = false;
    public static bool DisableRadialBlurEffect { get; set; } = true;
    public static bool DisableScreamEffect { get; set; } = true;
    public static bool DisableSpookyScreamEffect { get; set; } = true;
    public static EffectIntensity StarryImpactEffect { get; set; } = EffectIntensity.Reduced;

    public static bool DisableRegentAttackEffect { get; set; } = false;
    public static bool DisableSovereignBladeMovement { get; set; } = false;
    public static bool DisableDefectPowerUpAnimation { get; set; } = false;

    [ConfigSection("Environments")]
    public static bool DisableInsatiableSandfalls { get; set; } = false;
    public static bool DisableOtherInsatiableSandEffects { get; set; } = false;
    public static bool DisableWaterfallGiantWaterfalls { get; set; } = false;
    public static bool FreezeWaterfallGiantBackground { get; set; } = false;
    public static bool DisableRainEffect { get; set; } = false;

    [ConfigSection("Gameplay")]
    public static bool QuickerDraw { get; set; } = false;

    [ConfigSection("Timeline")]
    public static bool FreezeBackgroundStars { get; set; } = true;
    public static bool HideConfetti { get; set; } = true;
    public static bool DisableUnlockShockwaves { get; set; } = false;

    [ConfigSection("UserInterface")]
    public static bool DisableRareCardGlow { get; set; } = true;

    public static bool DisableRunHistoryScreenTransition { get; set; } = true;

    private static void ToggleAll(ModConfig config, EffectIntensity action)
    {
        foreach (var prop in config.GetType().GetProperties())
        {
            if (prop.PropertyType == typeof(bool))
                prop.SetValue(null, action != EffectIntensity.Default);
            else if (prop.PropertyType == typeof(EffectIntensity))
                prop.SetValue(null, action);
        }
    }
}