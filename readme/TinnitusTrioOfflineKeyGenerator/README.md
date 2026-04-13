# TinnitusTrioOfflineKeyGenerator

## Purpose

`TinnitusTrioOfflineKeyGenerator` is a small Windows desktop utility that generates an offline short key from an Android/device popup key. It is used as a support or activation aid in environments where a manual key needs to be derived without calling a live service.

## Technology stack

- WinForms on .NET Framework (x86 build)
- DevExpress UI controls
- SHA1 hashing for key derivation

## What the tool does

- accepts a source key pasted by the operator
- computes a SHA1 hash
- takes the first 8 characters of the resulting hash string
- displays that value as the offline/generated key

## Main files

- `Program.cs` - application entry point
- `Form1.cs` - key generation logic
- `Form1.Designer.cs` - form layout and controls

## How to run

### Requirements

- Windows
- Visual Studio 2013 or later
- DevExpress references matching the project version

### Run in Visual Studio

1. Open `TinnitusTrioOfflineKeyGenerator.sln`.
2. Restore references if needed.
3. Set the WinForms project as startup.
4. Run the application.
5. Paste the popup key, click the generate button, and copy the displayed short key.

### Build

```bat
msbuild TinnitusTrioOfflineKeyGenerator.sln /t:Build /p:Configuration=Debug
```

## Legacy notes

- The tool is intentionally narrow in scope.
- The generated key is deterministic for a given input because it is derived from the input hash.

For the exact button/control behavior and derivation flow, see `TECHNICAL_DETAILS.md`.
