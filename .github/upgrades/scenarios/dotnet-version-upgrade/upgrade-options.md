# Upgrade Options — AlphaFS

Assessment: 2 SDK-style projects targeting .NET Framework/.NET Standard with 461 API issues, no package incompatibilities, and Windows ACL-related migration work.

## Strategy

### Upgrade Strategy
Multiple projects cross the .NET Framework to modern .NET boundary, so the dependency chain should be upgraded and validated from the library upward.

| Value | Description |
|-------|-------------|
| **Bottom-Up** (selected) | Upgrade leaf-node libraries first, then move upward through dependents with validation at each stage. |

## Project Structure

### Project Approach
The main library must continue serving a .NET Framework test project during the transition, so the library work should preserve compatibility while the test project is upgraded afterward.

| Value | Description |
|-------|-------------|
| **Multi-targeting** (selected) | Add a modern .NET target alongside existing targets so the library can serve both old and upgraded consumers during the transition. |
| In-place | Replace the target framework directly and require all consumers to move at the same time. |

## Compatibility

### Unsupported API Handling
The assessment found hundreds of API compatibility issues, but the identified changes are concentrated in known BCL areas where resolving them in the owning upgrade tasks is the safest default.

| Value | Description |
|-------|-------------|
| **Fix Inline** (selected) | Resolve API changes directly in each upgrade task, including complex replacements, without creating deferred stub work. |
| Defer Complex Changes | Apply simple fixes immediately and create follow-up stub-resolution subtasks for complex replacements. |

### Windows Native APIs
Windows ACL-related API usage appears across the solution, so preserving Windows behavior first is the lowest-risk way to complete the framework upgrade before considering broader portability.

| Value | Description |
|-------|-------------|
| **Windows Compatibility Pack** (selected) | Add Microsoft.Windows.Compatibility so Windows-specific APIs remain available during the upgrade. |
| No Compatibility Pack | Surface Windows-specific build errors immediately and replace them with cross-platform alternatives now. |
