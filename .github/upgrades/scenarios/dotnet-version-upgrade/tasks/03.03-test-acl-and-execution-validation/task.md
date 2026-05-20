# 03.03-test-acl-and-execution-validation: Fix remaining ACL-focused tests and validate the upgraded test project

# 03.03-test-acl-and-execution-validation: Fix remaining ACL-focused tests and validate the upgraded test project

## Objective
Address the Windows ACL and access-control test cases that remain after the general path/runtime fixes, then build and run the upgraded test project.

## Scope
- Update ACL/security-related tests and helpers using `DirectorySecurity`, `FileSecurity`, `GetAccessControl`, and `SetAccessControl`
- Resolve any remaining target-specific build or runtime issues after retargeting and general test fixes
- Build and execute the AlphaFS unit tests on the upgraded framework

## Research Findings
- ACL-related test files now use a mix of AlphaFS access-control APIs and `System.IO.FileSystemAclExtensions` for the modern `System.IO` side.
- The dedicated access-control test files are `Directory_AccessControl/Directory.GetAccessControl.cs`, `Directory_AccessControl/Directory.SetAccessControl.cs`, `File_AccessControl/File.GetAccessControl.cs`, `File_AccessControl/File.SetAccessControl.cs`, plus helper/setup code in `TemporaryDirectory.cs` and `DirectoryInfo.InitializeInstance_AnalyzeDirectoryInfoSecurity.cs`.
- The remaining work in this subtask is primarily validation-focused: confirm the retargeted test project builds warning-free after the ACL updates and then execute the upgraded test project.
- The test project is SDK-style and now targets `net10.0-windows`, so command-line test execution should work once build validation is clean.

**Done when**: Remaining ACL-focused test issues are fixed, the test project builds warning-free, and project-level test execution passes on the upgraded target.
