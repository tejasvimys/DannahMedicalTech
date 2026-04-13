# TinnitusTrioOfflineKeyGenerator Technical Details

## Overview

This utility exposes a single-form workflow for converting a long popup/device key into a short offline key.

## Main form controls

From the form layout and code:

- `textEdit1` - input field where the operator pastes the Android popup key
- `simpleButton1` - trigger button to generate the offline key
- `lblUniqueId` - output label that displays the generated value

## Button-by-button behavior

### Generate button

When `simpleButton1` is clicked:

1. the text from `textEdit1` is read
2. the string is hashed with SHA1
3. the resulting hash string is converted to text
4. the first 8 characters are extracted
5. `lblUniqueId` is updated with the derived short key

## Intended operator workflow

1. receive a key from the Android popup/device
2. paste it into the input box
3. press the generate button
4. read the generated key from the label
5. use that short key in the offline activation workflow

## Why the UI is minimal

The tool does one job only:

- manual deterministic key derivation for offline support scenarios

There are no extra screens, storage layers, or background services.

## Control interpretation

- **Input textbox (`textEdit1`)** - source identifier string
- **Generate button (`simpleButton1`)** - executes the derivation
- **Output label (`lblUniqueId`)** - final offline code presented to the operator
