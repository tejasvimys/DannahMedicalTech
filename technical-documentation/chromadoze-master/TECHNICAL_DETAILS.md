# Technical details: chromadoze-master

## Purpose

`chromadoze-master` is a legacy Android app for generating **colored noise from a user-shaped frequency spectrum**. Instead of loading audio files, it lets the user sculpt a spectrum in the UI and then synthesizes noise from that spectrum in the background.

At a high level, the app is designed to:

1. let the user shape noise visually
2. turn that spectrum into audio blocks
3. keep audio running in a foreground service
4. persist presets and options

## Conceptual model

The main conceptual pipeline is:

**user edits spectrum -> spectrum becomes `Phonon`/`SpectrumData` -> `UIState` sends it to `NoiseService` -> `SampleGenerator` runs inverse DCT -> generated chunks are shuffled and played**

This project is more architectural than the other folders. It is not just one activity with one player object.

## Main UI structure

The app is organized around one activity and several fragments.

### `ChromaDoze.java`

Acts as:

- activity host
- toolbar owner
- fragment navigator
- menu handler
- persistence entry point
- playback start/stop bridge

### Fragment navigation

The toolbar contains a spinner (`nav_spinner`) that switches between fragments defined through `FragmentIndex`.

Main destinations:

- **Main / Chroma Doze**
- **Options**
- **Memory**
- **About**

## Screen-by-screen behavior

### 1. Main fragment

`MainFragment.java`

Purpose:

- display the equalizer surface
- show generation progress while audio chunks are being prepared

Key widgets:

- `EqualizerView`
- `StateText`
- `PercentBar`

What happens here:

1. the user edits the spectrum with the equalizer-like UI
2. `EqualizerView` updates `UIState`
3. `UIState.sendIfDirty()` pushes changes toward the service
4. while new blocks are being generated, progress events come back from `NoiseService`

### 2. Options fragment

`OptionsFragment.java`

Purpose:

- expose playback behavior settings

Settings controlled here:

- minimum volume
- period
- autoplay
- ignore audio focus
- volume limit

Each change updates `UIState`, and `UIState` decides when to send the updated state to the service.

### 3. Memory fragment

`MemoryFragment.java`

Purpose:

- manage saved presets

What it supports:

- save current scratch preset
- display saved presets
- select a preset as active
- reorder presets
- remove presets

This is implemented with `DragSortListView` and `MemoryArrayAdapter`.

### 4. About fragment

Provides app information and is part of the spinner-driven fragment set.

## How playback starts and stops

Playback is controlled from the activity menu, not a large play button inside the main layout.

### Menu action: Play / Stop

Handled in `ChromaDoze.onOptionsItemSelected(...)`.

If playback is not active:

1. `mUiState.sendToService()` is called
2. a chronometer is reset for logging

If playback is active:

1. `mUiState.stopService()` is called
2. elapsed time is calculated
3. a log row is inserted through `DatabaseLogger`

### Lock action

Also handled in `onOptionsItemSelected(...)`.

The lock button toggles whether the spectrum editor is locked against changes. `UIState` notifies listeners so the UI can redraw the current lock state.

## `UIState`: the central coordinator

`UIState.java` is the heart of the app.

It manages:

- lock state
- autoplay
- ignore-audio-focus setting
- volume limit
- current editable preset (`mScratchPhonon`)
- saved presets (`mSavedPhonons`)
- active preset selection (`mActivePos`)

### Why `UIState` matters

It is the bridge between:

- fragments and user interaction
- stored preferences
- the background playback service

### Key methods

- `loadState(...)`: restore options and saved phonons from `SharedPreferences`
- `saveState(...)`: persist them
- `sendToService()`: package the current `Phonon` into an `Intent` and start/update `NoiseService`
- `sendIfDirty()`: only pushes updates when something changed
- `stopService()`: stops playback via `NoiseService.sendStopIntent(...)`

## Audio engine internals

### `NoiseService`

This is the runtime audio owner.

Responsibilities:

- stay alive as a foreground service
- hold a wake lock
- receive spectrum and option updates
- coordinate sample generation and playback
- expose progress updates to the UI

`onStartCommand(...)` receives:

- current spectrum
- minimum volume wave settings
- period
- volume limit
- ignore-audio-focus flag

It applies synchronous state updates, then tells `SampleGenerator` to rebuild audio chunks if needed.

