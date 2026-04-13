# Technical details: AndroidBuildingMusicPlayerAndroidStudio

## Purpose

This project is a legacy Android **music player** for local MP3 files stored on external storage. Beyond basic playback, it also includes an older activation workflow, local SQLite logging, timed sessions, and HTTP-based log sync.

## What the app is trying to do

At a product level, the app combines three concerns:

1. **play local music**
2. **track sessions and usage**
3. **validate/deactivate usage through a remote service**

So it is not just a demo music player. It is a music player wrapped in an activation and logging layer.

## Main user flow

1. The app launches into `AndroidBuildingMusicPlayerActivity`.
2. It initializes all playback controls and loads the song list from external storage.
3. The user opens the playlist view.
4. `PlayListActivity` shows the available MP3 files.
5. The selected song index is returned to the main activity.
6. `playSong(...)` loads the file into `MediaPlayer` and starts playback.
7. The seekbar and duration labels keep updating while the song plays.
8. When playback ends:
   - repeat may restart the same song
   - shuffle may pick a random song
   - otherwise the next song plays
9. If a timer was selected, the app can sync logs, call deactivation, and terminate when the timer ends.

## How track discovery works

`SongsManager.java` looks for MP3 files in:

1. `SECONDARY_STORAGE/tinnitustrio/cmm/music`
2. `EXTERNAL_STORAGE/tinnitustrio/cmm/music`

For each file, it stores:

- `songTitle`
- `songPath`

Even though the repo contains `assets/songs/sample.mp3`, runtime playback uses the external-storage scan, not the asset.

## Screen-by-screen explanation

### 1. Main player screen

Owned by `AndroidBuildingMusicPlayerActivity`.

Controls:

- play/pause
- forward 5 seconds
- backward 5 seconds
- next
- previous
- playlist
- repeat toggle
- shuffle toggle
- seekbar
- current/total duration labels
- timer spinner

### 2. Playlist screen

Owned by `PlayListActivity`.

What it does:

1. asks `SongsManager` for the playlist
2. copies those rows into a list adapter
3. displays them using `SimpleAdapter`
4. returns the selected index to the main activity

## Playback internals

### `playSong(int songIndex)`

This is the core playback method.

It performs:

1. `mp.reset()`
2. `mp.setDataSource(songPath)`
3. `mp.prepare()`
4. `mp.start()`
5. update visible song title
6. switch play icon to pause
7. zero and configure the seekbar
8. start the periodic UI updater
9. append a playback record to SQLite if the logging DB exists

### Progress tracking

`updateProgressBar()` posts `mUpdateTimeTask`.

That task runs every 100 ms and:

1. reads current and total duration
2. formats duration strings through `Utilities`
3. computes playback percentage
4. updates the seekbar
5. posts itself again

### Manual seeking

When the user drags the seekbar:

- callbacks are temporarily paused
- the selected percentage is converted back into milliseconds
- `MediaPlayer.seekTo(...)` is called
- progress updates resume

### End-of-track behavior

`onCompletion(...)` implements:

1. repeat current song if repeat is enabled
2. pick a random song if shuffle is enabled
3. otherwise advance to the next song with wrap-around

## Timer behavior

Pressing play also checks the selected spinner value.

If it is not `No Limit`, the code creates a `CountDownTimer`.

When the timer finishes:

1. it checks network connectivity
2. calls `ConvertSQLtoXML()`
3. calls `DeActivateSystem()`
4. exits the app

This means timer handling is coupled directly to the play button, not to a dedicated session manager.

## Logging and activation flow

### Security database

`databasehandler.java` manages table:

- `security`

Stored in:

```text
/sdcard/TinnitusTrio/securityManager
```

This database holds activation-related user/device metadata.

### Playback logger

`DatabaseLogger.java` manages:

- `CMMLogger`

Rows are inserted when `playSong(...)` starts playback.

### Export and sync

`DatabaseExporter.java` reads logged rows and builds JSON.

`GetService.java` posts to:

- `insertpatient`
- `synccmmdata`
- `checkactivestatus`

Base URL:

```text
http://tinnitustrioservice-dev.elasticbeanstalk.com/Service1.svc
```

### Deactivation path

`DeActivateSystem()` asks the backend whether the user remains active.

If the service reports deactivation, the app deletes the `TinnitusTrio` directory from external storage.

## Important classes

| Class | Responsibility |
| --- | --- |
| `AndroidBuildingMusicPlayerActivity` | UI logic, playback, timer, logging, activation handling |
| `PlayListActivity` | Song selection UI |
| `SongsManager` | File-system scan for MP3s |
| `Utilities` | Time/progress math |
| `databasehandler` | Activation/security SQLite table |
| `DatabaseLogger` | Playback log table |
| `DatabaseExporter` | JSON export of logs |
| `GetService` | HTTP bridge to the old backend |

## Architecture style

This app uses an older Android pattern:

- a single, very large activity owns most behavior
- storage and networking are called directly from UI code
- `MediaPlayer` is activity-scoped
- playlist selection is handled by a second activity via `startActivityForResult(...)`

That makes the execution path straightforward:

**UI event -> activity method -> media/log/network side effect**

## Key limitations

- No service-based background audio architecture
- No modern lifecycle-safe playback abstraction
- Legacy external-storage assumptions
- Legacy identifier collection
- Tight coupling between playback and network logic
- Old backend dependency

## Practical mental model

Think of this app as:

**a local MP3 player whose playback events are recorded to SQLite and optionally synced to an activation server**
