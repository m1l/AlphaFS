# 02.01-project-targeting Progress Details

## Summary
Updated the AlphaFS library to add a modern Windows-specific target and applied the code and project-file changes required for successful multi-target compilation. The work added `net10.0-windows`, aligned conditional package/reference handling, removed unsupported CAS and legacy exception-serialization code from the modern target, replaced the obsolete `PolicyException` usage, and cleaned up build issues surfaced during validation.

## Files Modified
- `src/AlphaFS/AlphaFS.csproj`
- `src/AlphaFS/Device/Device.cs`
- `src/AlphaFS/Device/DeviceInfo.cs`
- `src/AlphaFS/Filesystem/KernelTransaction.cs`
- `src/AlphaFS/Filesystem/Native Methods/NativeMethods.DeviceManagement.cs`
- `src/AlphaFS/Filesystem/File Class/File Core Methods/File.GetHashCore.cs`
- `src/AlphaFS/Filesystem/File Class/File Core Methods/File.CopyMoveCore.cs`
- `src/AlphaFS/Filesystem/Directory Class/Directory.SetAccessControl.cs`
- `src/AlphaFS/NativeError.cs`
- `src/AlphaFS/Filesystem/Exceptions/AlreadyExistsException.cs`
- `src/AlphaFS/Filesystem/Exceptions/DeviceNotReadyException.cs`
- `src/AlphaFS/Filesystem/Exceptions/DirectoryNotEmptyException.cs`
- `src/AlphaFS/Filesystem/Exceptions/DirectoryReadOnlyException.cs`
- `src/AlphaFS/Filesystem/Exceptions/FileReadOnlyException.cs`
- `src/AlphaFS/Filesystem/Exceptions/InvalidTransactionException.cs`
- `src/AlphaFS/Filesystem/Exceptions/NotAReparsePointException.cs`
- `src/AlphaFS/Filesystem/Exceptions/NotSameDeviceException.cs`
- `src/AlphaFS/Filesystem/Exceptions/TransactionalConflictException.cs`
- `src/AlphaFS/Filesystem/Exceptions/TransactionAlreadyAbortedException.cs`
- `src/AlphaFS/Filesystem/Exceptions/TransactionAlreadyCommittedException.cs`
- `src/AlphaFS/Filesystem/Exceptions/TransactionException.cs`
- `src/AlphaFS/Filesystem/Exceptions/UnrecognizedReparsePointException.cs`
- `src/AlphaFS/Filesystem/Exceptions/UnsupportedRemoteTransactionException.cs`
- `.github/upgrades/scenarios/dotnet-version-upgrade/scenario-instructions.md`
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/02-alphafs-library/task.md`
- `.github/upgrades/scenarios/dotnet-version-upgrade/tasks/02.01-project-targeting/task.md`

## Validation
- Built `src/AlphaFS/AlphaFS.csproj` with Visual Studio MSBuild across all configured targets.
- Validation result: successful builds for `net10.0-windows`, `netstandard20`, `net47`, `net46`, and `net45`.
- Final build completed without reported warnings.

## Issues Encountered
- Initial `net10.0` validation failed because legacy exception serialization constructors were still compiled for the modern target; this was fixed by guarding those constructors and their `System.Runtime.Serialization` imports.
- CA1416 Windows-platform warnings showed that the library should explicitly target Windows for the modern framework, so the new target was adjusted from `net10.0` to `net10.0-windows`.
- Replacing obsolete `AsAny` marshalling introduced a temporary signature mismatch in `DeviceIoControlUnknownSize`; this was corrected by switching the call site to pinned buffers with explicit `IntPtr` arguments.
- Removing `Microsoft.Win32.Registry` entirely broke the `netstandard20` build, so the package was restored for `netstandard20` only and kept out of the new Windows target where it was unnecessary.
