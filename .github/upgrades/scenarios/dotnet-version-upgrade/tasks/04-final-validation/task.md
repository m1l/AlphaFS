# 04-final-validation: Run final solution validation and document remaining follow-up

Run end-to-end validation after both dependency tiers complete. This task verifies the full solution build, executes the relevant test suite, and records any remaining follow-up recommendations that are outside the core target framework migration but were discovered during execution.

This final pass confirms that the bottom-up upgrade preserved solution integrity and that no warnings remain in the upgraded projects. It is also the point where any temporary compatibility constructs introduced during execution are reviewed for cleanup or future simplification.

**Done when**: The full solution builds successfully without warnings, relevant tests pass, and any remaining post-upgrade recommendations are documented in the workflow artifacts.
