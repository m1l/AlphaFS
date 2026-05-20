# 03.03-test-acl-and-execution-validation: Fix remaining ACL-focused tests and validate the upgraded test project

# 03.03-test-acl-and-execution-validation: Fix remaining ACL-focused tests and validate the upgraded test project

## Objective
Address the Windows ACL and access-control test cases that remain after the general path/runtime fixes, then build and run the upgraded test project.

## Scope
- Update ACL/security-related tests and helpers using `DirectorySecurity`, `FileSecurity`, `GetAccessControl`, and `SetAccessControl`
- Resolve any remaining target-specific build or runtime issues after retargeting and general test fixes
- Build and execute the AlphaFS unit tests on the upgraded framework

## Research Starting Points
- Assessment flags Windows ACL usage as a named feature in this project with 28 issues
- Source sampling shows `TemporaryDirectory.cs` and dedicated directory/file access-control tests manipulate ACLs directly
- Validation should include both project build and project-level test execution after the ACL-focused fixes are complete

**Done when**: Remaining ACL-focused test issues are fixed, the test project builds warning-free, and project-level test execution passes on the upgraded target.
