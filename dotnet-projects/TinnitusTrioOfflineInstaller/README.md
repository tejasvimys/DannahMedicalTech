# TinnitusTrioOfflineInstaller

## Purpose

`TinnitusTrioOfflineInstaller` is not a standalone end-user application. It is a packaging workspace for building an installer around the offline/desktop ADB Bridge toolchain. The folder contains an InstallShield setup project plus a nested copy of the ADB Bridge solution and its BO/BAL/DAL projects.

## Technology stack

- InstallShield Limited / InstallShield project files
- WinForms on .NET Framework 4.5 for the packaged desktop app
- layered BO/BAL/DAL supporting projects

## Folder structure

- `TinnitusTrioADBBridge/`
  - `ADBSetupProject/` - InstallShield setup project
  - `TinnitusTrioADBBridge/` - packaged WinForms executable project
  - `TinnitusTrioADB_BAL/` - business layer
  - `TinnitusTrioADB_DAL/` - data layer
  - `TinnitusTrioADB_BO/` - business objects

## What this project does

- builds the offline ADB Bridge desktop application
- wraps that application into an installer package
- bundles supporting runtime assets and project outputs into a redistributable setup build

## Key packaging details

- `ADBSetupProject.isproj` is the main installer project
- it imports `InstallShield.targets`
- it references `..\TinnitusTrioADBBridge\TinnitusTrioADBBridge.csproj`
- the packaged desktop app targets **.NET Framework 4.5**

## How to build/run

### Requirements

- Windows
- Visual Studio with InstallShield Limited/2015 integration
- .NET Framework 4.5 targeting pack
- any third-party dependencies required by the nested `TinnitusTrioADBBridge` project

### Build the packaged application

1. Open the nested solution at `TinnitusTrioOfflineInstaller/TinnitusTrioADBBridge/TinnitusTrioADBBridge.sln`.
2. Restore packages and resolve third-party references.
3. Build the `TinnitusTrioADBBridge` WinForms project.

### Build the installer

1. In Visual Studio, load the InstallShield project `ADBSetupProject.isproj`.
2. Ensure InstallShield integration is available.
3. Build the setup project for the desired configuration.

This produces the installer package rather than launching a normal application directly from the root folder.

## Legacy notes

- This workspace exists for packaging/deployment, not for direct business use.
- The actual application behavior is inherited from the nested `TinnitusTrioADBBridge` project.

For the packaging workflow and setup-level details, see `TECHNICAL_DETAILS.md`.
