# AndroidBuildingMusicPlayerAndroidStudio

## Overview

This folder contains a legacy Android Studio project for **Composite Modulated Music (CMM)**. It is a local music-player app based on `MediaPlayer`, with playlist browsing, playback controls, repeat/shuffle support, seek tracking, timed sessions, SQLite logging, and a legacy server-based activation/sync flow.

## Tech stack

| Area | Details |
| --- | --- |
| Language | Java |
| Android plugin | `com.android.tools.build:gradle:1.3.0` |
| Gradle wrapper | `gradle-2.4-all.zip` |
| Compile SDK | 23 |
| Build tools | 23.0.1 |
| Min SDK | 16 in Gradle, `18` in `AndroidManifest.xml` |
| Target SDK | 23 |
| UI base | Android framework `Activity` + XML layouts |
| Playback | `android.media.MediaPlayer` |
| HTTP support | `useLibrary 'org.apache.http.legacy'` + local Apache HTTP JARs |
| Local persistence | SQLite database on external storage |

## Project structure

```text
AndroidBuildingMusicPlayerAndroidStudio/
├── app/
│   ├── build.gradle
│   ├── libs/
│   │   ├── apache-httpcomponents-httpclient.jar
│   │   └── apache-httpcomponents-httpcore.jar
│   └── src/main/
│       ├── AndroidManifest.xml
│       ├── assets/songs/sample.mp3
│       ├── java/com/androidhive/musicplayer/
│       │   ├── AndroidBuildingMusicPlayerActivity.java
│       │   ├── PlayListActivity.java
│       │   ├── SongsManager.java
│       │   ├── Utilities.java
│       │   ├── databasehandler.java
│       │   ├── DatabaseLogger.java
│       │   ├── DatabaseExporter.java
│       │   ├── GetService.java
│       │   └── sqlitesync.java
│       └── res/
│           ├── layout/player.xml
│           ├── layout/playlist.xml
│           └── drawable-hdpi/...
├── import-summary.txt
├── build.gradle
├── gradle/wrapper/gradle-wrapper.properties
└── settings.gradle
```

## Main components

- `AndroidBuildingMusicPlayerActivity.java`: launcher activity and primary playback controller.
- `PlayListActivity.java`: list screen for discovered audio files.
- `SongsManager.java`: scans external storage for `.mp3` content.
- `databasehandler.java`: activation/security database wrapper.
- `DatabaseLogger.java`: session and track logging.
- `DatabaseExporter.java`: exports logged rows for upload.
- `GetService.java`: legacy HTTP client for activation, log sync, and deactivation checks.
- `import-summary.txt`: indicates the project was migrated from an older Eclipse/ADT layout.

## Runtime behavior

### Audio discovery

The app loads MP3 files from external storage, not from the packaged sample asset. `SongsManager` checks:

1. `SECONDARY_STORAGE/tinnitustrio/cmm/music`
2. `EXTERNAL_STORAGE/tinnitustrio/cmm/music`

The asset `app/src/main/assets/songs/sample.mp3` exists in the project, but the current runtime path resolution uses external storage.

### Playback flow

- `PlayListActivity` shows available tracks from `SongsManager`.
- `AndroidBuildingMusicPlayerActivity` owns the `MediaPlayer`, seekbar, play/pause, next/previous, repeat, shuffle, and timer spinner.
- `playSong(int songIndex)` resets the player, loads the file path with `setDataSource(...)`, starts playback, and updates the UI.
- `onCompletion(...)` handles repeat/shuffle/advance logic.
- A spinner lets the user choose **No Limit** or a fixed number of minutes.

### Activation and logging

This project has a more active legacy activation path than the environmental-sounds variant:

- User/device data is stored in `/sdcard/TinnitusTrio/securityManager`
- `security` table stores activation details
- `CMMLogger` stores playback logs
- `GetService` posts to:
  - `/insertpatient`
  - `/synccmmdata`
  - `/checkactivestatus`

Base service URL in code:

```text
http://tinnitustrioservice-dev.elasticbeanstalk.com/Service1.svc
```

## Permissions

Declared in `AndroidManifest.xml`:

- `READ_EXTERNAL_STORAGE`
- `WRITE_EXTERNAL_STORAGE`
- `READ_PHONE_STATE`
- `ACCESS_WIFI_STATE`
- `INTERNET`
- `ACCESS_NETWORK_STATE`

Because the code predates modern Android privacy changes, serial number and MAC-address collection may fail or return empty/placeholder values on recent devices.

## Resources

Notable UI resources:

- `res/layout/player.xml`: player screen
- `res/layout/playlist.xml`: playlist view
- `res/layout/playlist_item.xml`: row view
- `res/layout/bg_player_header.xml` and `bg_player_footer.xml`: shared themed sections
- `res/drawable-hdpi/*`: button states, seekbar visuals, and background images

## How to run

### Recommended environment

Use a legacy-compatible Android setup:

1. **JDK 8**
2. Android SDK Platform **23**
3. Android Build Tools **23.0.1**
4. An Android Studio version that can work with AGP 1.3.0, or be prepared to migrate Gradle files first

### Run in Android Studio

1. Open Android Studio.
2. Open the `AndroidBuildingMusicPlayerAndroidStudio` folder.
3. Allow Gradle sync to complete.
4. Install missing API 23 / Build Tools 23.0.1 packages if prompted.
5. Copy MP3 files to:
   - `/sdcard/tinnitustrio/cmm/music`
   - or the equivalent secondary-storage path
6. Run the `app` module on a device or emulator.

### Run from the command line

```bash
cd AndroidBuildingMusicPlayerAndroidStudio
./gradlew assembleDebug
```

Install the output:

```bash
adb install -r app/build/outputs/apk/app-debug.apk
```

## Known compatibility notes

- Uses `jcenter()` and very old Gradle/AGP versions.
- Depends on legacy Apache HTTP APIs.
- Expects unrestricted external storage access.
- The remote activation/sync backend may no longer be online.
- Device identifier access is based on APIs that have changed significantly since Android 6+.

## Summary

This is a **legacy external-storage music player** with activation and logging features wired into an old backend service. The core playback path is local and straightforward, but the project runs best in an Android SDK 23-era environment with MP3 files placed in the expected storage directory.
