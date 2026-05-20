# 02.01-project-targeting: Apply AlphaFS library targeting and code compatibility changes

## Objective
Update `src/AlphaFS/AlphaFS.csproj` and the AlphaFS library source together so the project adds `net10.0` support and remains buildable after the necessary compatibility fixes are applied.

## Scope
- Add `net10.0` to the library's target frameworks and align package/reference conditions for the new target
- Resolve the assessment-flagged source, binary, and behavioral incompatibilities required to compile the library for `net10.0`
- Remove or conditionally isolate unsupported Code Access Security usage and other modern-.NET incompatibilities

## Research Findings
- `AlphaFS.csproj` defines `TargetFrameworks` directly in the project file, not in `build/common.props` or another imported props file, so the project file is the correct place to add `net10.0`.
- The project is already SDK-style but still contains a `packages.config` file with only `NuGet.CommandLine` as a development dependency; it does not currently drive runtime package resolution.
- The project has a `.resx` embedded resource and still targets .NET Framework TFMs, so full multi-target validation should use Visual Studio MSBuild instead of `dotnet build`.
- Existing conditional package references are only for `netstandard20`: `Microsoft.Win32.Registry` 4.5.0, `System.IO.FileSystem.AccessControl` 4.5.0, and `System.Security.Permissions` 4.5.0.
- Dependency discovery shows no Central Package Management in use for this project, so package changes belong directly in `AlphaFS.csproj`.
- Package version lookups for `net10.0` indicate `System.IO.FileSystem.AccessControl` is supported and `Microsoft.Windows.Compatibility` is available for the target framework, matching the selected Windows compatibility approach.
- Assessment-flagged source files include `OpenConnectionInfo.cs`, `SessionInfo.cs`, `ServerStatisticsInfo.cs`, `Host.SMB.GetHostShareFromPath.cs`, `Path.Helpers.cs`, `File.GetHashCore.cs`, `DeviceInfo.cs`, `KernelTransaction.cs`, and several exception types with legacy serialization constructors.
- Code Access Security usage currently appears in `DeviceInfo.cs` and `KernelTransaction.cs`; these attributes are unsupported on modern .NET and need conditional isolation or removal for the new target.
- No `// STUB:` markers were found under `src/AlphaFS`, so this task should complete with real fixes rather than deferred stub work.

**Done when**: `AlphaFS.csproj` includes `net10.0`, required project/package conditions are updated, the AlphaFS library builds warning-free across its configured targets, and no deferred stub work was introduced.
