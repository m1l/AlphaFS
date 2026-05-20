# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: .NET 10.0 (LTS)

## Source Control
- **Source Branch**: upgrade-dotnet-10
- **Working Branch**: upgrade-dotnet-10 (continue on current branch; do not create a new branch)
- **Commit Strategy**: After Each Task

## Upgrade Options
**Source**: .github/upgrades/scenarios/dotnet-version-upgrade/upgrade-options.md

### Strategy
- Upgrade Strategy: Bottom-Up

### Project Structure
- Project Approach: Multi-targeting

### Compatibility
- Unsupported API Handling: Fix Inline
- Windows Native APIs: Windows Compatibility Pack

## Strategy
**Selected**: Bottom-Up
**Rationale**: Two SDK-style projects form a 2-tier dependency graph across the .NET Framework to modern .NET boundary, so the library must be upgraded and validated before the dependent test project.

### Execution Constraints
- Strict tier ordering: complete and validate the AlphaFS library work before starting the dependent test project
- Keep the AlphaFS library multi-targeted while the test project is still on .NET Framework
- Resolve unsupported API changes inline within each upgrade task rather than creating deferred stub work
- Validate each tier with builds and affected tests before moving to the next tier
- Run full solution validation after all tier work is complete

## Build Tool Decisions
- **AlphaFS.csproj**: msbuild.exe (multi-targets .NET Framework TFMs and includes a .resx resource)

## User Preferences
### Technical Preferences
- Do not create a new branch; continue on the current branch instead.
