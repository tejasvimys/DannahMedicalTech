# TinnitusTrioService Technical Details

## Overview

This WCF service is the server-side integration surface for activation and sync requests originating from client applications and support tools.

## Hosting model

- ASP.NET-compatible WCF service
- service class: `Service1`
- service contract: `IService1`
- compatibility mode enabled with `AspNetCompatibilityRequirementsMode.Allowed`

## Endpoint layout

From `Web.config`:

- `basicHttpBinding` endpoint at `bll`
- `mexHttpBinding` metadata endpoint at `mex`
- `webHttpBinding` endpoint at the root with web/REST behavior

This lets the same service support both SOAP-like and REST-style integration patterns.

## Operations and behavior

### `GetUniqueId(string uId)`

- receives a unique ID
- builds a `TrioBusinessObject`
- calls `TrioData.GetUniqueId(...)`
- returns `true`/`false`
- logs entry, input value, and return value

### `GetUniqueId1()`

- performs a related unique-ID validation path without a direct input parameter
- calls `TrioData.GetUniqueId1()`
- returns a Boolean

### `insertpatient(Stream JsonArray)`

- POST endpoint at `insertpatient`
- reads the raw request stream
- deserializes JSON into a `stringarray` object
- extracts:
  - `uniqueid`
  - `macid`
  - `serialno`
  - `hashid`
- calls `TrioData.InsertPatientDetails(...)`
- sends a log email/notification indicating the patient was activated
- returns success/failure as Boolean

This is the core activation insert path.

### `checkactivestatus(Stream JsonArray)`

- POST endpoint at `checkactivestatus`
- reads JSON from the request stream
- deserializes it into the same lightweight payload object
- uses `testObj.uniqueid`
- calls `TrioData.CheckActiveStatus(...)`
- returns a Boolean indicating whether the patient is active

### `synccmtdata(Stream xmlfile)`

- POST endpoint at `synccmtdata`
- reads the request body
- strips slash escaping
- converts JSON-style payload text into XML with `ConvertJsonToXml(...)`
- unescapes the final XML
- forwards the XML to `TrioData.SyncLogFiles(..., "CMT")`
- returns:
  - `"true"` for success
  - `"false"` for fail
  - `"invalidxml"` when the stream is null
  - exception text on failure

### `SyncCmmData(Stream xmlfile)`

Same pipeline as `synccmtdata`, but stores the result under the sync category:

- `"CMM"`

### `SyncCmesData(Stream xmlfile)`

Same sync pipeline, category:

- `"CMES"`

### `SyncCmnData(Stream xmlfile)`

Same sync pipeline, category:

- `"CMN"`

### `CheckPatientCreds(string csvPatientDetails)`

- validates patient credentials through the BAL/DAL path
- returns Boolean success/failure

### `ConvertJsonToXml(string jsonValue)`

- converts inbound JSON text into XML
- used internally by the sync methods before database/storage forwarding

## Shared payload model

At the bottom of `Service1.svc.cs`, the service uses a simple DTO carrying:

- `uniqueid`
- `macid`
- `serialno`
- `hashid`

This compact payload is reused by activation-related endpoints.

## Logging and notification behavior

The service uses:

- `log4net` for runtime logs
- `TrioBAL.MailerLog.SendLogDetails(...)`
- `TrioBAL.MailerLog.SendErrorDetails(...)`

So each important endpoint has both:

- diagnostic logging
- error escalation/notification

## Control-by-control interpretation

This project is headless, so there are no UI buttons. The effective controls are the HTTP operations:

- **`insertpatient`** inserts activation records
- **`checkactivestatus`** verifies active state
- **`synccmtdata` / `synccmmdata` / `synccmesdata` / `synccmndata`** upload category-specific sync logs
- **`getunique` / `GetUniqueId`** validate identity state

## Operational concerns

- request parsing is manual and fragile compared with modern typed APIs
- sync methods accept escaped payloads and do their own cleanup/unescaping
- backend correctness depends on the referenced BAL/DAL projects and SQL schema being present
