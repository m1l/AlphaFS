# 02.02-library-api-compatibility: Fix AlphaFS code incompatibilities for the new target

## Objective
Resolve the code-level incompatibilities in the AlphaFS library that block successful compilation for `net10.0`, while preserving existing behavior on the older targets.

## Scope
- Address assessment-flagged source and binary incompatibilities in network, path, exception, and hashing code
- Remove or conditionally isolate Code Access Security usage that is unsupported on modern .NET
- Apply `#if` guards only where APIs genuinely differ between target frameworks

## Research Findings
- The assessment-flagged compatibility work was completed during `02.01-project-targeting` because the project could not add the new target without the corresponding code fixes.
- Completed code-level changes already in the repo include:
  - conditional removal of `SecurityPermission` usage for the `net10.0-windows` target in `DeviceInfo.cs` and `KernelTransaction.cs`
  - conditional exclusion of legacy exception serialization constructors across the custom exception types for the modern target
  - conditional exclusion of the `RIPEMD160` hashing path for the modern target in `File.GetHashCore.cs`
  - replacement of `PolicyException` with `InvalidOperationException` in `NativeError.cs`
  - project and marshalling fixes required to get the library compiling cleanly across all configured TFMs
- Validation evidence from the previous subtask shows `src/AlphaFS/AlphaFS.csproj` builds successfully for `net10.0-windows`, `netstandard20`, `net47`, `net46`, and `net45` after those code changes.
- No `// STUB:` markers were introduced; all compatibility changes were fixed inline as required by the selected upgrade options.

## Already-Done Check
This subtask's objective is already satisfied by the code changes completed in `02.01-project-targeting`. No additional product-code edits are required here before moving to downstream validation.

**Done when**: The required code fixes for `net10.0` are in place, no stub work is introduced, and the library source is ready for project-level validation across targets.
