# TinnitusTrioRequest Technical Details

## Overview

This application is a single-purpose request form for raising offline license requests tied to a doctor account.

## Page controls

From `Request.aspx` and its code-behind:

- `txtdoctorcode` - doctor code input
- `drplicenceqty` - license quantity selector
- `txtremarks` - free-text remarks field
- `btnsave` - submit request button

## Save button behavior

When `btnsave_Click` runs:

1. the doctor code is read from `txtdoctorcode`
2. the requested quantity is read from `drplicenceqty`
3. remarks are read from `txtremarks`
4. the code queries `DoctorDetails` to confirm the doctor exists
5. the code queries `OfflineLicenseRequests` to see whether there is already an active request
6. if an active request already exists:
   - the user is shown a duplicate/pending style message
   - no new request is inserted
7. if the doctor exists and no active request is pending:
   - a new request row is inserted
   - a success alert/message is shown
8. if validation fails:
   - an error alert/message is shown

## Feature-by-feature explanation

### Doctor verification

The page does not accept arbitrary doctor codes. It first validates that the request maps to a known doctor.

### Duplicate protection

The page prevents repeated concurrent requests for the same doctor by checking for already-active requests before inserting a new one.

### Request persistence

Once validated, the page stores a new offline license request entry in the database for later fulfillment.

## Control interpretation

- **Doctor code textbox** - identifies which doctor the request belongs to
- **License quantity drop-down** - chooses how many offline licenses are being requested
- **Remarks textbox** - carries operator context or justification
- **Save button** - executes validation and insertion logic

## Data model dependency

The code-behind depends on Entity Framework entities for:

- doctor master data
- offline license request records

This means the page is only functional when the EF model and backing SQL database are aligned.
