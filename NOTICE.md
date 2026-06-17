# NOTICE

This repository is a **community Enhanced Edition port** of **Better Chases+**. It is not an original mod.

## Original mod and authors

* **Better Chases+** by **Daimian** / GitHub: `jglassmaker`

  * https://www.gta5-mods.com/scripts/better-chases
  * https://github.com/jglassmaker/GTAV-BetterChasesPlus

* Better Chases+ continues earlier work from **Eddlm** and **Guadmaz**, with permission from the original authors.

  * https://www.gta5-mods.com/scripts/better-chases-arrest-warrant

All rights to the original mod, its design, and its gameplay systems remain with the original authors.

## This port

* GTA V Enhanced compatibility port by **b3hold**.
* Built for ScriptHookVDotNet Enhanced / SHVDNE.
* UI adapted from NativeUI to LemonUI.
* Compatibility shims added:

  * `EnhancedWanted`
  * `GameClockCompat`
  * LemonUI-backed `NativeUI` facade
* Helper subsystems added:

  * `Diagnostics`
  * `StoragePaths`
  * `XmlPersistence`

These compatibility and helper systems were added for the Enhanced port and were not part of the original mod.

The gameplay and balance logic is preserved as closely as possible from the original Better Chases+ mod. The Enhanced port changes are limited to runtime compatibility, UI integration, storage/logging helpers, and required API substitutions.

## Third-party components

These components are referenced by the project but are **not redistributed in this source repository**:

* **ScriptHookVDotNet Enhanced / SHVDNE**

  * https://www.gta5-mods.com/tools/script-hook-v-net-enhanced

* **LemonUI** / MIT License

  * https://github.com/LemonUIbyLemon/LemonUI

* **Script Hook V** by Alexander Blade

  * http://www.dev-c.com/gtav/scripthookv/

These dependencies must be obtained from their own official sources.

## Permission

The original Better Chases+ source has no explicit license. Default copyright rules apply, and all rights remain with the original authors.

This port is shared as a community compatibility effort and credits the original work throughout. If an original author objects to this port being available, it will be taken down on request.
