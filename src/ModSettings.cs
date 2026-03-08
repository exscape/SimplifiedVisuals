using System.Reflection.Metadata.Ecma335;
using Godot;
namespace SimplifiedAnimations;

public static class ModSettings
{
    private static readonly ConfigFile _config = new();

    // Cards section
    public static bool DisableBigSlashEffect { get; private set; } = true;
    public static bool QuickerDraw { get; private set; } = true;

    // Events section
    public static bool DisableRainEffect { get; private set; } = true;

    // Timeline section
    public static bool HideConfetti { get; private set; } = true;
    public static bool FreezeBackgroundStars { get; private set; } = true;
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

            // Cards section
            DisableBigSlashEffect = (bool)_config.GetValue("Cards", "DisableBigSlashEffect", DisableBigSlashEffect);

            // Events section
            DisableRainEffect = (bool)_config.GetValue("Events", "DisableRainEffect", DisableRainEffect);

            // Timeline section
            HideConfetti = (bool)_config.GetValue("Timeline", "HideConfetti", HideConfetti);
            FreezeBackgroundStars = (bool)_config.GetValue("Timeline", "FreezeBackgroundStars", FreezeBackgroundStars);
            DisableUnlockShockwaves = (bool)_config.GetValue("Timeline", "DisableUnlockShockwaves", DisableUnlockShockwaves);
        }
        else
        {
            Console.WriteLine("[SimplifiedAnimations] No config found! Using default values.");
        }
    }
}