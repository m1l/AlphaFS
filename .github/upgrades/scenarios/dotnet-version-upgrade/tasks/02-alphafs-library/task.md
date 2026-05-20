# 02-alphafs-library: Upgrade the AlphaFS library for multi-targeted .NET 10 support

Upgrade `src/AlphaFS/AlphaFS.csproj`, the leaf library in the dependency graph, by adding .NET 10 as a modern target while preserving existing frameworks needed by downstream consumers during the transition. This task includes the main compatibility work surfaced by the assessment: Code Access Security removal or replacement, binary/source API updates, and Windows ACL-related modernization compatible with the selected Windows Compatibility Pack approach.

Assessment context for this task includes 49 API issues in the library project, no package incompatibilities, and a requirement to keep the project buildable for both existing targets and the new `net10.0` target. Research should focus on conditional multi-targeting mechanics, ACL API replacements, obsolete serialization constructors, and any framework-specific code paths that need `#if` handling.

**Done when**: The AlphaFS library multi-targets `net10.0` alongside its existing targets, library code builds warning-free across targets, affected compatibility changes are resolved inline, and dependent solution projects still build successfully.
