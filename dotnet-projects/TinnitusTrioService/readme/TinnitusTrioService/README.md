# TinnitusTrioService

## Purpose

`TinnitusTrioService` is a legacy WCF web service that exposes activation and synchronization endpoints for the Tinnitus Trio ecosystem. It supports patient activation, activation-status checking, patient credential validation, and upload of multiple categories of log/synchronization data from connected clients.

## Technology stack

- WCF service on .NET Framework 4.0
- ASP.NET-compatible hosted service
- `basicHttpBinding`, `webHttpBinding`, and metadata endpoints
- log4net logging
- Newtonsoft.Json and `JavaScriptSerializer`
- BAL/BO project references for business logic

## Service contracts and endpoints

From `IService1.cs` and `Web.config`, the service exposes:

- `GetUniqueId(string uId)`
- `getunique`
- `insertpatient`
- `checkactivestatus`
- `synccmtdata`
- `synccmmdata`
- `synccmesdata`
- `synccmndata`
- `CheckPatientCreds(string csvPatientDetails)`
- `ConvertJsonToXml(string jsonValue)`

Configured endpoints:

- `basicHttpBinding` endpoint at `bll`
- metadata endpoint at `mex`
- REST-style `webHttpBinding` endpoint at the service root

## What the service does

- checks whether a unique ID or patient record exists/validates
- inserts patient activation details received from clients
- checks whether a patient is active
- accepts synchronization payloads for multiple data categories
- converts JSON payloads into XML before forwarding them to backend log-sync routines
- writes operational logs and sends error/log notifications through BAL helpers

## Configuration

- Target framework: **.NET Framework 4.0**
- Local development URL in project metadata: `http://localhost:19296/`
- `Web.config` contains a legacy SQL connection entry named `DBTinnitusTrio`

The checked-in config contains hard-coded remote database credentials. Replace them with safe local or environment-specific values before running.

## How to run

### Requirements

- Windows
- Visual Studio 2010 or later with WCF tooling
- .NET Framework 4.0 targeting support
- SQL Server access for the referenced backend schema
- NuGet restore for `log4net` and `Newtonsoft.Json`

### Run in Visual Studio

1. Open `TinnitusTrioService.sln`.
2. Restore packages if prompted.
3. Update `Web.config` connection settings.
4. Set `TinnitusTrioService` as the startup project.
5. Run the project.
6. Browse to `Service1.svc` or the configured localhost URL to host the service.
7. Test endpoints with WCF Test Client, Postman, or another HTTP client.

### Build

```bat
msbuild TinnitusTrioService.sln /t:Build /p:Configuration=Debug
```

## Legacy notes

- The service accepts raw streams and performs manual deserialization/parsing.
- Sync endpoints use category codes such as `CMT`, `CMM`, `CMES`, and `CMN`.
- Logging and notification behavior is tightly coupled to the BAL layer.

For the operation-by-operation walkthrough, see `TECHNICAL_DETAILS.md`.
