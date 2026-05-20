# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade the AlphaFS solution to .NET 10.0 while preserving the existing multi-targeted library surface until the dependent test project is moved to the new framework.
**Scope**: 2 SDK-style projects, ~102k LOC, 461 API compatibility issues concentrated in the core library and its dependent test project.

### Selected Strategy
**Bottom-Up (Dependency-First)** — Upgrade from leaf nodes to root applications, tier by tier.
**Rationale**: 2 projects with a 2-tier dependency graph.

Tier 2: [AlphaFS.UnitTest]
         ↓
Tier 1: [AlphaFS]

## Tasks

### 01-prerequisites: Verify SDK and repository prerequisites

Confirm that the .NET 10 SDK and repository configuration support the planned upgrade before project files are changed. This task covers toolchain readiness checks, any global.json compatibility review, and a quick verification that the current branch-based workflow settings in the upgrade artifacts are consistent with the user's preference to stay on the existing branch.

This task does not change product code, but it establishes the safety baseline for the rest of the upgrade. If SDK or repository constraints are discovered here, they must be resolved before the library and test migration tasks begin.

**Done when**: The .NET 10 SDK is confirmed installed, any global.json constraints are reviewed and documented, and the upgrade workflow artifacts reflect the confirmed execution setup.

---

### 02-alphafs-library: Upgrade the AlphaFS library for multi-targeted .NET 10 support

Upgrade `src/AlphaFS/AlphaFS.csproj`, the leaf library in the dependency graph, by adding .NET 10 as a modern target while preserving existing frameworks needed by downstream consumers during the transition. This task includes the main compatibility work surfaced by the assessment: Code Access Security removal or replacement, binary/source API updates, and Windows ACL-related modernization compatible with the selected Windows Compatibility Pack approach.

Assessment context for this task includes 49 API issues in the library project, no package incompatibilities, and a requirement to keep the project buildable for both existing targets and the new `net10.0` target. Research should focus on conditional multi-targeting mechanics, ACL API replacements, obsolete serialization constructors, and any framework-specific code paths that need `#if` handling.

**Done when**: The AlphaFS library multi-targets `net10.0` alongside its existing targets, library code builds warning-free across targets, affected compatibility changes are resolved inline, and dependent solution projects still build successfully.

---

### 03-alphafs-tests: Upgrade the AlphaFS unit tests to .NET 10

Upgrade `tests/AlphaFS.UnitTest/AlphaFS.UnitTest.csproj` after the library task is complete, moving the test project to `net10.0` and updating test code to account for API and behavioral differences identified in the assessment. This project carries the majority of migration impact, with 410 API issues driven largely by Windows ACL APIs and filesystem behavior expectations, so task research should inventory the failing patterns before code changes begin.

This task also finalizes the dependency transition by ensuring tests consume the upgraded library target correctly. Research should start with the assessment's Windows ACL findings, high-frequency `DirectoryInfo.FullName` differences, and any MSTest compatibility updates needed for successful .NET 10 execution.

**Done when**: The test project targets `net10.0`, builds warning-free, all affected tests are updated for the upgraded library and runtime behavior, and project-level test execution passes.

---

### 04-final-validation: Run final solution validation and document remaining follow-up

Run end-to-end validation after both dependency tiers complete. This task verifies the full solution build, executes the relevant test suite, and records any remaining follow-up recommendations that are outside the core target framework migration but were discovered during execution.

This final pass confirms that the bottom-up upgrade preserved solution integrity and that no warnings remain in the upgraded projects. It is also the point where any temporary compatibility constructs introduced during execution are reviewed for cleanup or future simplification.

**Done when**: The full solution builds successfully without warnings, relevant tests pass, and any remaining post-upgrade recommendations are documented in the workflow artifacts.
