# environmentalSounds

## Overview

This folder contains a legacy Android Studio project for **Composite Modulated Environmental Sounds (CMES)**. The app is a local audio player built around `MediaPlayer`, with playlist browsing, repeat/shuffle playback, seek controls, and a session timer. It is tailored to play environmental sound tracks stored on external storage rather than packaged app assets.

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
| Networking | Apache HTTP client JARs in `app/libs/` |
| Local persistence | SQLite database on external storage |

## Project structure

```text
environmentalSounds/
├── app/
│   ├── build.gradle
│   ├── libs/
│   │   ├── apache-httpcomponents-httpclient.jar
│   │   └── apache-httpcomponents-httpcore.jar
│   └── src/main/
│       ├── AndroidManifest.xml
│       ├── java/com/example/tejasvi/environmentalsounds/
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
│           └── drawable-*/...
├── build.gradle
├── gradle/wrapper/gradle-wrapper.properties
└── settings.gradle
```

## Main components

- `AndroidBuildingMusicPlayerActivity.java`: launcher activity, owns the `MediaPlayer`, timer spinner, playback buttons, repeat/shuffle state, seek bar updates, logging, and optional sync/deactivation flow.
- `PlayListActivity.java`: shows the discovered audio files in a `ListActivity` and returns the selected item index to the main player activity.
- `SongsManager.java`: scans external storage for `.mp3` files.
- `databasehandler.java`: stores security/activation data in a SQLite database located under external storage.
- `DatabaseLogger.java`: records playback events to SQLite.
- `DatabaseExporter.java`: reads logged rows and converts them into JSON for upload.
- `GetService.java`: calls the legacy backend endpoints hosted at `http://tinnitustrioservice-dev.elasticbeanstalk.com/Service1.svc`.

## Runtime behavior

### Audio source

The app does **not** use bundled demo audio. It scans these directories for MP3 files:

1. `SECONDARY_STORAGE/tinnitustrio/cmm/environmental`
2. `EXTERNAL_STORAGE/tinnitustrio/cmm/environmental` as fallback

If neither path exists or contains MP3 files, the playlist will be empty.

### Playback flow

- `PlayListActivity` loads the playlist from `SongsManager`.
- The selected file path is passed back to `AndroidBuildingMusicPlayerActivity`.
- `playSong(int songIndex)` resets the `MediaPlayer`, calls `setDataSource(...)`, starts playback, updates labels, and advances the seek bar.
- Repeat and shuffle are implemented in `onCompletion(...)`.
- A spinner supplies a session limit from **No Limit** to **60** minutes.

### Data and logging

The code writes a database named `securityManager` under:

```text
/sdcard/TinnitusTrio/securityManager
```

Important tables:

- `security`: activation/user data
- `CMESLogger`: playback log rows

The app can export logs through `DatabaseExporter` and sync them to the legacy service via `GetService.InsertLogDetails(...)`.

## Permissions

Declared in `AndroidManifest.xml`:

- `READ_EXTERNAL_STORAGE`
- `WRITE_EXTERNAL_STORAGE`
- `READ_PHONE_STATE`
- `ACCESS_WIFI_STATE`
- `INTERNET`
- `ACCESS_NETWORK_STATE`

This is legacy pre-runtime-permission code. On newer Android versions, storage, MAC address, and serial number access may not behave as originally expected.

## Resources

Notable UI resources:

- `res/layout/player.xml`: main player screen
- `res/layout/playlist.xml`: playlist list screen
- `res/layout/playlist_item.xml`: row layout
- `res/layout/bg_player_header.xml` and `bg_player_footer.xml`: shared visual sections
- `res/drawable-hdpi/*`: playback buttons, seekbar assets, and background art

## How to run

### Recommended environment

Because this project uses **Android Gradle Plugin 1.3.0**, **Gradle 2.4**, old Apache HTTP APIs, and `jcenter()`, the safest setup is:

1. **JDK 8**
2. An older Android Studio release, or a modern Studio instance only after migrating the Gradle setup
3. Android SDK Platform **23**
4. Android Build Tools **23.0.1**

### Run in Android Studio

1. Open Android Studio.
2. Choose **Open** and select the `environmentalSounds` folder.
3. Let Gradle sync.
4. Install any missing SDK components for API 23 / Build Tools 23.0.1.
5. Put MP3 files in one of the expected external-storage folders:
   - `/sdcard/tinnitustrio/cmm/environmental`
   - or the device's secondary storage equivalent
6. Run the `app` configuration on a connected device or emulator.

### Run from the command line

```bash
cd environmentalSounds
./gradlew assembleDebug
```

If the build succeeds, install the APK:

```bash
adb install -r app/build/outputs/apk/app-debug.apk
```

## Known compatibility notes

- The build uses `jcenter()`, which is legacy infrastructure.
- The code expects direct file access on external storage and does not implement modern scoped-storage handling.
- It relies on device identifiers (`Build.SERIAL`, Wi-Fi MAC address) that are restricted on newer Android releases.
- Several activation/sync paths depend on an old Elastic Beanstalk service that may no longer be reachable.

## Summary

This project is best understood as a **legacy Android MP3 environmental-sound player** with playback logging and optional remote sync. To run it successfully, use an Android SDK 23-era toolchain and provide MP3 files in the expected external storage folder.
