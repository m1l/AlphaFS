# 03-alphafs-tests Progress Details

## Summary
Completed the AlphaFS unit test project upgrade to the modern framework after the library tier was stabilized. The work retargeted the test project to `net10.0-windows`, updated MSTest packages, modernized outdated MSTest assertions, adapted System.IO ACL usage to `FileSystemAclExtensions` where required, and fixed shared test helper patterns so the upgraded test project now builds and runs successfully.

## Files Modified
- `tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj`
- `tests/AlphaFS.UnitTest/UnitTest Utility/TemporaryDirectory.cs`
- `tests/AlphaFS.UnitTest/Directory Class/AlphaFS_Directory.Copy/AlphaFS_Directory.Copy_UserExplicitDenyOnDestinationFolder_ThrowsUnauthorizedAccessException.cs`
- `tests/AlphaFS.UnitTest/Directory Class/Directory.Move/Directory.Move_UserExplicitDenyOnDestinationFolder_ThrowsUnauthorizedAccessException.cs`
- `tests/AlphaFS.UnitTest/File Class/File.Open/File_Open_Append.cs`
- `tests/AlphaFS.UnitTest/DirectoryInfo Class/DirectoryInfo.InitializeInstance/DirectoryInfo.InitializeInstance_ExistingDirectory.cs`
- `tests/AlphaFS.UnitTest/Path Class/Path.GetFullPath/Path.GetFullPath_InvalidLocalPath2_ThrowsArgumentException.cs`
- `tests/AlphaFS.UnitTest/File Class/File.Move/File.Move_ExistingFile.cs`
- `tests/AlphaFS.UnitTest/File Class/File_Create/File.Create.cs`
- `tests/AlphaFS.UnitTest/File Class/File_Create/File.Create_WithFileSecurity.cs`
- `tests/AlphaFS.UnitTest/Directory Class/Directory.CreateDirectory/Directory.CreateDirectory_WithDirectorySecurity.cs`
- `tests/AlphaFS.UnitTest/Directory Class/Directory_AccessControl/Directory.SetAccessControl.cs`
- `tests/AlphaFS.UnitTest/File Class/File_AccessControl/File.GetAccessControl.cs`
- `tests/AlphaFS.UnitTest/File Class/File_AccessControl/File.SetAccessControl.cs`
- `tests/AlphaFS.UnitTest/Directory Class/Directory_AccessControl/Directory.GetAccessControl.cs`
- `tests/AlphaFS.UnitTest/Directory Class/Directory_AccessControl/AlphaFS_Directory.HasInheritedPermissions.cs`
- `tests/AlphaFS.UnitTest/DirectoryInfo Class/DirectoryInfo.InitializeInstance/DirectoryInfo.InitializeInstance_AnalyzeDirectoryInfoSecurity.cs`
- `tests/AlphaFS.UnitTest/AlphaFS Junctions, Links/AlphaFS_Directory.CreateJunction_FileExistsWithSameNameAsDirectory_ThrowsIOException.cs`
- `tests/AlphaFS.UnitTest/AlphaFS Junctions, Links/AlphaFS_Directory.CreateSymbolicLink_And_GetLinkTargetInfo.cs`
- `tests/AlphaFS.UnitTest/AlphaFS Junctions, Links/AlphaFS_DirectoryInfo.CreateJunction_And_ExistsJunction_And_DeleteJunction.cs`
- `tests/AlphaFS.UnitTest/AlphaFS Junctions, Links/AlphaFS_File.CreateSymbolicLink_And_GetLinkTargetInfo.cs`
- `tests/AlphaFS.UnitTest/AlphaFS FileIdInfo Class/AlphaFS_Directory.GetFileIdInfo.cs`
- `tests/AlphaFS.UnitTest/AlphaFS FileIdInfo Class/AlphaFS_File.GetFileIdInfo.cs`
- `tests/AlphaFS.UnitTest/Directory Class/AlphaFS_Directory.DeleteEmptySubdirectories/AlphaFS_Directory.DeleteEmptySubdirectories.cs`
- `tests/AlphaFS.UnitTest/Directory Class/Directory_Enumeration/AlphaFS_Directory.EnumerateAlternateDataStreams.cs`
- `tests/AlphaFS.UnitTest/File Class/AlphaFS_File.EnumerateAlternateDataStreams.cs`
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/03-alphafs-tests/task.md`
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/03.01-test-project-retargeting/task.md`
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/03.01-test-project-retargeting/progress-details.md`
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/03.02-test-utility-and-path-fixes/task.md`
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/03.02-test-utility-and-path-fixes/progress-details.md`
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/03.03-test-acl-and-execution-validation/task.md`
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/03.03-test-acl-and-execution-validation/progress-details.md`
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/03-alphafs-tests/progress-details.md`

## Validation
- Built `tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj` successfully with Visual Studio MSBuild targeting `net10.0-windows`.
- Ran `dotnet test tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj --no-build -v minimal` successfully after the build.
- The final build/test cycle for the upgraded test project completed without reported warnings or errors.

## Issues Encountered
- The initial post-retarget build surfaced multiple categories of source incompatibilities: removed MSTest APIs, changed assertion overloads, System.IO ACL API changes, and helper/test assumptions tied to older framework behavior.
- These were resolved incrementally across the test subtasks by updating assertion forms, shifting modern System.IO ACL interactions to `FileSystemAclExtensions`, and tightening tests so they assert meaningful runtime behavior rather than patterns rejected by the new analyzers.
