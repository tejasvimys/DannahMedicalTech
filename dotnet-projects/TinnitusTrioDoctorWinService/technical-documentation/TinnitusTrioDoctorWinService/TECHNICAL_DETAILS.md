# TinnitusTrioDoctorWinService Technical Details

## Overview

This project wraps doctor-maintenance logic inside a Windows Service so it can run without a logged-in user.

## Runtime behavior

### Service startup

`Service1.OnStart()` initializes the timer-based execution pattern. Instead of doing all work once and exiting, the service configures itself based on values in `App.config`.

## Configuration controls

### `Mode`

- `Interval` - run repeatedly every configured number of minutes
- `Daily` - run once per day at the configured scheduled time

### `IntervalMinutes`

- used only when `Mode=Interval`
- controls the frequency of timer-based execution

### `ScheduledTime`

- used for daily scheduling
- expected in clock-time format

## Main background action

The service calls:

- `WinService.ResetDoctorDetails()`

This indicates the main function is doctor-record maintenance or state reset. The exact database-side implementation lives in referenced business/data code rather than in the service shell itself.

## Installer behavior

`ProjectInstaller.cs` configures deployment so the Windows Service:

- registers correctly with the service manager
- is set to start automatically
- begins running immediately after installation

## Logging

The service writes activity and errors to:

- `C:\ServiceLog.txt`

That file is the primary operational trace when diagnosing missed runs or failures.

## Operational workflow

1. Windows starts the service.
2. The service reads `Mode`, `IntervalMinutes`, and `ScheduledTime`.
3. A timer is armed using either interval or daily scheduling rules.
4. On each scheduled execution, the service runs doctor-maintenance logic.
5. Status is appended to the service log file.

## Control-by-control interpretation

Because this is a headless Windows Service, there are no buttons or form controls. The effective "controls" are the `App.config` settings:

- **Mode** controls which scheduler path is active.
- **IntervalMinutes** controls repeat frequency.
- **ScheduledTime** controls the daily fire time.
- **DBTinnitusTrio** controls which database the maintenance logic targets.

## Deployment considerations

- must run under an account with database connectivity
- must have write access to `C:\ServiceLog.txt`
- should be monitored because failures are not surfaced through a UI
