# TinnitusTrioOfflineInstaller Technical Details

## Overview

This folder is a deployment wrapper. Its primary role is to convert the offline ADB Bridge desktop application into an installable package.

## Key project pieces

### `ADBSetupProject.isproj`

This is the main setup project file.

Important elements:

- `InstallShieldProductConfiguration` is set to `Express`
- the setup project imports `InstallShield.targets`
- the project points at an InstallShield source file (`.isl`)
- it explicitly references the WinForms executable project `TinnitusTrioADBBridge.csproj`

### Nested application projects

The setup project packages output from:

- `TinnitusTrioADBBridge` - main WinForms executable
- `TinnitusTrioADB_BAL` - business logic DLL
- `TinnitusTrioADB_DAL` - data access DLL
- `TinnitusTrioADB_BO` - business objects DLL

## Build pipeline

1. build the WinForms executable and its referenced class libraries
2. InstallShield probes the project outputs
3. setup authoring files determine what gets included in the installer
4. the final setup package is emitted for offline deployment

## Effective controls

Because this is an installer workspace, there are no runtime buttons or forms at the root-project level. The important "controls" are packaging-time settings:

- **InstallShieldProductConfiguration** - selects the setup product configuration
- **Configuration** - selects Debug/Release setup output
- **ProjectReference** - determines which application output is packaged

## Functional relationship to ADB Bridge

The installer does not implement provisioning logic itself. It simply packages the application that does:

- login and licensing
- device detection
- APK/file deployment
- patient reporting
- driver installation

Those runtime features live in the nested `TinnitusTrioADBBridge` project, not in the installer metadata.

## Intended use

- support teams build a setup package
- the package is distributed to Windows operator machines
- installed machines then run the offline ADB Bridge app locally

## Constraints

- requires InstallShield support in Visual Studio
- depends on the nested app building successfully first
- installer output contents are defined by legacy setup authoring rather than modern packaging conventions
