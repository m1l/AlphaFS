# 02-alphafs-library: Upgrade the AlphaFS library for multi-targeted .NET 10 support

Upgrade `src/AlphaFS/AlphaFS.csproj`, the leaf library in the dependency graph, by adding .NET 10 as a modern target while preserving existing frameworks needed by downstream consumers during the transition. This task includes the main compatibility work surfaced by the assessment: Code Access Security removal or replacement, binary/source API updates, and Windows ACL-related modernization compatible with the selected Windows Compatibility Pack approach.

Assessment context for this task includes 49 API issues in the library project, no package incompatibilities, and a requirement to keep the project buildable for both existing targets and the new `net10.0` target. Research should focus on conditional multi-targeting mechanics, ACL API replacements, obsolete serialization constructors, and any framework-specific code paths that need `#if` handling.

## Scope Inventory

- **Projects affected**: `src/AlphaFS/AlphaFS.csproj` directly, with `tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj` as the dependent build-validation project.
- **Distinct concerns**:
  - Add `net10.0` to the existing multi-targeted project and adjust package/reference conditions.
  - Fix source and behavioral compatibility issues in network/path code (`TimeSpan.FromSeconds`, URI/path handling).
  - Remove or conditionally isolate Code Access Security usage and old exception serialization patterns.
  - Preserve Windows-specific filesystem/security behavior for the new target using the selected Windows compatibility approach.
- **Change signals**:
  - Assessment summary for `src/AlphaFS/AlphaFS.csproj`: 50 issues total, including 1 target-framework change, 40 source incompatibilities, 6 behavioral changes, and 3 binary incompatibilities.
  - Affected files reported by the assessment include `OpenConnectionInfo.cs`, `SessionInfo.cs`, `ServerStatisticsInfo.cs`, `Host.SMB.GetHostShareFromPath.cs`, `Path.Helpers.cs`, several exception types with serialization constructors, `KernelTransaction.cs`, `DeviceInfo.cs`, and `File.GetHashCore.cs`.
  - The project is already SDK-style, uses `<TargetFrameworks>net45;net46;net47;netstandard20</TargetFrameworks>`, includes a `Resources.resx`, and has conditional package references today only for `netstandard20` (`Microsoft.Win32.Registry`, `System.IO.FileSystem.AccessControl`, `System.Security.Permissions`).
  - Existing `packages.config` contains only `NuGet.CommandLine` as a development dependency and does not drive runtime compatibility.
- **Skill matches**:
  - `managing-target-frameworks`: required for adding `net10.0` while preserving existing target frameworks.
  - `building-projects`: required because the project multi-targets .NET Framework and includes a `.resx`, which makes MSBuild the safer validation path.

## Research Findings

- `build/common.props` sets `LangVersion` to `7.2`, so language-level modernization is out of scope unless required for compatibility fixes.
- No `// STUB:` markers were found under `src/AlphaFS`, so the task does not need stub-resolution decomposition.
- Code Access Security usage currently appears in `Device/DeviceInfo.cs` and `Filesystem/KernelTransaction.cs` via `SecurityPermission` attributes; these need conditional handling or removal for modern .NET.
- The project contains exception serialization constructors in several filesystem exception types; these are assessment-flagged source incompatibilities that need review for `net10.0`.
- `File.GetHashCore.cs` still uses `RIPEMD160.Create()` behind a `!NETSTANDARD20` preprocessor guard, which is a likely source of the binary incompatibility flagged for the new target.
- `Host.SMB.GetHostShareFromPath.cs` and `Path.Helpers.cs` are part of the behavioral-change surface for URI/path handling on the new framework.
- Because the library task has one project but multiple independent concerns with different validation points, it should be executed as subtasks rather than as one atomic edit.

**Done when**: The AlphaFS library multi-targets `net10.0` alongside its existing targets, library code builds warning-free across targets, affected compatibility changes are resolved inline, and dependent solution projects still build successfully.
