# 03.01-test-project-retargeting: Retarget the AlphaFS unit test project and update test packages

## Objective
Update `tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj` so the test project targets a modern Windows-compatible framework and uses MSTest package versions that support that target.

## Scope
- Replace the current `net47` target with a modern Windows target compatible with `AlphaFS` on `net10.0-windows`
- Update `MSTest.TestAdapter` and `MSTest.TestFramework` from `1.3.2` to supported versions
- Keep the project reference to `src/AlphaFS/AlphaFS.csproj` working on the upgraded framework

## Research Findings
- `AlphaFS.UnitTest.csproj` defines its target framework directly in the project file as `<TargetFrameworks>net47</TargetFrameworks>`, so the project file is the correct place for the TFM change.
- The project is already SDK-style and has no `packages.config`, so retargeting and MSTest updates are direct in-place edits to the `.csproj`.
- Dependency inspection shows two explicit test packages in the project file: `MSTest.TestAdapter` `1.3.2` and `MSTest.TestFramework` `1.3.2`.
- Supported package lookups indicate both MSTest packages can be updated to `4.2.3` for a modern Windows test target.
- The project references `src/AlphaFS/AlphaFS.csproj`, which now targets `net10.0-windows` on its modern path, so the test project should align to `net10.0-windows` rather than plain `net10.0`.
- A preliminary MSBuild of the existing `net47` test project succeeded against the upgraded library, confirming the dependency chain is currently stable before retargeting.

**Done when**: The test project file targets the selected modern Windows TFM, test package references are updated to supported versions, and the project is ready for code-level test fixes.
