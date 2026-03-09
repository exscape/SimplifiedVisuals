using Godot;
namespace SimplifiedAnimations;

public static class ModSettings
{
    private static readonly ConfigFile _config = new();

    // Events section
    public static bool DisableRainEffect { get; private set; } = true;

    // Gameplay section
    public static bool QuickerDraw { get; private set; } = true;
    //public static float HandDrawSpeed { get; private set; } = 2f;

    //VFX section
    public static bool DisableBigSlashEffect { get; private set; } = true;
    public static bool DisablePurpleDoomOverlay { get; private set; } = true;

    // Timeline section
    public static bool FreezeBackgroundStars { get; private set; } = true;
    public static bool HideConfetti { get; private set; } = true;
    public static bool DisableUnlockShockwaves { get; private set; } = true;

    public static void Load()
    {
        var gameDir = Path.GetDirectoryName(OS.GetExecutablePath());
        if (gameDir == null)
        {
            Console.WriteLine("[SimplifiedAnimations] Failed to load config: unable to figure out game directory");
            return;
        }

        var ConfigPath = Path.Combine([gameDir, "mods", "SimplifiedAnimations", "configuration.ini"]);

        var err = _config.Load(ConfigPath);

        if (err == Error.Ok)
        {
            Console.WriteLine("[SimplifiedAnimations] Loaded configuration.ini");

            // Events section
            DisableRainEffect = (bool)_config.GetValue("Events", "DisableRainEffect", DisableRainEffect);

            // Gameplay section
            QuickerDraw = (bool)_config.GetValue("Gameplay", "QuickerDraw", QuickerDraw);

            // VFX section
            DisableBigSlashEffect = (bool)_config.GetValue("VFX", "DisableBigSlashEffect", DisableBigSlashEffect);
            DisablePurpleDoomOverlay = (bool)_config.GetValue("VFX", "DisablePurpleDoomOverlay", DisablePurpleDoomOverlay);

            // Timeline section
            FreezeBackgroundStars = (bool)_config.GetValue("Timeline", "FreezeBackgroundStars", FreezeBackgroundStars);
            HideConfetti = (bool)_config.GetValue("Timeline", "HideConfetti", HideConfetti);
            DisableUnlockShockwaves = (bool)_config.GetValue("Timeline", "DisableUnlockShockwaves", DisableUnlockShockwaves);
        }
        else
        {
            Console.WriteLine("[SimplifiedAnimations] No config found! Using default values.");
        }
    }
}