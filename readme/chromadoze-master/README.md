# chromadoze-master

## Overview

This folder contains **Chroma Doze / Composite Modulated Noise**, a legacy Android app that synthesizes colored noise from a user-shaped spectrum instead of playing prerecorded files. The app presents an equalizer-style editor, persists presets, exposes playback options through fragments, and runs audio generation inside a foreground `Service`.

## Tech stack

| Area | Details |
| --- | --- |
| Language | Java |
| Android plugin | `com.android.tools.build:gradle:1.3.0` |
| Gradle wrapper | `gradle-2.2.1-all.zip` |
| Compile SDK | 22 |
| Build tools | 22.0.1 |
| Min SDK | 9 |
| Target SDK | 22 |
| UI base | `ActionBarActivity` + support fragments |
| Audio engine | Custom noise synthesis + `FloatDCT_1D` |
| Background playback | `NoiseService` foreground service |
| State storage | `SharedPreferences`, plus some SQLite helpers |
| Extra libraries | bundled source for drag-sort list support and JTransforms |

## Project structure

```text
chromadoze-master/
├── app/
│   ├── build.gradle
│   └── src/main/
│       ├── AndroidManifest.xml
│       ├── java/net/pmarks/chromadoze/
│       │   ├── ChromaDoze.java
│       │   ├── MainFragment.java
│       │   ├── OptionsFragment.java
│       │   ├── MemoryFragment.java
│       │   ├── UIState.java
│       │   ├── NoiseService.java
│       │   ├── SampleGenerator.java
│       │   ├── SampleShuffler.java
│       │   ├── SpectrumData.java
│       │   ├── EqualizerView.java
│       │   ├── Phonon.java
│       │   ├── PhononMutable.java
│       │   ├── AudioFocusHelper.java
│       │   ├── MediaButtonReceiver.java
│       │   ├── databasehandler.java
│       │   ├── DatabaseLogger.java
│       │   ├── DatabaseExporter.java
│       │   ├── GetService.java
│       │   └── ...
│       ├── java/edu/emory/mathcs/jtransforms/dct/FloatDCT_1D.java
│       ├── java/com/mobeta/android/dslv/*.java
│       └── res/
│           ├── layout/main.xml
│           ├── layout/main_fragment.xml
│           ├── layout/options_fragment.xml
│           ├── layout/memory_list*.xml
│           └── values/*.xml
├── misc/
├── simpleapp/
├── LICENSE
├── build.gradle
├── gradle/wrapper/gradle-wrapper.properties
└── settings.gradle
```

`settings.gradle` includes only `:app`, so `simpleapp/` is present in the repository but is **not** part of the active build.

## Main architecture

### Activity and fragments

- `ChromaDoze.java`: main activity, toolbar/spinner navigation host, persistence entry point, and integration layer for the app's modified activation/logging code.
- `MainFragment.java`: shows the equalizer-based spectrum editor and generation progress UI.
- `OptionsFragment.java`: controls minimum volume, period, autoplay, audio-focus behavior, and volume limiting.
- `MemoryFragment.java`: manages saved presets using a drag-sort list.

### State model

- `UIState.java`: central state container for lock state, active preset, scratch preset, autoplay, volume limit, and service communication.
- `Phonon` / `PhononMutable`: represent the current spectrum and playback settings.
- `SpectrumData`: stores the spectrum bands used for synthesis.

### Audio engine

- `NoiseService.java`: foreground service that owns long-running playback.
- `SampleGenerator.java`: background worker that fills DCT buffers and applies random noise before inverse transforming.
- `SampleShuffler.java`: shuffles/generated blocks for steady-state playback.
- `AudioFocusHelper.java`: integrates with Android audio focus.
- `MediaButtonReceiver.java`: handles media button events.

The core generation path uses `FloatDCT_1D` from the bundled JTransforms source to turn the user-defined spectrum into time-domain audio blocks.

## Runtime behavior

### UI flow

