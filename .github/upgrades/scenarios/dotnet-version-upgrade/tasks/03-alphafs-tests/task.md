# 03-alphafs-tests: Upgrade the AlphaFS unit tests to .NET 10

Upgrade `tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj` after the library task is complete, moving the test project to `net10.0` and updating test code to account for API and behavioral differences identified in the assessment. This project carries the majority of migration impact, with 410 API issues driven largely by Windows ACL APIs and filesystem behavior expectations, so task research should inventory the failing patterns before code changes begin.

This task also finalizes the dependency transition by ensuring tests consume the upgraded library target correctly. Research should start with the assessment's Windows ACL findings, high-frequency `DirectoryInfo.FullName` differences, and any MSTest compatibility updates needed for successful .NET 10 execution.

## Scope Inventory

- **Projects affected**: `tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj` directly, consuming the already-upgraded `src/AlphaFS/AlphaFS.csproj`.
- **Distinct concerns**:
  - Retarget the test project from `net47` to a modern Windows-compatible TFM that can reference `AlphaFS` on `net10.0-windows`.
  - Update MSTest packages from `1.3.2` to versions supported by the modern target.
  - Fix high-volume behavioral/API differences in test code, especially `System.IO.DirectoryInfo.FullName` usage and filesystem path expectations.
  - Update Windows ACL / access-control-oriented tests and utilities that depend on `DirectorySecurity`, `FileSecurity`, `GetAccessControl`, and `SetAccessControl` behavior.
  - Validate project build and test execution on the upgraded target.
- **Change signals**:
  - Assessment summary for `tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj`: 411 issues total, including 1 target-framework change, 407 mandatory binary incompatibilities, and 3 behavioral changes.
  - The dominant reported issue pattern is `System.IO.DirectoryInfo.FullName`, with many flagged files concentrated in path-heavy and temporary-directory test helpers.
  - Files with the highest issue concentration include `UnitTest Utility/TemporaryDirectory.cs`, multiple `AlphaFS_Directory.IsEmpty*` tests, timestamp tests, file-security tests, and file/directory utility wrappers.
  - Dependency discovery shows direct `PackageReference`s to `MSTest.TestAdapter` and `MSTest.TestFramework` version `1.3.2`, and a `ProjectReference` to `src/AlphaFS/AlphaFS.csproj`.
  - Supported-package lookup shows `MSTest.TestAdapter` and `MSTest.TestFramework` version `4.2.3` are available for a modern Windows test target.
- **Skill matches**:
  - `managing-target-frameworks`: needed for retargeting the test project to the modern framework.
  - `building-projects`: needed for project build validation and subsequent test execution.

## Research Findings

- `AlphaFS.UnitTest.csproj` is already SDK-style and currently uses `<TargetFrameworks>net47</TargetFrameworks>`, so this is an in-place retarget rather than a project-format conversion.
- The test project has no `packages.config`; package updates will be direct edits to the project file.
- The upgraded library now targets `net10.0-windows`, so the test project should move to a compatible modern Windows TFM rather than plain `net10.0`.
- A sample of assessment hits and source scanning confirms that `TemporaryDirectory.cs` is a central utility touching `Directory.FullName`, ACL setup, and cleanup behavior, making it a likely foundation file for many downstream test fixes.
- Test Explorer currently does not report tests for the project by a simple project-name lookup, so command-line or project-based build/test validation may be required during execution.
- This task is not atomic: it combines project retargeting/package changes, shared test utility updates, and broad test-code compatibility fixes across many files, so it should be executed as subtasks.

**Done when**: The test project targets `net10.0`, builds warning-free, all affected tests are updated for the upgraded library and runtime behavior, and project-level test execution passes.
