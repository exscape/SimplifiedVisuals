# Simplified Visuals for Slay the Spire 2

A mod that offers options to remove or tweak certain visual effects.  
Might be of use for people who are sensitive to motion/visual stimulation, easily distracted, or just want a "cleaner" look.  

**All changes are optional**, see the configuration menu (Settings -> Mod Settings -> Simplified Visuals, then click the cog wheel in the upper right).

**The mod works in multiplayer** even if you are the only player using it.

New in v0.1.2 (2026-06-11):
* New options for the new Waterfall Giant fight background
* New option to disable the Defect's new "Power Up" animation (when playing powers)
* Requires v0.107.0 (beta branch)

New in v0.1.1 (2026-05-08):

* Add "Reduced" for some options, as a middle-ground between show/enabled and hide/disabled. For example, for Big Slash and Starry Impact it reduces the brightness/flashiness instead of hiding them entirely
* Add option to disable the animation when swiching between runs in the Run History screen
* Add quick presets to show/reduce/hide all effects
* Add beta branch compatibility (v0.105.0) -- beta branch is now REQUIRED, don't update yet if you play on the standard game branch!

The mod supports removing:

* Various effects on the game's Timeline (moving stars, confetti, pulsing buttons)
* Radial blur: used by Bygone Effigy, Ceremonial Beast, Mecha Knight, Shrinker Beetle, Vantom
* "Big Slash": used by Perfected Strike, Sovereign Blade, Mecha Knight
* "Scream": used by Bygone Effigy, Ceremonial Beast, Mecha Knight, Shrinker Beetle, The Insatiable
* "Spooky scream": used by Haunted Ship, Soul Fysh
* Purple Doom screen overlay (when an enemy dies to Doom)
* The Insatiable's sandfalls and other sand VFX
* Waterfall Giant's waterfalls and other water VFX
* Defect's "Power Up" animation (when playing powers)
* Slippery Bridge event (disable rain)
* Regent's attack animation

You can also:

* Remove the delay between drawn cards, making them move together as a unit (also speeds up the game)
* Make Sovereign Blade stay still when idle
* Remove the glow/sparkles from rare and uncommon cards (in card rewards and unlocks)
* Remove the animation on the Run History screen, when moving between runs

Feel free to contact me via [GitHub issues](https://github.com/exscape/SimplifiedVisuals/issues) or on the Slay the Spire Discord (@Aeluwas) if you have suggestions on additional settings or other improvements, or to report issues.

v0.1.0 is tested with Slay the Spire 2 version 0.99.1 - 0.104.0. (It also works with 0.105.0, but with errors logged in the game.)  
v0.1.1 requires game version 0.105.0 (released 2026-05-08) or newer due to changes in the game's mod manifest format. It also requires BaseLib version 3.1.2 or higher.

Later game versions are likely to work just as well; if something breaks, it should be just a single effect toggle or two being affected.

## Installation

1. Ensure you have a `mods` folder where StS 2 is installed, for example: `C:\Program Files (x86)\Steam\steamapps\common\Slay the Spire 2\mods`
2. Ensure you have the latest version of [BaseLib](https://github.com/Alchyr/BaseLib-StS2/releases) installed: simply unpack the .zip in the above folder
3. Download this mod's [latest release](https://github.com/exscape/SimplifiedVisuals/releases) and unpack it into the mods folder

You can have separate folders for each mod, or simply unpack everything together; the game doesn't care.

When updating this mod, also check for updates to BaseLib, as I'm currently aggressively using new features for the mod configuration.
