## StatBall System

The StatBall system is a custom feature designed for Ultima Online server environments, allowing players to redistribute their character's core attributes: Strength, Dexterity, and Intelligence. This system introduces the old and well-known, easily customizable "225 StatBall" item, which provides a user-friendly interface for managing stat distribution within predefined limits.

### Features
- **StatBall Item:** An in-game item that allows players to allocate a total of 225 stat points across Strength, Dexterity, and Intelligence.
- **Intuitive GUI:** The StatBall comes with a Gump interface, guiding players through the stat allocation process with clear instructions and error handling.
- **Validation:** Ensures that players distribute exactly 225 points and enter valid numeric values for each stat.
- **Persistence:** The item's state is saved and loaded across sessions, maintaining consistency.

### Installation
1. Clone the repository to your server's script directory.
2. Compile the code to ensure it's properly integrated with your server.
3. Use the `StatBall` item in-game to begin customizing your character's stats.

### Usage
- **Activate the StatBall:** Double-click the StatBall item in your inventory to open the stat allocation interface.
- **Distribute Stats:** Enter your desired values for Strength, Dexterity, and Intelligence, ensuring the total equals 225.
- **Apply Changes:** Confirm your selection to update your character's stats. The StatBall will be consumed upon successful use.

This system enhances the gameplay experience by providing players with a flexible and controlled method of stat management.
