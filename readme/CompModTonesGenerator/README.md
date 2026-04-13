# CompModTonesGenerator

## Overview

This folder contains a legacy Android Studio project for a **Composite Modulated Tones Generator (CMT)**. Instead of playing existing media files, it synthesizes tones in real time with `AudioTrack`, calculates four derived frequencies from a user-entered base frequency, and plays them through the left channel, right channel, or both channels with a configurable timer.

## Tech stack

| Area | Details |
| --- | --- |
| Language | Java |
| Android plugin | `com.android.tools.build:gradle:1.3.0` |
| Gradle wrapper | `gradle-2.4-all.zip` |
| Compile SDK | 23 |
| Build tools | 23.0.1 |
| Min SDK | 16 |
| Target SDK | 23 |
| UI base | `AppCompatActivity` + XML layouts |
| Audio synthesis | `android.media.AudioTrack` |
| Randomization | `ThreadLocalRandom` in the tone shuffle logic |
| Local persistence | SQLite on external storage |
| Networking | Apache HTTP JARs + JSON/XML helpers |

## Project structure

```text
CompModTonesGenerator/
├── app/
│   ├── build.gradle
│   ├── libs/
│   │   ├── apache-httpcomponents-httpclient.jar
│   │   ├── apache-httpcomponents-httpcore.jar
│   │   └── json-jena-1.0.jar
│   └── src/main/
│       ├── AndroidManifest.xml
│       ├── java/com/example/tejasvi/compmodtonesgenerator/
│       │   ├── MainActivity.java
│       │   ├── AudioGenerator.java
│       │   ├── AudioGeneratorLeft.java
│       │   ├── AudioGeneratorRight.java
│       │   ├── Metronome.java
│       │   ├── Utilities.java
│       │   ├── databasehandler.java
│       │   ├── DatabaseLogger.java
│       │   ├── DatabaseExporter.java
│       │   ├── GetService.java
│       │   └── sqlitesync.java
│       └── res/
│           ├── layout/activity_main.xml
│           ├── menu/menu_main.xml
│           └── drawable*/...
├── Keyfile.jks
├── build.gradle
├── gradle/wrapper/gradle-wrapper.properties
└── settings.gradle
```

## Main components

- `MainActivity.java`: launcher activity, UI controller, session timer, security flow, and tone-generation orchestration.
- `AudioGenerator.java`: mono/both-channel tone synthesis using `AudioTrack`.
- `AudioGeneratorLeft.java`: left-channel-only playback by setting stereo volume to `(100, 0)`.
- `AudioGeneratorRight.java`: right-channel-only playback by setting stereo volume to `(0, 100)`.
- `Metronome.java`: additional tone helper class.
- `databasehandler.java`, `DatabaseLogger.java`, `DatabaseExporter.java`, `GetService.java`: activation, logging, export, and remote sync infrastructure.

## Tone-generation logic

The activity calculates four frequencies from the base input:

- `Freq1 = floor(base * 0.773 - 44.5)`
- `Freq2 = floor(base * 0.903 - 21.5)`
- `Freq3 = floor(base * 1.09 + 52)`
- `Freq4 = floor(base * 1.395 + 26.5)`

Playback details from the code:

- Sample rate: **44100 Hz**
- Note duration: **7500 samples**
- Short silence inserted between tones
- Longer silence inserted every third pass
- Tone order is shuffled with a Fisher-Yates style shuffle
- Output modes:
  - both ears: `generateTones()`
  - left only: `generateleftTones()`
  - right only: `generaterightTones()`

## UI behavior

The main screen (`activity_main.xml`) includes:

- base frequency input
- **Set Frequency** button
- timer spinner
- left / right / both playback buttons
- stop button
- chronometer
- text fields that reveal the four calculated frequencies

If the user leaves the frequency field empty, the code defaults to **500 Hz**.

## External files and data

### Optional XML input

`MainActivity` contains logic to read:

```text
SECONDARY_STORAGE/tinnitustrio/cmt/Freq.xml
```

The parser expects a `<Value>` element and can load a base frequency from XML, but the app also supports manual entry directly in the UI.

### SQLite and logging

The project stores its database under:

```text
/sdcard/TinnitusTrio/securityManager
```

Important tables:

- `security`
- `cmtLogger`

### Remote sync

The code includes a legacy HTTP client for:

- user activation
- session log upload
- active-status checking

That flow uses the old Elastic Beanstalk service referenced in `GetService.java`.

## Permissions

Declared in `AndroidManifest.xml`:

- `READ_EXTERNAL_STORAGE`
- `WRITE_EXTERNAL_STORAGE`
- `READ_PHONE_STATE`
- `ACCESS_WIFI_STATE`
- `INTERNET`
- `ACCESS_NETWORK_STATE`

## Build and runtime notes

- The Gradle files are legacy and still use `compile` dependencies.
- `json-jena-1.0.jar` is bundled locally.
- `ThreadLocalRandom.current()` is used in `shuffleArray(...)`, which is more naturally aligned with newer Android runtimes than the declared `minSdkVersion`.
- A keystore file (`Keyfile.jks`) exists in the project root, but the current Gradle scripts do not wire a signing configuration to it.

## How to run

### Recommended environment

1. **JDK 8**
2. Android SDK Platform **23**
3. Android Build Tools **23.0.1**
4. An Android Studio version that can sync AGP 1.3.0 projects, or a migrated Gradle setup

### Run in Android Studio

1. Open Android Studio.
2. Open the `CompModTonesGenerator` folder.
3. Let Gradle sync.
4. Install missing API 23 / Build Tools 23.0.1 packages if needed.
5. Run the `app` module on a device or emulator.
6. Enter a base frequency or rely on the default `500`.
7. Tap **Set Frequency**, then choose left, right, or both-channel playback.

Optional:

- If you want XML-driven frequency input, place `Freq.xml` in `/sdcard/tinnitustrio/cmt/`.

### Run from the command line

```bash
cd CompModTonesGenerator
./gradlew assembleDebug
```

Install the APK:

```bash
adb install -r app/build/outputs/apk/app-debug.apk
```

## Known compatibility notes

- Uses old Gradle/AGP and `jcenter()`.
- Tone generation uses deprecated audio constants such as `CHANNEL_CONFIGURATION_MONO`.
- The activation/logging code depends on device identifiers and a legacy HTTP backend.
- External storage assumptions predate scoped storage and modern permission behavior.

## Summary

This project is a **real-time composite-tone synthesizer** with left/right/binaural playback modes, timer-based sessions, and legacy activation/logging support. Its core functionality is local audio synthesis, and it runs best in an Android SDK 23-era environment.
