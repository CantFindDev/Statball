## StatBall System

The StatBall system is a custom feature designed for Ultima Online server environments, enabling players to redistribute their character's core attributes: Strength, Dexterity, and Intelligence. This system introduces the classic and highly customizable "225 StatBall" item, offering a user-friendly interface for precise stat management within predefined limits.

### Features
- **StatBall Item:** An in-game item allowing players to allocate a total of 225 stat points across Strength, Dexterity, and Intelligence, facilitating balanced character development.
- **User-Friendly Interface:** The StatBall includes an intuitive Gump interface, guiding players through the stat allocation process with clear instructions and robust error handling.
- **Validation Mechanism:** Ensures players distribute exactly 225 points and enter valid numeric values for each stat, preventing errors and enhancing gameplay balance.
- **Persistence:** The item's state is saved and loaded across sessions, ensuring consistent player experience and reliable data management.

![StatBall Interface](https://github.com/user-attachments/assets/e949dc5d-d454-44c5-adfb-d80b964252e0)
![Stat Distribution](https://github.com/user-attachments/assets/d61c9053-6193-419e-a98b-666813fd04f8)

### Installation
1. **Script Integration:** Add `statball.cs` to the `scripts/items/[CUSTOM]` directory within your server files.
2. **Starter Item Setup:** To provide players with the StatBall at character creation, modify `CharacterCreation.cs` by adding `PackItem(new StatBall());` under the `AddBackpack` method.

   ![Script Example](https://github.com/user-attachments/assets/e8723aaf-d7d3-4042-a45c-f1984d7ab02a)

### Usage
- **Activate the StatBall:** Double-click the StatBall item in your inventory to access the stat allocation interface.
- **Distribute Stats:** Enter your desired values for Strength, Dexterity, and Intelligence, ensuring the total equals 225 points.
- **Apply Changes:** Confirm your selection to update your character's stats. The StatBall will be consumed upon successful use.
