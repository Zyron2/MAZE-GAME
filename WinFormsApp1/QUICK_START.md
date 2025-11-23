# ?? Quick Start Guide

## Installation & Running

### Option 1: Visual Studio
```
1. Open WinFormsApp1.sln
2. Press F5 (Debug) or Ctrl+F5 (Release)
3. Game window opens
```

### Option 2: Command Line
```bash
cd WinFormsApp1
dotnet run
```

---

## Game Controls

| Key | Action |
|-----|--------|
| **?** or **W** | Move up |
| **?** or **S** | Move down |
| **?** or **A** | Move left |
| **?** or **D** | Move right |
| **R** | Restart game (when game over) |
| **H** | Show high scores (when game over) |

---

## Legend

| Symbol | Color | Meaning |
|--------|-------|---------|
| **P** | ?? Green | You (Player) |
| **E** | ?? Gold | Exit (Goal) |
| **M** | ?? Red | Monster (Enemy) |
| **#** | ? Gray | Wall (Obstacle) |
| (space) | ? White | Walkable area |

---

## Game Rules

? **WIN CONDITIONS:**
- Reach the Exit (E) without getting caught

? **LOSE CONDITIONS:**
- Touch a Monster (M)

?? **SCORING:**
- Your completion time is recorded
- Lower times = higher on leaderboard
- Top 10 scores are saved

---

## File Descriptions

| File | Purpose |
|------|---------|
| `Maze.cs` | Maze storage using Dictionary |
| `MonsterBFS.cs` | Smart monster using Queue (BFS) |
| `MonsterDFS.cs` | Alternative monster using Stack (DFS) |
| `GameController.cs` | Main game logic |
| `HighScoreTable.cs` | Score storage and sorting |
| `Form1.cs` | UI and graphics |
| `ExampleUsage.cs` | Demonstration code |

---

## Code Highlights

### Using the Maze (Dictionary)
```csharp
Maze maze = new Maze(21, 11);
maze.CreateSimpleMaze();

// Access a cell in O(1) time
char? cell = maze.GetAt(5, 3);

// Get walkable neighbors (graph edges)
var neighbors = maze.GetWalkableNeighbors(5, 3);
```

### BFS Monster AI (Queue)
```csharp
MonsterBFS monster = new MonsterBFS(maze, 5, 5);
monster.MoveTowardsPlayer(playerX, playerY);
// Uses Queue for breadth-first exploration
// Guarantees shortest path!
```

### DFS Monster AI (Stack)
```csharp
MonsterDFS monster = new MonsterDFS(maze, 5, 5);
monster.MoveTowardsPlayer(playerX, playerY);
// Uses Stack for depth-first exploration
// Takes longer, non-optimal paths
```

### High Scores (Sorting)
```csharp
HighScoreTable scores = new HighScoreTable();
scores.AddScore("Player", 45.5); // Automatically sorted
scores.SortByQuickSort();         // Custom QuickSort
scores.SortByMergeSort();         // Custom MergeSort
Console.WriteLine(scores.GetScoresAsString());
```

### Game Loop
```csharp
GameController game = new GameController();
game.InitializeGame();
game.MovePlayer(dx, dy);          // Move player
// Monsters automatically move (BFS)
if (game.GetGameState() == GameController.GameState.Won)
    game.AddHighScore("PlayerName");
```

---

## Data Structures Reference

### ??? Dictionary (Hash Map)
- **Usage**: Maze cell storage
- **Access**: O(1)
- **Key**: "x,y" coordinate string
- **Value**: Cell character

### ?? Queue
- **Usage**: BFS pathfinding
- **Method**: FIFO (First-In, First-Out)
- **Guarantees**: Shortest path in unweighted graph
- **Best For**: Finding optimal solutions

### ?? Stack
- **Usage**: DFS pathfinding
- **Method**: LIFO (Last-In, First-Out)
- **Explores**: Deeply before backtracking
- **Best For**: Memory-constrained scenarios

### ?? List
- **Usage**: Score storage
- **Operations**: Add, sort, iterate
- **Sorting Methods**: QuickSort, MergeSort, built-in

### ? HashSet
- **Usage**: Visited tracking in pathfinding
- **Access**: O(1)
- **Prevents**: Cycles and infinite loops

---

## Algorithm Comparison

### Pathfinding: BFS vs DFS

| Feature | BFS (Queue) | DFS (Stack) |
|---------|------------|-----------|
| **Data Structure** | Queue | Stack |
| **Exploration** | Level-by-level | Deep-first |
| **Shortest Path** | ? Yes | ? No |
| **Memory** | High | Low |
| **Time** | O(w×h) | O(w×h) limited |
| **Use Case** | Smart AI | Dumb AI |

### Sorting Comparison

| Algorithm | Time Avg | Time Worst | Space | Stable |
|-----------|----------|-----------|-------|--------|
| QuickSort | O(n log n) | O(n²) | O(log n) | No |
| MergeSort | O(n log n) | O(n log n) | O(n) | Yes |
| Built-in | O(n log n) | O(n log n) | O(1-log n) | Yes |

---

## Tips & Tricks

?? **Strategy Tips:**
- Watch monster movement patterns
- Plan your route to exit
- Use walls to your advantage
- Monsters move AFTER you, so act fast!

?? **Debugging:**
- Game state shown in title
- Time displayed on screen
- Check output window for any messages

?? **Learning:**
- Read the code with algorithm names
- Look at `ExampleUsage.cs` for demonstrations
- Check `USAGE_GUIDE.md` for detailed examples

---

## Troubleshooting

| Issue | Solution |
|-------|----------|
| Game won't start | Check .NET 10 runtime installed |
| Monsters not moving | Check GameTimer is enabled |
| Crashes on file load | Verify maze.txt exists |
| Score not saving | Check write permissions in directory |

---

## Next Steps

1. ? **Play the game** - Experience the maze
2. ?? **Read the code** - Understand data structures
3. ?? **Run ExampleUsage.cs** - See demonstrations
4. ?? **Modify the code** - Add features
5. ?? **Extend it** - Add new AI or algorithms

---

## Resources

- **README.md** - Full project description
- **USAGE_GUIDE.md** - Complete API documentation  
- **IMPLEMENTATION_SUMMARY.md** - Detailed summary
- **ExampleUsage.cs** - Code examples
- **Source code** - Fully commented classes

---

## Quick Code Snippets

### Add a score:
```csharp
HighScoreTable scores = new HighScoreTable();
scores.AddScore("YourName", 42.5);
```

### Create maze from file:
```csharp
Maze maze = new Maze(21, 11);
maze.LoadFromFile("maze.txt");
```

### Move player:
```csharp
GameController game = new GameController();
game.InitializeGame();
game.MovePlayer(1, 0);  // Right
```

### Start new game:
```csharp
GameController game = new GameController();
game.InitializeGame();
```

---

## About Data Structures

This game teaches:
- ? **Hash Maps** - O(1) lookups
- ? **Graphs** - Nodes and edges
- ? **Queues** - BFS algorithm
- ? **Stacks** - DFS algorithm
- ? **Sorting** - Multiple algorithms
- ? **Sets** - Visited tracking

Perfect for computer science students!

---

## Contact & Support

Check the source code comments for detailed explanations.
Each class includes XML documentation comments.

---

**Have fun and learn!** ????

Target Framework: .NET 10 | Language: C# 12 | Platform: Windows
