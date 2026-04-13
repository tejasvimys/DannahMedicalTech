# TinnitusTrioADBBridge Technical Details

## Overview

This application is the desktop operator console for provisioning and servicing Tinnitus Trio Android devices. The UI is centered on `Form1`, which behaves like a shell: clicking a navigation item swaps a user control into `pnlMain`.

## Startup and routing

Based on `Program.cs`, the application does not jump straight to the main window. It first evaluates environment and account state, then routes to one of several forms:

- `SplashScreen`
- `ResetPassword`
- `LoginForm`
- `LicenseManager`

After login/patient selection, the main console is shown.

## Main form responsibilities

`Form1` keeps the current operational context in labels such as:

- patient ID
- doctor ID
- doctor clinic
- patient name

On load, it:

1. applies branded background assets
2. resolves the patient name through the BAL layer
3. prepares the panel that hosts the working user controls

## Navigation items and what they do

### Exit button

- `simpleButton1_Click`
- shows **"Are You Sure To Exit?"**
- closes the form only when the user confirms

### Patient Console

- `patientDetails_LinkClicked`
- currently contains commented-out logic for a patient detail user control
- appears to have been intended as a patient summary landing item

### Install Device Driver

- `deviceDriverInstall_LinkClicked`
- launches `adbdriver\ADBDriverInstaller.exe`
- embeds the launched installer window under the main form using `SetParent`
- used when generic ADB drivers are missing

### Push Apps to Device

- `navpushappstodevice_LinkClicked`
- checks SD-card/device connectivity through `GetSdCardPath()`
- if the device is reachable, loads `UC_InstallAPK`
- passes patient ID, doctor ID, and clinic name into the control
- if the device is not reachable, shows a warning dialog instead of continuing

### Push Files To Device

- `navPushitemstoDevice_LinkClicked`
- runs the same connectivity gate as the APK flow
- on success, loads `UC_PushFiles`
- intended for pushing media/content/data folders rather than application binaries

### Get User / Device Details

- `outboxItem_LinkClicked`
- checks for device connectivity
- loads `UC_GetUserDetails`
- passes patient ID, doctor ID, and clinic name into the control
- used to inspect device-side identity or installation state

### Deactivate Patient

- `DeActivatePatient_Clicked`
- loads `UC_DeactivatePatient`
- passes the current patient ID
- gives support staff a dedicated control for deactivation workflows

### Reports

- `lnkRpt_Clicked`
- loads `UC_PatientReports`
- passes patient ID and patient name
- intended for viewing therapy/device/report outputs associated with the patient

### Patient Records

- `patientRecords_LinkClicked`
- loads `UC_PatientList`
- used as a browsable patient record inventory

### Patient Log Details / Installer Report

- `pntRecords_ItemChanged`
- `pntRecords_LinkChanged`
- both load `UC_PatientInstallerReport`
- pass the current doctor ID
- appears to show installation/provisioning history per doctor/patient scope

### Samsung Driver

- `samsungdriver_linkClicked`
- launches `Drivers\SAMSUNG_USB_Driver_for_Mobile_Phones.exe`
- intended for Samsung-specific USB driver installation

### Check for Updates

- `checkforUpdates_LinkClicked`
- calls `AutoUpdater.Start("http://52.37.207.186/TinnitusTrioUpdate/Updater.xml")`
- checks a remote XML manifest for desktop application updates

### Change/Search Patient context

- `simpleButton2_Click`
- hides the current form
- opens `QueryScreen`
- passes clinic and doctor identifiers
- lets the operator switch to another patient/device context

## Connectivity gating pattern

Several actions use the same pattern before they open a device-facing tool:

1. call `GetSdCardPath()`
2. treat empty output or daemon startup output as failure
3. if valid, load the requested user control
4. otherwise, show a warning that the device is disconnected or drivers are missing

This prevents APK push, file push, and device detail flows from running without a connected Android target.

## Embedded helper installers

The desktop application doubles as a launcher for support utilities:

- generic ADB driver installer
- Samsung USB driver installer

Instead of telling the user to locate these tools manually, the app starts them directly from its bundled folders.

## Error handling style

Most event handlers:

- catch exceptions
- forward details into `TinnitusTrioADB_BO.TinnitusTrioLogger.SendErrorReport(...)`
- show a branded generic error dialog to the operator

This keeps the operator-facing UI simple while preserving a support log trail.

## Related forms and workflow components

The project tree shows additional forms that support the end-to-end workflow:

- `InsertPatientForm`
- `PatientIDEntry`
- `PatientViewForm`
- `QueryScreen`
- `RenewLicenseForm`
- `ResetPassword`
- `LoginForm`
- `PleaseWait`

Together they suggest this workflow:

1. sign in / satisfy licensing
2. identify doctor and patient
3. attach a device over USB
4. push apps or files
5. inspect reports or logs
6. deactivate/reactivate or renew support state as needed
