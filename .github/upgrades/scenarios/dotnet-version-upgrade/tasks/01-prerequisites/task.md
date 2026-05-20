# 01-prerequisites: Verify SDK and repository prerequisites

Confirm that the .NET 10 SDK and repository configuration support the planned upgrade before project files are changed. This task covers toolchain readiness checks, any global.json compatibility review, and a quick verification that the current branch-based workflow settings in the upgrade artifacts are consistent with the user's preference to stay on the existing branch.

This task does not change product code, but it establishes the safety baseline for the rest of the upgrade. If SDK or repository constraints are discovered here, they must be resolved before the library and test migration tasks begin.

## Research Findings

- `validate_dotnet_sdk_installation` confirmed that a compatible .NET 10 SDK is installed.
- A workspace search found no `global.json` file, so there is no repository-pinned SDK version blocking the upgrade.
- Git checks confirmed the current working branch is `upgrade-dotnet-10` and `git diff --name-only` returned no existing uncommitted source changes before task execution.
- Upgrade workflow artifacts were updated to reflect the confirmed options, bottom-up strategy, and the user preference to continue on the current branch instead of creating a new one.

**Done when**: The .NET 10 SDK is confirmed installed, any global.json constraints are reviewed and documented, and the upgrade workflow artifacts reflect the confirmed execution setup.
