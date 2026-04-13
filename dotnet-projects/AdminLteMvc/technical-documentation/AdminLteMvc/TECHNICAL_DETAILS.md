# AdminLteMvc Technical Details

## Overview

This project is the browser-based administration console for the Tinnitus Trio ecosystem. It aggregates doctor registration, subscription control, patient review, and admin user management into a single MVC application.

## Request flow

1. Browser request reaches an MVC controller.
2. The controller validates input and forwards work into BAL/DAL classes.
3. The business/data layers query or update the Tinnitus Trio database.
4. The controller returns a full Razor view, partial view, or JSON payload depending on the screen.
5. AdminLTE layout partials keep navigation and shell behavior consistent across modules.

## Controllers and what they do

### `DoctorRegistrationController`

- `Index()` opens the main doctor registration view.
- `InsertDoctor(...)` submits doctor registration details to the backend.
- `InsertOnlineDoctor(...)` handles the online-registration variant.
- `OnlineDoctorReg()` opens the online doctor registration page.
- `GetDoctorDetails()` retrieves doctor records.
- `ViewDoctorDetails()` renders doctor detail results.
- `DoctorStatusResults(string id)` checks or displays doctor status information.
- `ReactivateDoctorStatus(string id)` reactivates an inactive doctor.

### `SubscriptionController`

- `Index()` opens the subscription module.
- `GetDoctorDetails(...)` resolves doctor information needed before subscription actions.
- `ActivateLicenses(...)` activates or extends licenses for a doctor.
- `SubscriptionDetails()` renders the subscription detail screen.

### `PatientController`

- `GetPatientDetails()` opens the doctor/patient search surface.
- `ViewDoctorDetails()` and `GetDoctorDetails()` support doctor lookup for patient-related actions.
- `GetDoctorDetailsForPatients()` loads doctor records in the patient workflow.
- `GetPatientInformation(string doctorCode)` returns patients attached to a doctor.
- `GetUserAuthenticationDetails(string uniqueId)` returns activation/authentication details for one patient.
- `UpdateDoctorDetails(string userId)` updates doctor linkage information.
- `ShowPatientDetails()` opens a patient-detail results page.
- `GetPatientDetailsForTransaction(string doctorCode)` loads transaction-oriented patient data.
- `GetPatientInformationforPatient(string doctorCode)` and `GetUserAuthenticationDetailsforPatient(string uniqueId)` drive patient-focused detail views.
- `DeActivateaPatient(string patientId)` and `ReActivateaPatient(string patientId)` change patient activation state.

### `UserController`

- `CreateUser(...)` creates administrative users.
- `User()` opens the user entry form.
- `ViewUser()` returns the user list.
- `Roles()` opens the role-management page.
- `CreateRoles(...)` inserts a new role.
- `GetRoles()` returns role lists.
- `UserRoleMapping()` opens the mapping screen between users and roles.

### `LoginController`, `AccountController`, `ManageController`

- These cover login, ASP.NET Identity account flows, password reset, external login scaffolding, and self-service account changes.

### `DashboardController` and `AdminLteController`

- These provide landing pages and theme/demo surfaces that anchor the AdminLTE shell.

## Page-by-page behavior

### Doctor registration pages

- **DoctorRegistration/Index**
  - primary doctor enrollment screen
  - collects doctor identity and clinic details
  - submits to `InsertDoctor(...)`
- **DoctorRegistration/OnlineDoctorReg**
  - alternate registration entry point for online workflows
  - likely captures the same core details but routes through `InsertOnlineDoctor(...)`
- **DoctorRegistration/ViewDoctorDetails**
  - review screen for existing doctor records
  - used after search or registration actions
- **Doctor status/reactivation**
  - route-driven operations using doctor ID
  - intended for support/admin recovery workflows

### Subscription pages

- **Subscription/Index**
  - opens subscription management
- **Subscription/SubscriptionDetails**
  - shows doctor-specific subscription status
  - supports activation or extension actions

Typical controls in this flow:

- doctor code input
- search/lookup trigger
- result summary labels or tables
- activation/extension action button

### Patient pages

- **Patient/GetPatientDetails**
  - entry point to locate patient records by doctor
- **Patient/GetPatientInformation**
  - renders patient lists for a selected doctor
- **Patient/PatientInformation**
  - detailed patient information surface
- **Patient/ShowPatientInformation**
  - alternate summary/detail rendering page
- **Patient/_DetailedPatientView**
  - partial used to render extended patient details
- **Patient/_UserAuthenticationDetails**
  - partial used to show activation/authentication fields
- **Patient/_ViewDoctors** and `_ViewDoctorForPatientDetails`
  - reusable doctor lookup/result partials

Typical actions available in the patient module:

- search by doctor code
- open patient details
- inspect activation/authentication data
- deactivate a patient
- reactivate a patient

### User and role pages

- **User/User**
  - create user or maintain user metadata
- **User/Roles**
  - create new roles
- **User/RolesList**
  - display role inventory
- **User/UserRoleMapping**
  - bind users to roles

Typical controls:

- text inputs for username, role name, or mapping keys
- save/create button
- grid/table of current roles or assignments

## Layout and navigation controls

The AdminLTE shell is composed from:

- `_AdminLteLayout.cshtml`
- `_AdminLteLeftMenu.cshtml`
- `_AdminLteTopMenu.cshtml`
- `_AdminLteBreadcrumb.cshtml`

These files collectively provide:

- left-side module navigation
- top navigation/status/user menu
- breadcrumb trail
- consistent page framing for all business modules

## Data dependencies

- `TinnitusTrioEntities` Entity Framework metadata connection
- `DBTinnitusTrio` direct SQL connection
- doctor, patient, subscription, and admin identity tables must already exist

## Operational notes

- The project assumes the backend database is already populated and structurally compatible.
- Many screens are wrappers around BAL/DAL methods, so most business rules live outside the controller itself.
- Because connection details are embedded in config, this project should be treated as a legacy internal tool rather than a deployment-ready public web app.
