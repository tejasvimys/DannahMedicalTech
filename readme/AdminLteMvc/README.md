# AdminLteMvc

## Purpose

`AdminLteMvc` is a legacy ASP.NET MVC 5 administration portal for the Tinnitus Trio platform. It centralizes doctor onboarding, doctor subscription management, patient lookup, patient activation/deactivation, user and role management, and dashboard-style back-office workflows in a browser-based UI built on the AdminLTE theme.

## Technology stack

- ASP.NET MVC 5 on .NET Framework 4.5
- Razor views with AdminLTE-based layout and partials
- ASP.NET Identity account/management scaffolding
- Entity Framework and direct SQL connection usage
- Multi-project solution structure with UI, business, data, model, and helper layers

## Solution structure

- `AdminLteMvc/` - web application, controllers, views, config, startup surface
- `AdminLteBAL/` - business-layer operations used by controllers
- `AdminLteDAL/` - data-access code for doctor, patient, login, and subscription operations
- `AdminLteModels/` - view models and domain transfer objects
- `BusinessFunctions/` - helper/business utilities referenced by the web layer

## Main functional areas

- **Authentication and admin access**
  - `AccountController`, `ManageController`, and `LoginController`
  - login, password reset, external login scaffolding, user self-management
- **Doctor management**
  - `DoctorRegistrationController`
  - doctor registration, online registration, doctor detail viewing, doctor status and reactivation
- **Subscription management**
  - `SubscriptionController`
  - doctor lookup, subscription detail retrieval, license activation
- **Patient management**
  - `PatientController`
  - doctor-based patient lookup, detailed patient information, authentication details, deactivation/reactivation
- **User and role administration**
  - `UserController`
  - user creation, role creation, role listing, user-role mapping
- **UI shell**
  - `DashboardController`, `AdminLteController`, shared AdminLTE menus/layout partials

## Important views

- `Views/DoctorRegistration/Index.cshtml`
- `Views/DoctorRegistration/OnlineDoctorReg.cshtml`
- `Views/DoctorRegistration/ViewDoctorDetails.cshtml`
- `Views/Subscription/SubscriptionDetails.cshtml`
- `Views/Patient/GetPatientDetails.cshtml`
- `Views/Patient/PatientInformation.cshtml`
- `Views/Patient/ShowPatientInformation.cshtml`
- `Views/User/User.cshtml`
- `Views/User/Roles.cshtml`
- `Views/User/UserRoleMapping.cshtml`
- `Views/Dashboard/Dashboard.cshtml`

## Configuration notes

- The project is configured for **.NET Framework 4.5**.
- The web project has IIS Express metadata in the project file and a legacy local URL of `http://localhost:49827/`.
- `Web.config` contains legacy database connection entries for:
  - `TinnitusTrioEntities`
  - `DBTinnitusTrio`
- The checked-in config uses hard-coded remote SQL endpoints and credentials. Replace them with local or environment-specific values before running outside the original environment.

## How it works at a high level

1. An admin logs into the portal.
2. The left-menu AdminLTE shell routes the user into doctor, patient, subscription, dashboard, or user-management screens.
3. Controllers call BAL/DAL classes to query or mutate doctor and patient records in the backend database.
4. Razor views and partial views render detail grids, forms, and status/activation actions.
5. Account and identity controllers handle admin authentication and profile maintenance.

## How to run

### Requirements

- Windows
- Visual Studio 2013 or later with ASP.NET MVC/.NET Framework 4.5 tooling
- SQL Server access matching the application schema
- NuGet package restore enabled

### Run in Visual Studio

1. Open `AdminLteMvc/AdminLteMvc.sln`.
2. Restore missing NuGet packages if Visual Studio prompts for it.
3. Update the `Web.config` connection strings to a reachable SQL Server instance.
4. Set `AdminLteMvc` as the startup project.
5. Run the project with IIS Express or local IIS.
6. Browse to the configured site URL and sign in with a valid application user.

### Build from Developer Command Prompt

```bat
msbuild AdminLteMvc.sln /t:Build /p:Configuration=Debug
```

## Known legacy constraints

- The codebase targets older MVC and .NET Framework components.
- Configuration is environment-specific and not deployment-safe as checked in.
- Some screens depend on existing database contents and stored business rules rather than self-contained seed data.

For the feature-by-feature behavior and page-level walkthrough, see `TECHNICAL_DETAILS.md`.
