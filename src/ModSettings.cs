using Godot;

namespace SimplifiedAnimations;

public interface IModSetting
{
    void LoadFrom(ConfigFile config);
}

public class Setting<T> : IModSetting
{
    private T Value { get; set;  } // The operator overload handles standard reads
    private string Section { get; }
    private string Name { get; }

    public Setting(string section, string name, T defaultValue)
    {
        Section = section;
        Name = name;
        Value = defaultValue;
        ModSettings.Register(this);
    }

    public void LoadFrom(ConfigFile config)
    {
        Value = config.GetValue(Section, Name, Variant.From(Value)).As<T>();
    }

    public static implicit operator T(Setting<T> setting) => setting.Value;
}

public static class ModSettings
{
    private static readonly ConfigFile _config = new();
    private static readonly List<IModSetting> _allSettings = [];
    internal static void Register(IModSetting setting) => _allSettings.Add(setting);

    // CombatEffects
    public static readonly Setting<bool> DisableBigSlashEffect = new("CombatEffects", nameof(DisableBigSlashEffect), true);
    public static readonly Setting<bool> DisablePurpleDoomOverlay = new("CombatEffects", nameof(DisablePurpleDoomOverlay), true);
    public static readonly Setting<bool> DisableRadialBlurEffect = new("CombatEffects", nameof(DisableRadialBlurEffect), true);
    public static readonly Setting<bool> DisableScreamEffect = new("CombatEffects", nameof(DisableScreamEffect), true);
    public static readonly Setting<bool> DisableSpookyScreamEffect = new("CombatEffects", nameof(DisableSpookyScreamEffect), true);

    public static readonly Setting<bool> DisableRegentAttackEffect = new("CombatEffects", nameof(DisableRegentAttackEffect), false);

    // Environments
    public static readonly Setting<bool> DisableInsatiableSandfalls = new("Environments", nameof(DisableInsatiableSandfalls), true);
    public static readonly Setting<bool> DisableOtherInsatiableSandEffects = new("Environments", nameof(DisableOtherInsatiableSandEffects), true);
    public static readonly Setting<bool> DisableRainEffect = new("Environments", nameof(DisableRainEffect), true);

    // Gameplay
    public static readonly Setting<bool> QuickerDraw = new("Gameplay", nameof(QuickerDraw), true);

    // Timeline
    public static readonly Setting<bool> FreezeBackgroundStars = new("Timeline", nameof(FreezeBackgroundStars), true);
    public static readonly Setting<bool> HideConfetti = new("Timeline", nameof(HideConfetti), true);
    public static readonly Setting<bool> DisableUnlockShockwaves = new("Timeline", nameof(DisableUnlockShockwaves), true);

    // UI
    public static readonly Setting<bool> DisableRareCardGlow = new("UI", nameof(DisableRareCardGlow), true);
    public static readonly Setting<bool> DisableUncommonCardGlow = new("UI", nameof(DisableUncommonCardGlow), true);

    public static void Load()
    {
        var gameDir = Path.GetDirectoryName(OS.GetExecutablePath());
        if (gameDir == null)
        {
            Console.WriteLine("[SimplifiedAnimations] Failed to load config: unable to figure out game directory");
            return;
        }

        var configPath = Path.Combine(gameDir, "mods", "SimplifiedAnimations", "configuration.ini");
        var err = _config.Load(configPath);

        if (err == Error.Ok)
        {
            Console.WriteLine("[SimplifiedAnimations] Loaded configuration.ini");

            foreach (var setting in _allSettings)
            {
                setting.LoadFrom(_config);
            }
        }
        else
        {
            Console.WriteLine("[SimplifiedAnimations] No config found! Using default values.");
        }
    }
}