# 03.01-test-project-retargeting Progress Details

## Summary
Retargeted the AlphaFS unit test project from `net47` to `net10.0-windows` and updated the MSTest packages to versions supported on the modern framework. A validation build was run immediately afterward to confirm the project-file changes took effect and to surface the first set of code-level compatibility issues for follow-up subtasks.

## Files Modified
- `tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj`
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/03-alphafs-tests/task.md`
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/03.01-test-project-retargeting/task.md`
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/03.01-test-project-retargeting/progress-details.md`

## Validation
- Updated `TargetFrameworks` from `net47` to `net10.0-windows` in `tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj`.
- Updated `MSTest.TestAdapter` and `MSTest.TestFramework` from `1.3.2` to `4.2.3`.
- Ran MSBuild on `tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj` after the retarget.
- The build reached code compilation and surfaced expected follow-up issues in test source, including direct `Assert.ThrowsException` usage, newer MSTest assertion overload differences, and access-control API usages that now require code updates in the next subtasks.

## Issues Encountered
- The retargeted build exposed source-level test incompatibilities rather than project-file problems. Key examples include missing `Assert.ThrowsException`, changed assertion overloads, and `GetAccessControl`/`SetAccessControl` member usage in tests. These belong to the next code-fix subtasks rather than blocking the project-retargeting step itself.
