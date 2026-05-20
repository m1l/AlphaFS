# 03-alphafs-tests: Upgrade the AlphaFS unit tests to .NET 10

Upgrade `tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj` after the library task is complete, moving the test project to `net10.0` and updating test code to account for API and behavioral differences identified in the assessment. This project carries the majority of migration impact, with 410 API issues driven largely by Windows ACL APIs and filesystem behavior expectations, so task research should inventory the failing patterns before code changes begin.

This task also finalizes the dependency transition by ensuring tests consume the upgraded library target correctly. Research should start with the assessment's Windows ACL findings, high-frequency `DirectoryInfo.FullName` differences, and any MSTest compatibility updates needed for successful .NET 10 execution.

**Done when**: The test project targets `net10.0`, builds warning-free, all affected tests are updated for the upgraded library and runtime behavior, and project-level test execution passes.
