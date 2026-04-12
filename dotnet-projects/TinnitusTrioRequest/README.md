# TinnitusTrioRequest

## Purpose

`TinnitusTrioRequest` is an ASP.NET Web Forms request portal used to submit offline license requests for doctors. It allows an operator to enter a doctor code, requested license quantity, and remarks, then stores the request if the doctor exists and no active request is already pending.

## Technology stack

- ASP.NET Web Forms on .NET Framework 4.5
- Entity Framework (`TinnitusTrioEntities`)
- SQL Server backend
- server-side code-behind validation with browser alerts

## Main page

- `Request.aspx` - single request submission form

## Main inputs

- doctor code
- requested license quantity
- remarks

## Backend behavior

`Request.aspx.cs` checks:

- whether the doctor exists
- whether an active offline license request already exists for that doctor
- whether the request can be inserted as a new pending item

## Configuration

- `Web.config` contains the Entity Framework connection `TinnitusTrioEntities`
- the checked-in connection points at a legacy remote SQL Server and should be replaced before running in a new environment

## How to run

### Requirements

- Windows
- Visual Studio 2013 or later with Web Forms support
- SQL Server database matching the EF model

### Run in Visual Studio

1. Open `TinnitusTrioRequest.sln`.
2. Update the `TinnitusTrioEntities` connection string in `Web.config`.
3. Restore packages if needed.
4. Set the web project as startup.
5. Run with IIS Express or local IIS.
6. Open `Request.aspx` and submit a valid request.

### Build

```bat
msbuild TinnitusTrioRequest.sln /t:Build /p:Configuration=Debug
```

## Legacy notes

- The application is intentionally narrow: one page, one business workflow.
- Feedback is presented through alert-style messages rather than a richer dashboard.

For the control-level request workflow, see `TECHNICAL_DETAILS.md`.
