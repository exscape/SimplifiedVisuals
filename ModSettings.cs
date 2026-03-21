using BaseLib.Config;

namespace SimplifiedVisuals;

[HoverTipsByDefault]
public class ModSettings : SimpleModConfig
{
    [ConfigSection("CombatEffects")]
    public static bool DisableBigSlashEffect { get; set; } = true;
    public static bool DisablePurpleDoomOverlay { get; set; } = false;
    public static bool DisableRadialBlurEffect { get; set; } = true;
    public static bool DisableScreamEffect { get; set; } = true;
    public static bool DisableSpookyScreamEffect { get; set; } = true;

    public static bool DisableRegentAttackEffect { get; set; } = false;
    public static bool DisableSovereignBladeMovement { get; set; } = false;

    [ConfigSection("Environments")]
    public static bool DisableInsatiableSandfalls { get; set; } = true;
    public static bool DisableOtherInsatiableSandEffects { get; set; } = false;
    public static bool DisableRainEffect { get; set; } = false;

    [ConfigSection("Gameplay")]
    public static bool QuickerDraw { get; set; } = false;

    [ConfigSection("Timeline")]
    public static bool FreezeBackgroundStars { get; set; } = true;
    public static bool HideConfetti { get; set; } = true;
    public static bool DisableUnlockShockwaves { get; set; } = false;

    [ConfigSection("UserInterface")]
    public static bool DisableRareCardGlow { get; set; } = true;
}