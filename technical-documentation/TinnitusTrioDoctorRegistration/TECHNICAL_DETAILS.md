# TinnitusTrioDoctorRegistration Technical Details

## Overview

This web application is a doctor administration portal. It mixes server-side Web Forms page lifecycle behavior with client-side jQuery-driven form submission.

## Login flow

### `Login.aspx`

Key controls identified in the login surface:

- `UserName` - username textbox
- `password` - password textbox
- `pin` - 4-digit PIN textbox
- `btnLogin` - submits credentials
- `AlertSpan` - inline error message area

`btnLogin_Click`:

1. reads username, password, and PIN
2. builds a `TinnitusTrioBO.Login` object
3. calls `TinnitusTrioBAL.AdminLogin.CheckLogin`
4. if successful:
   - redirects to `FirstLogin.aspx` when required
   - otherwise redirects to `Dashboard.aspx`
5. if unsuccessful:
   - updates `AlertSpan` with an invalid-credentials message

## First login flow

### `FirstLogin.aspx`

Controls:

- `UserName` - username
- `password` - new password
- `checkpassword` - confirm password
- `pin` - new PIN
- `checkpin` - confirm PIN
- `btnLogin` - completes the first-login reset
- `AlertSpan` - shows failure text

`btnLogin_Click`:

1. reads the entered username, password, and PIN
2. calls `AdminLogin.UpdatePassword(...)`
3. on `"SUCCESS"`, redirects to `Dashboard.aspx`
4. otherwise sets `AlertSpan` to tell the user the username, password, or PIN is invalid

This page is effectively a first-use credential activation screen rather than a standard login.

## Dashboard

### `Dashboard.aspx`

- currently acts as a minimal landing page inside `MasterPage.Master`
- likely depends on the master-page navigation rather than on-screen controls of its own

## Register doctor workflow

### `RegisterDoctor.aspx`

This is the most feature-rich page in the project.

#### Client-side behavior

- adds a full-page loading overlay `#ajaxBusy`
- hides success/error alerts on load
- uses jQuery UI `datepicker` for `txtSubscriptionTo`
- uses `populateCountries("country", "state")` for country/state drop-down lists
- disables browser back-navigation using a history-forward script

#### Main form controls

- `txtHospitalName`
- `txtFirstName`
- `txtMiddleName`
- `txtLastName`
- `txtAddressLine1`
- `txtAddressLine2`
- `country`
- `state`
- `txtCity`
- `txtZipCode`
- `txtTelephone`
- `txtMobileNo`
- `txtFax`
- `txtEmail`
- `txtWebsite`
- `optionsRadiosInline1` - full license
- `optionsRadiosInline2` - subscription-based license
- `txtSubscriptionTo` - date shown only for subscription-based mode
- `btnSave`
- `btnReset`
- `successMsg`
- `errorMsg`

#### Button-by-button behavior

**Reset button**

- clears every input field
- clears subscription date
- hides the subscription date section
- resets the license radio buttons

**License type radio buttons**

- `optionsRadiosInline1` hides the "subscription until" field
- `optionsRadiosInline2` shows it

**Save button**

1. shows the loading overlay
2. gathers every textbox and drop-down value
3. derives `SubscriptionType`
   - `1` for full
   - `0` for subscription based
4. runs client-side validation for required fields and email format
5. posts JSON to `RegisterDoctor.aspx/SaveDoctorDetail`
6. on success:
   - shows `successMsg`
   - hides `errorMsg`
7. on failure:
   - shows `errorMsg`
   - hides the busy overlay

#### Server-side handler

`SaveDoctorDetail(DoctorRegistration objDoctorRegistration)`:

- calls `TinnitusTrioBAL.DoctorDetails.GetDoctorDetails(...)`
- returns the BAL result string to the AJAX caller

The success message says credentials are emailed to the doctor and admin, so the BAL layer likely inserts the doctor record and triggers credential delivery.

## Doctor details page

### `DoctorDetails.aspx`

Controls and behavior:

- `dataTablesexample` - ASP.NET `GridView`
- page script upgrades `.myTable` into a DataTables grid

`Page_Load`:

1. creates `TinnitusTrioBAL.DoctorDetails`
2. calls `DoctorDetailsTable()`
3. binds the returned table to the grid
4. enables accessible header rendering for DataTables styling

This page is the read-only doctor inventory view.

## Extend subscription page

### `ExtendSubscription.aspx`

Controls:

- `txtDoctorId`
- `txtSubscriptionTo`
- `btnSave`
- `btnReset`

Client-side behavior:

- attaches a datepicker to `txtSubscriptionTo`
- disables back-navigation

Observed server-side behavior:

- the current code-behind contains no handler implementation

This means the page UI exists, but the business action is either unfinished, implemented elsewhere, or expected to be wired later.

## Change subscription page

### `ChangeSubscription.aspx`

Controls:

- `txtDoctorId`
- `optionsRadiosInline1` - Full
- `optionsRadiosInline2` - Subscription Based
- `txtSubscriptionTo`
- `btnSave`
- `btnReset`

Button/control behavior:

- datepicker is attached to `txtSubscriptionTo`
- subscription date field is hidden by default
- selecting subscription-based mode reveals the date field
- clicking **Save**:
  1. reads doctor ID
  2. determines license type from the selected radio button
  3. builds a JSON payload
  4. posts to `ChangeSubscription.aspx/UpdateSubscriptionType`
  5. alerts success when the return value is `"1"`
  6. alerts `"Error"` on AJAX failure

Server-side method:

- `UpdateSubscriptionType(DoctorRegistration objDoctorRegistration)`
- calls `TinnitusTrioBAL.DoctorDetails.UpdateDoctorSubscriptionType(...)`

## Master-page role

Pages other than the standalone login screens inherit from `MasterPage.Master`, so shared navigation and layout controls likely live there. The dashboard, registration, doctor list, and subscription pages all depend on that shell.

## Summary of feature coverage

- credential-based login with username/password/PIN
- first-login credential reset
- doctor registration with hospital/contact/subscription data
- doctor record listing
- subscription type change
- placeholder/partial subscription extension UI
