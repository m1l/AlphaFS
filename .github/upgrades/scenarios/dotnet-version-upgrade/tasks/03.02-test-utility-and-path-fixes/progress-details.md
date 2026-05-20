# 03.02-test-utility-and-path-fixes Progress Details

## Summary
Updated the shared test utilities and the first broad batch of path- and behavior-sensitive unit tests so the retargeted `AlphaFS.UnitTest` project compiles successfully on `net10.0-windows`. This work focused on modern MSTest assertion APIs, System.IO ACL extension APIs, and helper/test patterns that were no longer valid after the framework and test package upgrade.

## Files Modified
- `tests/AlphaFS.UnitTest/UnitTest Utility/TemporaryDirectory.cs`
- `tests/AlphaFS.UnitTest/Directory Class/AlphaFS_Directory.Copy/AlphaFS_Directory.Copy_UserExplicitDenyOnDestinationFolder_ThrowsUnauthorizedAccessException.cs`
- `tests/AlphaFS.UnitTest/Directory Class/Directory.Move/Directory.Move_UserExplicitDenyOnDestinationFolder_ThrowsUnauthorizedAccessException.cs`
- `tests/AlphaFS.UnitTest/File Class/File.Open/File_Open_Append.cs`
- `tests/AlphaFS.UnitTest/DirectoryInfo Class/DirectoryInfo.InitializeInstance/DirectoryInfo.InitializeInstance_ExistingDirectory.cs`
- `tests/AlphaFS.UnitTest/Path Class/Path.GetFullPath/Path.GetFullPath_InvalidLocalPath2_ThrowsArgumentException.cs`
- `tests/AlphaFS.UnitTest/File Class/File.Move/File.Move_ExistingFile.cs`
- `tests/AlphaFS.UnitTest/File Class/File.Create/File.Create.cs`
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
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/03.02-test-utility-and-path-fixes/task.md`
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/03.02-test-utility-and-path-fixes/progress-details.md`

## Validation
- Built `tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj` successfully with Visual Studio MSBuild targeting `net10.0-windows`.
- Final validation build for this subtask completed with exit code `0` and no reported warnings or errors.

## Issues Encountered
- The retargeted test project initially failed because legacy MSTest APIs (`Assert.ThrowsException`, old multi-argument assertion overloads) and direct ACL instance/static APIs no longer compiled under the upgraded framework and MSTest version.
- These issues were resolved by switching to modern MSTest assertion forms, using `System.IO.FileSystemAclExtensions` for System.IO ACL operations, and tightening a few tests to assert meaningful values instead of tautological conditions.
