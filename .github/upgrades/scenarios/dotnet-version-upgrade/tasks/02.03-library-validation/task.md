# 02.03-library-validation: Build and validate the upgraded AlphaFS library and dependents

## Objective
Validate that the upgraded AlphaFS library builds warning-free across all targets and that the dependent solution project still builds after the library changes.

## Scope
- Build `src/AlphaFS/AlphaFS.csproj` across its target frameworks
- Build the dependent test project or solution as required to confirm downstream compatibility
- Fix any remaining warnings or target-specific issues exposed by the validation builds

## Research Findings
- `AlphaFS.csproj` requires Visual Studio MSBuild for validation because it multi-targets .NET Framework TFMs and includes a `.resx` resource.
- The library was successfully built across all configured targets with MSBuild: `net10.0-windows`, `netstandard20`, `net47`, `net46`, and `net45`.
- The dependent project `tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj` was then built successfully with MSBuild against the upgraded library, producing `net47` outputs for both the library and test assembly.
- No additional code changes were required during this validation step; the upstream library work was sufficient for downstream build compatibility.

**Done when**: The AlphaFS library builds warning-free for all configured targets, downstream build validation succeeds, and the parent library-upgrade objective is satisfied.
