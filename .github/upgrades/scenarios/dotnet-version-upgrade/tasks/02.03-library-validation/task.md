# 02.03-library-validation: Build and validate the upgraded AlphaFS library and dependents

# 02.03-library-validation: Build and validate the upgraded AlphaFS library and dependents

## Objective
Validate that the upgraded AlphaFS library builds warning-free across all targets and that the dependent solution project still builds after the library changes.

## Scope
- Build `src/AlphaFS/AlphaFS.csproj` across its target frameworks
- Build the dependent test project or solution as required to confirm downstream compatibility
- Fix any remaining warnings or target-specific issues exposed by the validation builds

## Research Starting Points
- Use MSBuild for full validation because the project multi-targets .NET Framework TFMs and includes a `.resx` resource
- The dependent project is `tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj`
- The library task is not complete until the dependent project still builds successfully

**Done when**: The AlphaFS library builds warning-free for all configured targets, downstream build validation succeeds, and the parent library-upgrade objective is satisfied.