### `SampleGenerator`

This class runs a worker thread that:

1. waits for a pending spectrum
2. builds DCT-domain data
3. fills it from `SpectrumData`
4. multiplies it by random white-noise values
5. runs `FloatDCT_1D.inverse(...)`
6. hands generated chunks to `SampleShuffler`

This is the most important algorithmic part of the app.

### Why inverse DCT is used

The user edits a spectrum-like shape, not a waveform.

So the app:

1. represents the desired energy across frequency bands
2. builds an array in that transformed domain
3. applies inverse DCT to get time-domain samples

That is how the visual spectrum becomes audible colored noise.

### `SampleShuffler`

Receives generated chunks and manages steady-state playback behavior. The app does not simply loop one raw block; it works with multiple generated blocks and crossfades/shuffles them to avoid obvious repetition.

### `AudioFocusHelper`

Controls integration with Android audio focus, depending on the current `ignoreAudioFocus` setting.

## Preset system

Presets are represented by `Phonon` / `PhononMutable`.

### Scratch preset

`mScratchPhonon` is the editable working preset.

### Saved presets

`mSavedPhonons` contains saved versions.

### Memory workflow

1. user edits the scratch preset
2. user opens Memory
3. user saves the current configuration
4. the saved preset can later be selected, reordered, or deleted

The app serializes presets as JSON strings inside `SharedPreferences`.

## Activity lifecycle behavior

### On create

`ChromaDoze.onCreate(...)`:

1. loads saved `UIState`
2. configures the toolbar
3. populates the navigation spinner
4. inserts the initial `MainFragment`
5. creates an extra SQLite table `ChromaDoze_Bars` from custom project modifications

### On resume

1. registers as a `NoiseService.PercentListener`
2. registers as a `UIState.LockListener`
3. optionally starts playback automatically if autoplay is enabled

### On pause

1. stops playback if the current phonon is silent
2. saves `UIState` to `SharedPreferences`
3. unregisters listeners

## Local persistence

### Primary app state

Stored in `SharedPreferences` through `UIState.saveState(...)` and `loadState(...)`.

### Additional project-specific SQLite additions

This repo also contains extra storage code not central to the original Chroma Doze design:

- `databasehandler.java`
- `DatabaseLogger.java`
- `DatabaseExporter.java`
- `GetService.java`
- `MySQLite.java`
- `ChromaDoze_Bars` table creation in `ChromaDoze.java`

These additions appear to support logging, activation, and custom bar-state storage.

## Logging and remote integration

Like the other folders, this project includes legacy device/user activation and session logging support.

Data is stored under:

```text
/sdcard/TinnitusTrio/securityManager
```

Important logging/security tables:

- `security`
- `cmnLogger`

Remote operations include:

- inserting user details
- syncing logger data
- checking active status

That functionality is separate from the core noise-generation engine, but it has been integrated into the app lifecycle and play/stop flow.

## Important classes

| Class | Responsibility |
| --- | --- |
| `ChromaDoze` | Activity host, menu flow, navigation, persistence integration |
| `MainFragment` | Equalizer screen and progress UI |
| `OptionsFragment` | Playback/settings editor |
| `MemoryFragment` | Preset save/reorder/remove screen |
| `UIState` | Central state and service bridge |
| `NoiseService` | Foreground playback service |
| `SampleGenerator` | Worker that builds audio chunks from spectrum data |
| `SampleShuffler` | Chunk management for playback |
| `SpectrumData` | Frequency-band representation |
| `Phonon` / `PhononMutable` | Preset/state objects |
| `AudioFocusHelper` | Audio-focus integration |

## Why this codebase is different from the others

Compared with the MP3-player projects and tone generator, this app is more layered:

- UI logic is split across fragments
- playback is owned by a service, not directly by the activity
- state is modeled explicitly in `UIState`
- audio is synthesized through a transform-based algorithm rather than file playback or direct tone generation

That makes it the most technically interesting project in the set.

## Key limitations

- Old support-library / pre-AndroidX structure
- Legacy APIs and Gradle setup
- Very verbose logging throughout the code
- Extra custom SQLite code that is only partially integrated
- Legacy activation/network assumptions

## Practical mental model

The simplest accurate description is:

**this app is a spectrum editor connected to a foreground noise-synthesis engine**
