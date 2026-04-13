# Technical details: CompModTonesGenerator

## Purpose

`CompModTonesGenerator` is a legacy Android app for generating **composite modulated tones** in real time. Instead of reading audio from files, it computes frequencies from a base input and synthesizes PCM data on demand with `AudioTrack`.

Its practical goal is to let a user:

1. enter a base frequency
2. derive four related frequencies
3. play those frequencies in a repeating randomized pattern
4. choose whether audio goes to the left ear, right ear, or both
5. stop automatically after a chosen timer interval

## What the user sees

The main screen contains:

- a base-frequency text field
- a **Set Frequency** button
- a timer spinner
- playback buttons:
  - left
  - right
  - both
  - stop
- a chronometer
- text labels showing the four generated frequencies

There is only one main activity, and almost all logic lives there.

## Core idea

The app converts a single base frequency into four playback frequencies:

- `Freq1 = floor(base * 0.773 - 44.5)`
- `Freq2 = floor(base * 0.903 - 21.5)`
- `Freq3 = floor(base * 1.09 + 52)`
- `Freq4 = floor(base * 1.395 + 26.5)`

Those four tones are then played in a shuffled order, separated by silence, in a loop.

## User flow

1. `MainActivity` starts and initializes the UI.
2. The user enters a base frequency. If left blank, the app defaults to `500`.
3. Tapping **Set Frequency** runs `generateFrequencies(...)`.
4. The four calculated frequencies are shown on screen.
5. The user chooses:
   - both-channel playback
   - left-channel playback
   - right-channel playback
6. The app starts a worker thread and begins writing PCM audio to an `AudioTrack`.
7. The chronometer runs during playback.
8. If the timer spinner was set to a finite value, a `CountDownTimer` ends the session automatically.
9. Tapping **Stop** interrupts playback, logs the session, and restores the buttons.

## How synthesis works

### Audio classes

- `AudioGenerator`: both channels
- `AudioGeneratorLeft`: left only
- `AudioGeneratorRight`: right only

Each class:

1. creates a sine wave array for a given frequency
2. converts the samples to 16-bit PCM bytes
3. creates an `AudioTrack`
4. streams PCM bytes into the track

### Sample generation

The synthesis path is:

1. call `getSineWave(samples, sampleRate, frequency)`
2. get a `double[]` containing the sine wave
3. convert it through `get16BitPcm(...)`
4. write it to `AudioTrack.write(...)`

### Sample settings

From the code:

- sample rate: `44100`
- note duration: `7500`
- short silence: generated as a sine wave at frequency `0`
- long silence: generated after every third loop cycle

### Output channel selection

The app does not use different channel buffer formats for left/right. Instead, it creates a mono `AudioTrack` and changes stereo volume:

- left only -> `setStereoVolume(100, 0)`
- right only -> `setStereoVolume(0, 100)`
- both -> `setStereoVolume(volume, volume)`

## Playback algorithms

### Both-channel mode

`generateTones()`:

1. recalculates the four frequencies
2. creates four sine-wave note arrays
3. creates an `AudioTrack`
4. shuffles the order array `[1, 2, 3, 4]`
5. writes each note plus a short silence
6. every third pass, inserts a longer silence
7. keeps looping until `loopBreak == true`

### Left-channel mode

`generateleftTones()` follows the same pattern using `AudioGeneratorLeft`.

### Right-channel mode

`generaterightTones()` follows the same pattern using `AudioGeneratorRight`.

### Shuffle logic

The order array is randomized with a Fisher-Yates style shuffle using:

- `ThreadLocalRandom.current()`

This means each cycle reorders the same four derived tones before playback.

## Screen interaction mapping

### Set Frequency button

When tapped:

1. reads the frequency textbox
2. defaults to `500` if blank
3. computes the four derived frequencies
4. updates the text labels
5. hides the keyboard
6. changes the button background to show that the value was accepted

### Both / Left / Right buttons

Each playback button:

1. resets and starts the chronometer
2. reads the timer spinner
3. optionally starts a `CountDownTimer`
4. changes the button graphics
5. disables the other playback buttons
6. clears `loopBreak`
7. starts a thread that runs the corresponding tone-generation method

### Stop button

When tapped:

1. restores button graphics
2. disables the stop button
3. re-enables the playback buttons
4. stops the chronometer
5. interrupts the worker thread
6. sets `loopBreak = true`
7. writes a log row if the logging DB exists

## Optional XML-driven input

`MainActivity` includes code to read:

```text
SECONDARY_STORAGE/tinnitustrio/cmt/Freq.xml
```

It parses a `<Value>` element with `XmlPullParser`.

This path is not the main user path, but it shows the app was intended to support externally supplied frequencies in addition to manual entry.

## Logging and remote integration

### Local database

The app stores activation and logging data under:

```text
/sdcard/TinnitusTrio/securityManager
```

Important tables:

- `security`
- `cmtLogger`

### What gets logged

Session logging records values like:

- date
- time
- elapsed playback duration

### Remote service usage

`GetService.java` supports:

- user registration / activation
- session log upload
- activation-status checking

So this app is not only a tone generator; it also participates in the same legacy activation ecosystem as the music-player projects.

## Important classes

| Class | Role |
| --- | --- |
| `MainActivity` | Main UI, flow control, timer, logging, and thread startup |
| `AudioGenerator` | Both-channel PCM synthesis |
| `AudioGeneratorLeft` | Left-only playback |
| `AudioGeneratorRight` | Right-only playback |
| `Metronome` | Auxiliary tone helper |
| `databasehandler` | Security database |
| `DatabaseLogger` | Session logger |
| `DatabaseExporter` | Log export |
| `GetService` | Remote service bridge |

## Why the app feels the way it does

The whole design is centered around one real-time loop:

**frequency input -> derive 4 tones -> synthesize PCM -> stream to `AudioTrack` -> repeat until stopped**

Everything else in the app exists around that loop:

- the UI chooses the frequencies and channel mode
- the chronometer measures session length
- the timer limits the session
- the database records what happened

## Key limitations

- Single large activity with heavy responsibilities
- Manual thread management
- No audio focus abstraction
- Uses older `AudioTrack` constants
- Legacy storage/network/identifier assumptions
- No modern background playback architecture

## Practical mental model

The cleanest way to understand this project is:

**it is a procedural tone engine wrapped in a simple Android control panel**
