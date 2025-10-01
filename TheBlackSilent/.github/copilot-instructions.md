# Copilot Instructions for TheBlackSilent

## Project Overview
This is a Unity 2D game project using the Universal Render Pipeline and several Unity packages (see `Packages/manifest.json`). The main gameplay involves player movement, hiding mechanics, and an inventory system.

## Key Components
- **PlayerController** (`Assets/Scrip/PlayerController.cs`): Handles player movement, hiding, and interaction with hiding spots.
- **Inventory System**
  - `InventoryManeger.cs`, `ItemSlot.cs`, `Item.cs`: Manages item pickup, inventory UI, and item slot logic. Note: Naming inconsistencies exist (`InventoryManeger` vs `InventoryManager`, `ItemSolot` vs `ItemSlot`).
- **HideInCabinet** (`Assets/Scrip/HideInCabinet.cs`): Allows the player to hide in cabinets using a child object named `HidePoint`.
- **CameraController**: Follows the player with smooth movement.

## Developer Workflows
- **Build**: Use Unity Editor for building and running the game. No custom build scripts detected.
- **Testing**: The Unity Test Framework is included (`com.unity.test-framework`), but no test scripts found. Add PlayMode/EditMode tests in `Assets/Tests/` if needed.
- **Debugging**: Use Unity Editor's Play mode and Inspector. Debug logs are used for missing components (e.g., missing `HidePoint`).

## Project-Specific Patterns
- **Inventory**: Items are picked up via collision and added to the inventory. If the inventory is full, leftover items remain in the world.
- **Hiding Mechanic**: Player can hide/unhide at designated points using the `F` key. Layer collision is toggled for hiding.
- **UI**: Inventory and prompts are toggled via input. UI elements are referenced via serialized fields.
- **Naming**: Watch for typos and inconsistencies in class/file names (e.g., `InventoryManeger`, `ItemSolot`).

## Integration Points
- **Unity Packages**: See `Packages/manifest.json` for dependencies (Input System, URP, Visual Scripting, etc.).
- **Input**: Uses Unity's Input System. Key bindings may be set in `InputSystem_Actions.inputactions`.
- **External Assets**: Scenes, UI, and sprites are managed in `Assets/` subfolders.

## Recommendations for AI Agents
- Always check for naming mismatches when referencing classes or components.
- When adding new features, follow the serialized field pattern for Unity Inspector integration.
- Place new scripts in `Assets/Scrip/` and new assets in appropriate subfolders.
- For new mechanics, ensure cross-component communication via Unity events or direct references as seen in existing scripts.
- If adding tests, create an `Assets/Tests/` folder and use the Unity Test Framework.

---
_If any section is unclear or missing, please provide feedback for further refinement._
