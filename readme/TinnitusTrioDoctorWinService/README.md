# TinnitusTrioDoctorWinService

## Purpose

`TinnitusTrioDoctorWinService` is a Windows Service that performs background doctor-maintenance tasks on a schedule. The service is designed to run unattended and periodically reset or update doctor details according to a configured mode.

## Technology stack

- Windows Service on .NET Framework 4.5
- Timer-based scheduling
- SQL Server connectivity
- Installer-backed service deployment

## What it does

- starts automatically with Windows after installation
- reads scheduling settings from `App.config`
- supports at least two run modes:
  - `DAILY`
  - `INTERVAL`
- writes service activity to `C:\ServiceLog.txt`
- calls backend business logic such as `WinService.ResetDoctorDetails()`

## Configuration

`App.config` defines:

- `Mode` - scheduler mode
- `IntervalMinutes` - interval frequency when running in interval mode
- `ScheduledTime` - target clock time for daily mode
- `DBTinnitusTrio` - SQL Server connection string

The checked-in configuration uses a legacy remote SQL endpoint. Replace it with a safe environment-specific value before deployment.

## How to run

### Requirements

- Windows
- Visual Studio 2013 or later
- .NET Framework 4.5
- permission to install Windows Services
- backend database access

### Debug/run as a project

1. Open `TinnitusTrioDoctorWinService.sln`.
2. Update `App.config` with a valid connection string and desired scheduling values.
3. Build the solution.
4. If the project includes a debug harness, use it; otherwise install the service and test through the Windows Service Control Manager.

### Install the service

Use the .NET Framework `InstallUtil.exe` tool from an elevated Developer Command Prompt:

```bat
InstallUtil.exe TinnitusTrioDoctorWinService.exe
```

The installer project is configured to start the service automatically after installation.

### Uninstall

```bat
InstallUtil.exe /u TinnitusTrioDoctorWinService.exe
```

## Legacy notes

- Logging is file-based rather than centralized.
- Schedule behavior is config-driven.
- This service is intended to be deployed on a managed Windows host rather than run interactively.

For method-level and schedule behavior details, see `TECHNICAL_DETAILS.md`.
