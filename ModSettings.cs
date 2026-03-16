using BaseLib.Config;

namespace SimplifiedVisuals;

public class ModSettings : SimpleModConfig
{
    // CombatEffects
    public static bool DisableBigSlashEffect { get; set; } = true;
    public static bool DisablePurpleDoomOverlay { get; set; } = true;

    public static bool DisableRadialBlurEffect { get; set; } = true;
    public static bool DisableScreamEffect { get; set; } = true;
    public static bool DisableSpookyScreamEffect { get; set; } = true;


    public static bool DisableRegentAttackEffect { get; set; } = false;
    public static bool DisableSovereignBladeMovement { get; set; } = true;

    // Environments
    public static bool DisableInsatiableSandfalls { get; set; } = true;
    public static bool DisableOtherInsatiableSandEffects { get; set; } = true;
    public static bool DisableRainEffect { get; set; } = true;

    // Gameplay
    public static bool QuickerDraw { get; set; } = true;

    // Timeline
    public static bool FreezeBackgroundStars { get; set; } = true;
    public static bool HideConfetti { get; set; } = true;
    public static bool DisableUnlockShockwaves { get; set; } = true;

    // UI
    public static bool DisableRareCardGlow { get; set; } = true;
}