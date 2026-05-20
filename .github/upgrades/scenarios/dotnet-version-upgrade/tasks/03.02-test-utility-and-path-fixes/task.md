# 03.02-test-utility-and-path-fixes: Update shared test utilities and path-behavior-sensitive tests

# 03.02-test-utility-and-path-fixes: Update shared test utilities and path-behavior-sensitive tests

## Objective
Fix the shared utilities and high-volume path-sensitive tests that break under the modern runtime, with special attention to `DirectoryInfo.FullName`-driven behaviors.

## Scope
- Update `UnitTest Utility/TemporaryDirectory.cs` and other shared helpers used broadly by the test suite
- Fix path-sensitive and filesystem-behavior tests highlighted heavily by the assessment
- Adjust assertions or setup code where the upgraded runtime changes normalized path output or related behavior

## Research Starting Points
- Assessment hotspots include `TemporaryDirectory.cs`, multiple `AlphaFS_Directory.IsEmpty*` tests, file-id tests, timestamp tests, and directory enumeration/path tests
- The dominant issue pattern is `System.IO.DirectoryInfo.FullName` usage in test setup, diagnostics, and assertions
- Changes here are likely to unblock many dependent tests at once, so tackle shared helpers first

**Done when**: Shared test utilities and the main path-behavior-sensitive test files are updated for the modern runtime and are ready for full project validation.
