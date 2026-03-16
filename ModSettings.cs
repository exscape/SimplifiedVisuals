using BaseLib.Config;

namespace SimplifiedVisuals;

public class ModSettings : SimpleModConfig
{
    [ConfigSection("CombatEffects")]
    public static bool DisableBigSlashEffect { get; set; } = true;
    public static bool DisablePurpleDoomOverlay { get; set; } = true;
    public static bool DisableRadialBlurEffect { get; set; } = true;
    public static bool DisableScreamEffect { get; set; } = true;
    public static bool DisableSpookyScreamEffect { get; set; } = true;

    public static bool DisableRegentAttackEffect { get; set; } = false;
    public static bool DisableSovereignBladeMovement { get; set; } = true;

    [ConfigSection("Environments")]
    public static bool DisableInsatiableSandfalls { get; set; } = true;
    public static bool DisableOtherInsatiableSandEffects { get; set; } = true;
    public static bool DisableRainEffect { get; set; } = true;

    [ConfigSection("Gameplay")]
    public static bool QuickerDraw { get; set; } = true;

    [ConfigSection("Timeline")]
    public static bool FreezeBackgroundStars { get; set; } = true;
    public static bool HideConfetti { get; set; } = true;
    public static bool DisableUnlockShockwaves { get; set; } = true;

    [ConfigSection("UserInterface")]
    public static bool DisableRareCardGlow { get; set; } = true;
}