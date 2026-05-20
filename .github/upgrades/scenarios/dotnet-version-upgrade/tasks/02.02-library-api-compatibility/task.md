# 02.02-library-api-compatibility: Fix AlphaFS code incompatibilities for the new target

# 02.02-library-api-compatibility: Fix AlphaFS code incompatibilities for the new target

## Objective
Resolve the code-level incompatibilities in the AlphaFS library that block successful compilation for `net10.0`, while preserving existing behavior on the older targets.

## Scope
- Address assessment-flagged source and binary incompatibilities in network, path, exception, and hashing code
- Remove or conditionally isolate Code Access Security usage that is unsupported on modern .NET
- Apply `#if` guards only where APIs genuinely differ between target frameworks

## Research Starting Points
- Assessment-flagged files include `OpenConnectionInfo.cs`, `SessionInfo.cs`, `ServerStatisticsInfo.cs`, `Host.SMB.GetHostShareFromPath.cs`, `Path.Helpers.cs`, several exception classes, `KernelTransaction.cs`, `DeviceInfo.cs`, and `File.GetHashCore.cs`
- `SecurityPermission` attributes appear in `DeviceInfo.cs` and `KernelTransaction.cs`
- `RIPEMD160.Create()` is likely incompatible on the new target and may require conditional handling

**Done when**: The required code fixes for `net10.0` are in place, no stub work is introduced, and the library source is ready for project-level validation across targets.