- `main.xml` hosts a toolbar plus a fragment container.
- `MainFragment` shows a horizontal progress bar while new IDCT blocks are being generated.
- The editable equalizer surface is implemented in `EqualizerView`.
- A hidden `Chronometer` is present in `main_fragment.xml`, but the visible experience centers on the equalizer and foreground playback service.

### Presets and persistence

- `UIState.saveState(...)` and `loadState(...)` store state in `SharedPreferences`.
- Saved presets are represented as serialized `Phonon` JSON.
- `MemoryFragment` lets users save, reorder, and remove presets with drag-sort support.

### Background playback

- `UIState.sendToService()` packages the active phonon into an `Intent`.
- `NoiseService.onStartCommand(...)` receives spectrum and option updates.
- The service runs in the foreground, holds a partial wake lock, and exposes a notification with a stop button on supported Android versions.

## Manifest and permissions

Declared in `AndroidManifest.xml`:

- `READ_EXTERNAL_STORAGE`
- `WRITE_EXTERNAL_STORAGE`
- `READ_PHONE_STATE`
- `ACCESS_WIFI_STATE`
- `INTERNET`
- `ACCESS_NETWORK_STATE`
- `WAKE_LOCK`

Registered components:

- launcher activity: `net.pmarks.chromadoze.ChromaDoze`
- receiver: `MediaButtonReceiver`
- services: `NoiseService`, `MySqliteService`

## Local data and legacy additions

This codebase contains extra activation/logging infrastructure beyond the original upstream Chroma Doze design:

- `databasehandler.java`: security table stored under `/sdcard/TinnitusTrio/securityManager`
- `DatabaseLogger.java`: `cmnLogger` table for session logging
- `DatabaseExporter.java`: export path for log upload
- `GetService.java`: legacy remote activation/logging client

There is also partially implemented SQLite code in `MySQLite.java` and additional SQL statements embedded in `ChromaDoze.java`, including a `ChromaDoze_Bars` table definition.

## Resources

Notable resource files:

- `res/layout/main.xml`: toolbar + fragment host
- `res/layout/main_fragment.xml`: progress bar and `EqualizerView`
- `res/layout/options_fragment.xml`: playback/settings screen
- `res/layout/memory_list*.xml`: preset management UI
- `res/layout-v11/notification_with_stop_button.xml`: foreground-service notification extension
- `res/values/themes.xml`, `colors.xml`, `strings.xml`: app theming and labels

## How to run

### Recommended environment

This is a pre-AndroidX, pre-modern-Gradle Android app. The safest setup is:

1. **JDK 8**
2. Android SDK Platform **22**
3. Android Build Tools **22.0.1**
4. An Android Studio version compatible with AGP 1.3.0, or a migrated Gradle setup

### Run in Android Studio

1. Open Android Studio.
2. Open the `chromadoze-master` folder.
3. Let Gradle sync.
4. Install missing API 22 / Build Tools 22.0.1 packages if prompted.
5. Run the `app` module on a device or emulator.
6. Use the equalizer screen to shape the spectrum, then start playback.

No external audio files are required; the app synthesizes audio internally.

### Run from the command line

```bash
cd chromadoze-master
./gradlew assembleDebug
```

Install the APK:

```bash
adb install -r app/build/outputs/apk/app-debug.apk
```

## Screenshots

<img src="misc/screenshot1.png?raw=true" width="360px">
<img src="misc/screenshot2.png?raw=true" width="360px">
<img src="misc/screenshot3.png?raw=true" width="360px">

## Known compatibility notes

- Uses very old support libraries and `jcenter()`.
- The project predates AndroidX and modern scoped-storage/runtime-permission patterns.
- The modified activation/logging code depends on legacy device identifiers and old HTTP endpoints.
- `ActionBarActivity` and several APIs used here are obsolete in current Android development.

## Summary

This project is a **noise-synthesis Android app** with a richer architecture than the other folders: fragment-based UI, service-driven playback, spectrum-to-audio generation with inverse DCT, preset storage, and extra legacy activation/logging additions. It is self-contained for audio generation and does not require external media files to run.
