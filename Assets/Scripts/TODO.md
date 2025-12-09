# TODO List

## Plan

### Edit Plan

#### Information Gathered
1. **Current Logic**:
   - The game currently awards a score based on the number of pieces cleared.
   - The score is updated in the `OnPieceCleared` method of the `Level` class.

2. **New Requirements**:
   - Each level should specify the number and type of pieces required to win.
   - The score should be calculated based on the specified pieces rather than the number of pieces cleared.

3. **Relevant Files**:
   - `Level.cs`: Base class for all levels.
   - `LevelMoves.cs`: Handles levels based on the number of moves.
   - `LevelObstacles.cs`: Handles levels based on clearing obstacles.
   - `LevelTimer.cs`: Handles levels based on time.
   - `Hud.cs`: Handles the user interface updates.

4. **Dependencies**:
   - The `Level` class needs to be modified to handle the new win conditions.
   - The `LevelMoves`, `LevelObstacles`, and `LevelTimer` classes need to be updated to specify the required pieces.
   - The `Hud` class needs to be updated to display the new win conditions.

#### Plan
1. **Modify `Level.cs`**:
   - Add fields to store the required pieces and their counts.
   - Update the `OnPieceCleared` method to check if the required pieces have been cleared.
   - Update the `GameWin` and `GameLose` methods to check the new win conditions.

2. **Modify `LevelMoves.cs`**:
   - Add fields to specify the required pieces.
   - Update the `OnMove` method to check the new win conditions.

3. **Modify `LevelObstacles.cs`**:
   - Add fields to specify the required pieces.
   - Update the `OnPieceCleared` and `OnMove` methods to check the new win conditions.

4. **Modify `LevelTimer.cs`**:
   - Add fields to specify the required pieces.
   - Update the `Update` method to check the new win conditions.

5. **Modify `Hud.cs`**:
   - Update the `SetLevelType` method to display the new win conditions.
   - Update the `SetTarget` and `SetRemaining` methods to display the required pieces.

#### Dependent Files to be edited
- `Level.cs`
- `LevelMoves.cs`
- `LevelObstacles.cs`
- `LevelTimer.cs`
- `Hud.cs`

#### Follow-up steps
1. **Testing**:
   - Test each level type to ensure the new win conditions are working correctly.
   - Verify that the `Hud` updates are displaying the correct information.

2. **Installations**:
   - No additional installations are required.

3. **Documentation**:
   - Update the documentation to reflect the new win conditions.

## Tasks

- [ ] Modify `Level.cs`
- [ ] Modify `LevelMoves.cs`
- [ ] Modify `LevelObstacles.cs`
- [ ] Modify `LevelTimer.cs`
- [ ] Modify `Hud.cs`
- [ ] Test each level type
- [ ] Verify `Hud` updates
- [ ] Update documentation
