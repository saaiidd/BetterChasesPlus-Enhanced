# Better Chases+ — Enhanced Edition (source)

A community port of **Better Chases+** to **GTA V Enhanced** (ScriptHookVDotNet
Enhanced / SHVDNE), with the UI moved from NativeUI to LemonUI.

This repository holds the **source** for the Enhanced port. The compiled,
ready-to-install release is published separately on GTA5-Mods.

> **All credit for the mod belongs to its original creators.** This is only an
> Enhanced compatibility port. See [Credits](#credits) and [NOTICE](NOTICE.md).

---

## Credits

- **Daimian** — developer & maintainer of Better Chases+
  - Mod: https://www.gta5-mods.com/scripts/better-chases
  - Original source: https://github.com/jglassmaker/GTAV-BetterChasesPlus
- **Eddlm** — original mod this continues
  - https://www.gta5-mods.com/scripts/better-chases-arrest-warrant
- **Guadmaz** — original work it is based on

Enhanced port by **b3hold**. No ownership of the mod itself is claimed.

---

## What changed for Enhanced

The gameplay/balance logic (chase escalation, witness line-of-sight recognition,
arrest warrants) is the original author's, unchanged. The port consists of:

**Enhanced compatibility shims** (`src/`)
- `EnhancedWanted.cs` — sets the wanted level via the Enhanced API
  (`Wanted.SetWantedLevel` + `ApplyWantedLevelChangeNow`).
- `GameClockCompat.cs` — adapts `World.CurrentDate` to the Enhanced
  `GTA.Chrono.GameClock`.
- `NativeUiCompatibility.cs` — a thin LemonUI-backed facade exposing the
  `NativeUI` menu API the original used, so the menu code is unchanged.
- API substitutions in the gameplay files: `Game.Player.WantedLevel` →
  `Game.Player.Wanted.WantedLevel`, `IsDriveable = false` →
  `IsUndriveable = true`, `ClearAnimation(...)` → `StopScriptedAnimationTask(...)`.

**Helper subsystems added by this port** (not present in the original mod)
- `Diagnostics.cs` — optional logging to
  `%appdata%\BetterChasesPlus\Enhanced\BetterChasesPlus.Enhanced.log`.
- `StoragePaths.cs` — stores config/warrants/log under
  `%appdata%\BetterChasesPlus\Enhanced\`, with migration of older files.
- `XmlPersistence.cs` — persistence helper.

> Note: the original mod is itself distributed as a decompilation export by its
> author; the gameplay sources here are likewise decompilation-derived, then
> adapted for Enhanced.

---

## Building

Requires the .NET SDK (the project targets **.NET Framework 4.8, x64**).

1. Supply the two reference assemblies (not redistributed here) in a `refs/`
   folder at the repo root:
   - `refs/ScriptHookVDotNet3.dll` — from your **ScriptHookVDotNet Enhanced**
     (SHVDNE) install.
   - `refs/LemonUI.SHVDN3.dll` — from **LemonUI**.
2. Build:
   ```
   dotnet build -c Release
   ```
   or override the reference paths:
   ```
   dotnet build -c Release -p:ScriptHookVDotNet3Path="<path>\ScriptHookVDotNet3.dll" -p:LemonUIPath="<path>\LemonUI.SHVDN3.dll"
   ```

The build is configured for deterministic Release output with debug symbols
disabled (`DebugType=none`), so it avoids embedding local build paths. Exact
binary reproduction depends on matching the same toolchain and reference
assembly versions.

---

## Install (compiled release)

See the GTA5-Mods release page. In short: install Script Hook V (Enhanced) +
SHVDNE + LemonUI, then drop `BetterChasesPlus.Enhanced.dll` and
`BetterChasesConfig.xml` into your `...\Grand Theft Auto V Enhanced\scripts\`
folder. **Story Mode only — do not use in GTA Online.**

---

## License / permission

The original Better Chases+ has **no explicit license**, so default copyright
applies — all rights remain with its authors (Daimian / Eddlm / Guadmaz) and no
license is granted here over the original work. This port is published as a
community compatibility effort and credits the original work throughout. If an
original author objects to this port being available, it will be taken down on
request.

See **[LICENSE_STATUS.md](LICENSE_STATUS.md)** for the full statement and
**[NOTICE.md](NOTICE.md)** for attribution.
