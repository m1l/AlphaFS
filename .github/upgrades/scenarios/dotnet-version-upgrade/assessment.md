# Projects and dependencies analysis

This document provides a comprehensive overview of the projects and their dependencies in the context of upgrading to .NETCoreApp,Version=v10.0.

## Table of Contents

- [Executive Summary](#executive-Summary)
  - [Highlevel Metrics](#highlevel-metrics)
  - [Projects Compatibility](#projects-compatibility)
  - [Package Compatibility](#package-compatibility)
  - [API Compatibility](#api-compatibility)
- [Aggregate NuGet packages details](#aggregate-nuget-packages-details)
- [Top API Migration Challenges](#top-api-migration-challenges)
  - [Technologies and Features](#technologies-and-features)
  - [Most Frequent API Issues](#most-frequent-api-issues)
- [Projects Relationship Graph](#projects-relationship-graph)
- [Project Details](#project-details)

  - [src\AlphaFS\AlphaFS.csproj](#srcalphafsalphafscsproj)
  - [tests\AlphaFS.UnitTest\AlphaFS.UnitTest.csproj](#testsalphafsunittestalphafsunittestcsproj)


## Executive Summary

### Highlevel Metrics

| Metric | Count | Status |
| :--- | :---: | :--- |
| Total Projects | 2 | All require upgrade |
| Total NuGet Packages | 2 | All compatible |
| Total Code Files | 1033 |  |
| Total Code Files with Incidents | 149 |  |
| Total Lines of Code | 101844 |  |
| Total Number of Issues | 461 |  |
| Estimated LOC to modify | 459+ | at least 0.5% of codebase |

### Projects Compatibility

| Project | Target Framework | Difficulty | Package Issues | API Issues | Est. LOC Impact | Description |
| :--- | :---: | :---: | :---: | :---: | :---: | :--- |
| [src\AlphaFS\AlphaFS.csproj](#srcalphafsalphafscsproj) | net45;net46;net47;netstandard20 | 🟢 Low | 0 | 49 | 49+ | ClassLibrary, Sdk Style = True |
| [tests\AlphaFS.UnitTest\AlphaFS.UnitTest.csproj](#testsalphafsunittestalphafsunittestcsproj) | net47 | 🟡 Medium | 0 | 410 | 410+ | ClassLibrary, Sdk Style = True |

### Package Compatibility

| Status | Count | Percentage |
| :--- | :---: | :---: |
| ✅ Compatible | 2 | 100.0% |
| ⚠️ Incompatible | 0 | 0.0% |
| 🔄 Upgrade Recommended | 0 | 0.0% |
| ***Total NuGet Packages*** | ***2*** | ***100%*** |

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 410 | High - Require code changes |
| 🟡 Source Incompatible | 40 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 9 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 40922 |  |
| ***Total APIs Analyzed*** | ***41381*** |  |

## Aggregate NuGet packages details

| Package | Current Version | Suggested Version | Projects | Description |
| :--- | :---: | :---: | :--- | :--- |
| MSTest.TestAdapter | 1.3.2 |  | [AlphaFS.UnitTest.csproj](#testsalphafsunittestalphafsunittestcsproj) | ✅Compatible |
| MSTest.TestFramework | 1.3.2 |  | [AlphaFS.UnitTest.csproj](#testsalphafsunittestalphafsunittestcsproj) | ✅Compatible |

## Top API Migration Challenges

### Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |
| Windows Access Control Lists (ACLs) | 28 | 6.1% | Windows Access Control List (ACL) APIs for file, directory, and synchronization object security that have moved to extension methods or different types. While .NET Core supports Windows ACLs, the APIs have been reorganized. Use System.IO.FileSystem.AccessControl and similar packages for ACL functionality. |
| Code Access Security (CAS) | 4 | 0.9% | Code Access Security (CAS) APIs that were removed in .NET Core/.NET for security and performance reasons. CAS provided fine-grained security policies but proved complex and ineffective. Remove CAS usage; not supported in modern .NET. |

### Most Frequent API Issues

| API | Count | Percentage | Category |
| :--- | :---: | :---: | :--- |
| P:System.IO.DirectoryInfo.FullName | 377 | 82.1% | Binary Incompatible |
| M:System.TimeSpan.FromSeconds(System.Double) | 16 | 3.5% | Source Incompatible |
| M:System.IO.IOException.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext) | 12 | 2.6% | Source Incompatible |
| M:System.IO.DirectoryInfo.GetAccessControl | 8 | 1.7% | Binary Incompatible |
| M:System.IO.DirectoryInfo.SetAccessControl(System.Security.AccessControl.DirectorySecurity) | 7 | 1.5% | Binary Incompatible |
| T:System.Uri | 4 | 0.9% | Behavioral Change |
| M:System.UnauthorizedAccessException.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext) | 4 | 0.9% | Source Incompatible |
| M:System.IO.File.GetAccessControl(System.String) | 4 | 0.9% | Binary Incompatible |
| P:System.IO.DriveInfo.DriveFormat | 3 | 0.7% | Behavioral Change |
| M:System.IO.Directory.GetAccessControl(System.String) | 3 | 0.7% | Binary Incompatible |
| M:System.TimeSpan.FromMilliseconds(System.Double) | 2 | 0.4% | Source Incompatible |
| M:System.Uri.TryCreate(System.String,System.UriKind,System.Uri@) | 2 | 0.4% | Behavioral Change |
| M:System.SystemException.#ctor(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext) | 2 | 0.4% | Source Incompatible |
| T:System.Security.Permissions.SecurityPermissionAttribute | 2 | 0.4% | Source Incompatible |
| T:System.Security.Cryptography.RIPEMD160 | 2 | 0.4% | Binary Incompatible |
| M:System.IO.DirectoryInfo.GetAccessControl(System.Security.AccessControl.AccessControlSections) | 2 | 0.4% | Binary Incompatible |
| M:System.Security.Cryptography.RIPEMD160.Create | 1 | 0.2% | Binary Incompatible |
| T:System.Security.Policy.PolicyException | 1 | 0.2% | Source Incompatible |
| M:System.Security.Policy.PolicyException.#ctor(System.String) | 1 | 0.2% | Source Incompatible |
| M:System.IO.FileStream.GetAccessControl | 1 | 0.2% | Binary Incompatible |
| M:System.IO.File.Create(System.String,System.Int32,System.IO.FileOptions,System.Security.AccessControl.FileSecurity) | 1 | 0.2% | Binary Incompatible |
| M:System.IO.Directory.CreateDirectory(System.String,System.Security.AccessControl.DirectorySecurity) | 1 | 0.2% | Binary Incompatible |
| M:System.IO.Directory.GetAccessControl(System.String,System.Security.AccessControl.AccessControlSections) | 1 | 0.2% | Binary Incompatible |
| M:System.IO.File.GetAccessControl(System.String,System.Security.AccessControl.AccessControlSections) | 1 | 0.2% | Binary Incompatible |
| M:System.IO.FileInfo.GetAccessControl(System.Security.AccessControl.AccessControlSections) | 1 | 0.2% | Binary Incompatible |

## Projects Relationship Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart LR
    P1["<b>📦&nbsp;AlphaFS.csproj</b><br/><small>net45;net46;net47;netstandard20</small>"]
    P2["<b>📦&nbsp;AlphaFS.UnitTest.csproj</b><br/><small>net47</small>"]
    P2 --> P1
    click P1 "#srcalphafsalphafscsproj"
    click P2 "#testsalphafsunittestalphafsunittestcsproj"

```

## Project Details

<a id="srcalphafsalphafscsproj"></a>
### src\AlphaFS\AlphaFS.csproj

#### Project Info

- **Current Target Framework:** net45;net46;net47;netstandard20
- **Proposed Target Framework:** net45;net46;net47;netstandard20;net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 0
- **Dependants**: 1
- **Number of Files**: 679
- **Number of Files with Incidents**: 21
- **Lines of Code**: 76907
- **Estimated LOC to modify**: 49+ (at least 0.1% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph upstream["Dependants (1)"]
        P2["<b>📦&nbsp;AlphaFS.UnitTest.csproj</b><br/><small>net47</small>"]
        click P2 "#testsalphafsunittestalphafsunittestcsproj"
    end
    subgraph current["AlphaFS.csproj"]
        MAIN["<b>📦&nbsp;AlphaFS.csproj</b><br/><small>net45;net46;net47;netstandard20</small>"]
        click MAIN "#srcalphafsalphafscsproj"
    end
    P2 --> MAIN

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 3 | High - Require code changes |
| 🟡 Source Incompatible | 40 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 6 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 25657 |  |
| ***Total APIs Analyzed*** | ***25706*** |  |

#### Project Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |
| Code Access Security (CAS) | 4 | 8.2% | Code Access Security (CAS) APIs that were removed in .NET Core/.NET for security and performance reasons. CAS provided fine-grained security policies but proved complex and ineffective. Remove CAS usage; not supported in modern .NET. |

<a id="testsalphafsunittestalphafsunittestcsproj"></a>
### tests\AlphaFS.UnitTest\AlphaFS.UnitTest.csproj

#### Project Info

- **Current Target Framework:** net47
- **Proposed Target Framework:** net10.0
- **SDK-style**: True
- **Project Kind:** ClassLibrary
- **Dependencies**: 1
- **Dependants**: 0
- **Number of Files**: 358
- **Number of Files with Incidents**: 128
- **Lines of Code**: 24937
- **Estimated LOC to modify**: 410+ (at least 1.6% of the project)

#### Dependency Graph

Legend:
📦 SDK-style project
⚙️ Classic project

```mermaid
flowchart TB
    subgraph current["AlphaFS.UnitTest.csproj"]
        MAIN["<b>📦&nbsp;AlphaFS.UnitTest.csproj</b><br/><small>net47</small>"]
        click MAIN "#testsalphafsunittestalphafsunittestcsproj"
    end
    subgraph downstream["Dependencies (1"]
        P1["<b>📦&nbsp;AlphaFS.csproj</b><br/><small>net45;net46;net47;netstandard20</small>"]
        click P1 "#srcalphafsalphafscsproj"
    end
    MAIN --> P1

```

### API Compatibility

| Category | Count | Impact |
| :--- | :---: | :--- |
| 🔴 Binary Incompatible | 407 | High - Require code changes |
| 🟡 Source Incompatible | 0 | Medium - Needs re-compilation and potential conflicting API error fixing |
| 🔵 Behavioral change | 3 | Low - Behavioral changes that may require testing at runtime |
| ✅ Compatible | 15265 |  |
| ***Total APIs Analyzed*** | ***15675*** |  |

#### Project Technologies and Features

| Technology | Issues | Percentage | Migration Path |
| :--- | :---: | :---: | :--- |
| Windows Access Control Lists (ACLs) | 28 | 6.8% | Windows Access Control List (ACL) APIs for file, directory, and synchronization object security that have moved to extension methods or different types. While .NET Core supports Windows ACLs, the APIs have been reorganized. Use System.IO.FileSystem.AccessControl and similar packages for ACL functionality. |

