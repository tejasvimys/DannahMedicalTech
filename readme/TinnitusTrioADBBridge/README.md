# TinnitusTrioADBBridge

## Purpose

`TinnitusTrioADBBridge` is a Windows desktop support application used to provision and maintain Tinnitus Trio Android devices over USB/ADB. It acts as a clinician/support console for pushing APKs and content to a device, viewing patient details, generating reports, deactivating patients, and installing required USB drivers.

## Technology stack

- WinForms on .NET Framework 4.5
- DevExpress UI components
- Managed ADB integration and bundled `adb` tooling
- Layered BO/BAL/DAL solution structure
- AutoUpdater.NET for desktop update checks
- Local SQL Server MDF files and external service/database integrations

## Solution structure

- `TinnitusTrioADBBridge/` - main WinForms executable
- `TinnitusTrioADB_BAL/` - business logic
- `TinnitusTrioADB_DAL/` - data access
- `TinnitusTrioADB_BO/` - business objects, logging, USB models

## Main screens and modules

- splash and startup validation
- login / reset password / license manager routing
- query screen for patient lookup
- main console (`Form1`)
- patient detail and patient list forms
- renew license workflow
- report and sync user controls

## What the application does

- verifies connectivity, licensing, and startup conditions
- allows staff to select a patient/device context
- detects whether an Android device is attached
- opens task-specific user controls inside the main panel
- pushes application packages and content files to connected devices
- opens patient reports and installer reports
- launches bundled Samsung/ADB driver installers
- checks for desktop software updates from a remote XML manifest

## Important runtime assets

- `adb/` - bundled ADB binaries
- `adbdriver/` - ADB driver installer payloads
- `Drivers/` - Samsung USB driver installer
- `APK/` - packaged Android app artifacts for deployment
- `CMES/`, `CMM/` - content/data folders pushed during provisioning

## Startup flow

`Program.cs` performs the initial application routing. From the earlier analysis of the project:

1. internet connectivity and environment checks run first
2. splash screen is shown
3. the app decides whether to open:
   - `ResetPassword`
   - `LoginForm`
   - `LicenseManager`
4. after a patient/doctor context is selected, the main console opens

## How to run

### Requirements

- Windows
- Visual Studio 2013 or later
- .NET Framework 4.5
- DevExpress components matching the project version
- NuGet restore
- Android USB drivers
- A connected Android device with USB debugging enabled for device operations

### Run in Visual Studio

1. Open `TinnitusTrioADBBridge/TinnitusTrioADBBridge.sln`.
2. Restore NuGet packages.
3. Make sure DevExpress references resolve.
4. Set `TinnitusTrioADBBridge` as the startup project.
5. Confirm required runtime folders (`adb`, `adbdriver`, `Drivers`, `APK`) are present next to the executable output.
6. Start the application and sign in with a valid account/license state.

### Build

```bat
msbuild TinnitusTrioADBBridge.sln /t:Build /p:Configuration=Debug
```

## Legacy configuration notes

- The project contains checked-in local database files and environment-specific dependencies.
- Some features rely on remote services, update URLs, or backend database state.
- The main workflow assumes Windows USB driver installation rights and direct device access.

For the screen-by-screen and control-level walkthrough, see `TECHNICAL_DETAILS.md`.
