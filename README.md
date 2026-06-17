# Better Chases+ - Enhanced Edition (source)

A community port of **Better Chases+** to **GTA V Enhanced** using ScriptHookVDotNet Enhanced / SHVDNE, with the UI moved from NativeUI to LemonUI.

This repository contains the **source code** for the Enhanced port. The compiled, ready-to-install release is published separately on GTA5-Mods.

> **All credit for the mod belongs to its original creators.** This is only an Enhanced compatibility port. See [Credits](#credits) and [NOTICE](NOTICE.md).

---

## Credits

* **Daimian** - developer and maintainer of Better Chases+

  * Mod: https://www.gta5-mods.com/scripts/better-chases
  * Original source: https://github.com/jglassmaker/GTAV-BetterChasesPlus
* **Eddlm** - creator of the original mod this continues

  * https://www.gta5-mods.com/scripts/better-chases-arrest-warrant
* **Guadmaz** - credited original work that Better Chases+ is based on

Enhanced port by **b3hold**. No ownership of the original mod is claimed.

---

## What changed for Enhanced

The gameplay and balance logic, including chase escalation, witness line-of-sight recognition, and arrest warrants, is preserved as closely as possible from the original mod.

The Enhanced port mainly consists of compatibility changes, UI integration changes, and helper systems needed for the new runtime.

**Enhanced compatibility shims** (`src/`)

* `EnhancedWanted.cs` - sets the wanted level through the Enhanced API using `Wanted.SetWantedLevel` and `ApplyWantedLevelChangeNow`.
* `GameClockCompat.cs` - adapts `World.CurrentDate` usage to the Enhanced `GTA.Chrono.GameClock`.
* `NativeUiCompatibility.cs` - provides a thin LemonUI-backed facade for the `NativeUI` menu API used by the original mod, so most menu code can remain intact.
* Gameplay API substitutions:

  * `Game.Player.WantedLevel` to `Game.Player.Wanted.WantedLevel`
  * `IsDriveable = false` to `IsUndriveable = true`
  * `ClearAnimation(...)` to `StopScriptedAnimationTask(...)`

**Helper subsystems added by this port**

These were added for the Enhanced port and were not part of the original mod:

* `Diagnostics.cs` - optional logging to `%appdata%\BetterChasesPlus\Enhanced\BetterChasesPlus.Enhanced.log`.
* `StoragePaths.cs` - stores config, warrants, and logs under `%appdata%\BetterChasesPlus\Enhanced\`, with migration support for older files.
* `XmlPersistence.cs` - persistence helper used for saving and loading data.

> Note: the original mod is distributed by its author as a decompilation export. The gameplay sources here are also decompilation-derived, then adapted for GTA V Enhanced.

---

## Building

Requires the .NET SDK. The project targets **.NET Framework 4.8, x64**.

1. Add the two reference assemblies to a `refs/` folder at the repository root. These files are not redistributed here.

   * `refs/ScriptHookVDotNet3.dll` - from your **ScriptHookVDotNet Enhanced** / SHVDNE install.
   * `refs/LemonUI.SHVDN3.dll` - from **LemonUI**.

2. Build the project:

   ```bash
   dotnet build -c Release
   ```

   You can also override the reference paths:

   ```bash
   dotnet build -c Release -p:ScriptHookVDotNet3Path="<path>\ScriptHookVDotNet3.dll" -p:LemonUIPath="<path>\LemonUI.SHVDN3.dll"
   ```

The Release build is configured with deterministic output and debug symbols disabled (`DebugType=none`) to avoid embedding local build paths. Exact binary reproduction depends on using the same toolchain and reference assembly versions.

---

## Install (compiled release)

See the GTA5-Mods release page for the ready-to-install version.

In short:

1. Install Script Hook V with Enhanced support.
2. Install SHVDNE.
3. Install LemonUI.
4. Copy `BetterChasesPlus.Enhanced.dll` and `BetterChasesConfig.xml` into your `...\Grand Theft Auto V Enhanced\scripts\` folder.

**Story Mode only. Do not use this in GTA Online.**

---

## License / permission

The original Better Chases+ source has **no explicit license**, so default copyright rules apply. All rights remain with the original authors: Daimian, Eddlm, and Guadmaz.

No license is granted here over the original work. This port is published as a community compatibility effort and credits the original work throughout.

If an original author objects to this port being available, it will be taken down on request.

See **[LICENSE_STATUS.md](LICENSE_STATUS.md)** for the full statement and **[NOTICE.md](NOTICE.md)** for attribution.
