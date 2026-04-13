# Technical details: environmentalSounds

## Purpose

`environmentalSounds` is a legacy Android app for playing **environmental sound MP3 files** from external storage. Its main goal is to give the user a simple audio-player interface with playlist browsing, repeat/shuffle playback, a session timer, and playback logging.

## What the user sees

The app has two main screens:

1. **Player screen**
   - Play/pause
   - Forward / backward seek
   - Next / previous track
   - Repeat / shuffle toggles
   - Progress bar and current/total duration labels
   - A timer spinner from `No Limit` to `60`
2. **Playlist screen**
   - A simple list of discovered MP3 files

## High-level flow

1. `AndroidBuildingMusicPlayerActivity` starts.
2. It creates a `MediaPlayer`, loads the playlist from `SongsManager`, and wires all button listeners.
3. The user opens the playlist screen.
4. `PlayListActivity` asks `SongsManager` for MP3 files and displays them in a `ListActivity`.
5. When the user taps a track, the selected index is returned to the main activity.
6. `playSong(songIndex)` loads the file path into `MediaPlayer`, starts playback, updates the labels, and starts periodic progress updates.
7. While audio is playing:
   - the seekbar is updated every 100 ms
   - repeat/shuffle influence what happens in `onCompletion(...)`
   - optional timer shutdown can sync logs and exit the app

## How audio discovery works

`SongsManager.java` is responsible for finding audio files.

It checks two folders in order:

1. `SECONDARY_STORAGE/tinnitustrio/cmm/environmental`
2. `EXTERNAL_STORAGE/tinnitustrio/cmm/environmental`

Then it filters files by extension:

- `.mp3`
- `.MP3`

For each match, it builds a map with:

- `songTitle`
- `songPath`

That list is passed to `PlayListActivity` and later used by `MediaPlayer`.

## How playback works internally

### Main playback method

`AndroidBuildingMusicPlayerActivity.playSong(int songIndex)` does the core work:

1. `mp.reset()`
2. `mp.setDataSource(songPath)`
3. `mp.prepare()`
4. `mp.start()`
5. updates the displayed song title
6. switches the play button icon to pause
7. resets the seekbar
8. starts the periodic progress updater
9. logs the playback event into SQLite if the logging database exists

### Progress updates

The activity posts `mUpdateTimeTask` every 100 ms. That runnable:

1. reads total duration and current position from `MediaPlayer`
2. formats both values through `Utilities`
3. computes a percentage for the seekbar
4. re-posts itself again after 100 ms

### Seeking

- Pressing **forward** jumps ahead by `5000` ms
- Pressing **backward** jumps back by `5000` ms
- Dragging the seekbar:
  - temporarily stops progress callbacks
  - converts the visual percentage back into a playback position
  - calls `mp.seekTo(...)`
  - resumes the callback loop

### Track completion

`onCompletion(...)` implements three cases:

1. **Repeat on** -> replay the same track
2. **Shuffle on** -> choose a random track
3. **Neither** -> move to the next track, wrapping to the first song at the end

## Button-by-button behavior

### Play / Pause

- If `MediaPlayer` is already playing, pause it and switch the icon to play.
- Otherwise, resume playback and switch the icon to pause.
- Also reads the selected timer value from the spinner.

### Timer spinner

If the selected timer is not `No Limit`, the app starts a `CountDownTimer`.

When the timer ends:

1. it checks for network connectivity
2. if online, it exports logs and calls the remote sync service
3. it calls the deactivation check
4. it exits with `System.exit(0)`

### Playlist

Tapping the playlist button starts `PlayListActivity` with `startActivityForResult(...)`.

When the user selects a row:

1. the row index is packed into the result `Intent`
2. the playlist activity finishes
3. `onActivityResult(...)` in the main activity receives the index
4. the main activity calls `playSong(index)`

## Local storage and logging

The app writes to a SQLite file on external storage:

```text
/sdcard/TinnitusTrio/securityManager
```

### `databasehandler`

Stores activation/security data in table:

- `security`

### `DatabaseLogger`

Stores playback log rows in table:

- `CMESLogger`

Each playback log row includes values such as:

- date
- time
- song title
- total duration

### `DatabaseExporter`

Reads the logger table and converts the rows into a JSON payload.

## Remote service integration

`GetService.java` sends HTTP requests to the legacy service:

```text
http://tinnitustrioservice-dev.elasticbeanstalk.com/Service1.svc
```

Important operations:

- `InsertUserDetails(...)`
- `InsertLogDetails(...)`
- `DeactivateStatus(...)`

This means the app is not only a local player; it was also designed to work inside an activation + logging ecosystem.

## Important classes and their roles

| Class | Role |
| --- | --- |
| `AndroidBuildingMusicPlayerActivity` | Main controller for playback, timer, UI, and logging |
| `PlayListActivity` | Displays the song list and returns the chosen index |
| `SongsManager` | Scans the expected external-storage folders for MP3 files |
| `Utilities` | Time formatting and seekbar percentage conversion helpers |
| `databasehandler` | Security/activation database |
| `DatabaseLogger` | Playback session logger |
| `DatabaseExporter` | Converts SQLite log rows into JSON |
| `GetService` | Sends data to the legacy backend |

## Why it works the way it does

This codebase follows a very common older Android pattern:

- one big activity owns almost all UI and media logic
- playlist selection happens in a separate list activity
- local file access is done directly on external storage
- background logging/sync is triggered inline from UI code

That design makes the code easy to follow, but it also tightly couples playback, logging, timers, networking, and lifecycle handling into one class.

## Key limitations

- No modern runtime-permission flow
- Direct external-storage access only
- No background playback service; playback is activity-owned
- Timer expiration exits the process directly
- Legacy backend dependencies may no longer be available

## Practical mental model

The simplest way to think about this app is:

**scan a folder for MP3s -> show them in a list -> play the chosen file with `MediaPlayer` -> keep the seekbar updated -> optionally log and sync the session**
