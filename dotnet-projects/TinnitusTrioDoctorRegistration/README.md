# TinnitusTrioDoctorRegistration

## Purpose

`TinnitusTrioDoctorRegistration` is a legacy ASP.NET Web Forms portal for doctor onboarding and administration. It handles doctor login, first-login credential reset, doctor registration, doctor listing, and subscription-related maintenance from a browser UI.

## Technology stack

- ASP.NET Web Forms on .NET Framework 4.5
- jQuery and jQuery UI
- Bootstrap/Admin-style page layout
- BAL/BO/DAL project references
- SQL Server backend accessed through a configured connection string

## Main pages

- `Login.aspx` - sign-in screen
- `FirstLogin.aspx` - forced first-login password/PIN update
- `Dashboard.aspx` - post-login landing page
- `RegisterDoctor.aspx` - doctor registration form
- `DoctorDetails.aspx` - doctor table/grid
- `ExtendSubscription.aspx` - extend subscription screen
- `ChangeSubscription.aspx` - switch full vs subscription-based licensing

## Configuration

- Targets **.NET Framework 4.5**
- `Web.config` defines a `TrioConnection` SQL Server connection string
- The checked-in config points at a machine-specific SQL Server name; replace it with a reachable local or network database before running

## How the application works

1. The user logs in on `Login.aspx`.
2. If the account is on first login, the user is redirected to `FirstLogin.aspx`.
3. Successful credential update redirects the user to `Dashboard.aspx`.
4. From the master-page navigation, the user can register doctors, review doctor records, or adjust subscription settings.

## How to run

### Requirements

- Windows
- Visual Studio 2013 or later with ASP.NET Web Forms support
- SQL Server instance containing the required registration schema
- NuGet package restore

### Run in Visual Studio

1. Open `TinnitusTrioDoctorRegistration.sln`.
2. Restore packages if prompted.
3. Update the `TrioConnection` value in `Web.config`.
4. Set `TinnitusTrioDoctorRegistration` as the startup project.
5. Run with IIS Express or local IIS.
6. Browse to `Login.aspx` and sign in with a valid doctor/admin account.

### Build

```bat
msbuild TinnitusTrioDoctorRegistration.sln /t:Build /p:Configuration=Debug
```

## Legacy notes

- Client-side validation is implemented directly in page scripts.
- Several actions are AJAX calls into static `[WebMethod]` handlers on the page code-behind.
- Behavior depends on existing backend business logic and database content.

For the page-by-page walkthrough and control-level behavior, see `TECHNICAL_DETAILS.md`.
