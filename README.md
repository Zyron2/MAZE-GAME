# Maze Game - WinForms Implementation

A comprehensive C# WinForms maze game demonstrating key data structures and algorithms:
- **Hash Maps (Dictionaries)** - Maze representation
- **Graphs** - Maze connectivity and pathfinding
- **Queues (BFS)** - Intelligent monster AI
- **Stacks (DFS)** - Alternative monster pathfinding
- **Sorting Algorithms** - High-score leaderboard

## Project Structure

### Core Game Files

#### **Maze.cs**
Represents the maze using a `Dictionary<string, char>` (Map/Hash Table)
- **Key**: Coordinate string like "x,y" (e.g., "10,5")
- **Value**: Character at that position ('#' wall, ' ' walkable, 'P' player, 'E' exit, 'M' monster)

**Key Methods:**
- `LoadFromFile()` - Load maze from text file
- `CreateSimpleMaze()` - Generate a default maze
- `IsWalkable()` - Check if a position is navigable
- `GetWalkableNeighbors()` - Get adjacent walkable tiles (implements graph edges)
- `GetMazeString()` - Render maze as string

**Data Structures Used:**
- `Dictionary<string, char>` - O(1) average lookup for maze cells
- Space complexity: O(width × height) for storing all maze cells

---

#### **MonsterBFS.cs**
Monster AI using **Breadth-First Search (BFS)** with a **Queue**

**Algorithm:**
1. Start BFS from monster's current position
2. Use a `Queue` to explore neighbors level-by-level
3. Track visited positions to avoid cycles
4. When player is found, return the first step of the shortest path
5. Monster moves one step closer each turn

**Key Features:**
- Finds the **guaranteed shortest path** to the player
- Time complexity: O(width × height) per pathfinding
- Space complexity: O(width × height) for visited set and queue

**Why Queue for BFS?**
- FIFO (First-In-First-Out) explores nodes in "waves" of distance
- Guarantees shortest path in unweighted graphs (maze with uniform movement cost)

---

#### **MonsterDFS.cs**
Monster AI using **Depth-First Search (DFS)** with a **Stack**

**Algorithm:**
1. Start DFS from monster's current position
2. Use a `Stack` to explore deep into paths
3. Backtrack when hitting dead ends
4. Creates a "dumber" monster that gets lost easily

**Key Features:**
- Takes longer, non-optimal paths
- Explores deeply before backtracking
- Max depth limit (50) prevents infinite exploration
- Falls back to random movement if player not found

**Why Stack for DFS?**
- LIFO (Last-In-First-Out) explores deeply down one path
- Useful for exploring maze structure
- Less memory efficient than BFS for finding shortest paths

---

#### **HighScoreTable.cs**
High-score leaderboard with multiple sorting algorithms

**Data Structure:**
- `List<ScoreEntry>` - Stores completion times with player names

**Sorting Algorithms Implemented:**
1. **Built-in Sort()** - Uses Timsort (hybrid of MergeSort and InsertionSort)
   - Time: O(n log n)
   - Space: O(n)

2. **QuickSort** - Custom implementation
   - Time: O(n log n) average, O(n²) worst case
   - Space: O(log n) recursive stack

3. **MergeSort** - Custom implementation
   - Time: O(n log n) guaranteed
   - Space: O(n) for merging

**Features:**
- Saves/loads scores from file (persistence)
- Keeps top 10 scores only
- Implements `IComparable<ScoreEntry>` for comparison

---

#### **GameController.cs**
Main game logic controller

**Responsibilities:**
- Initialize maze and game state
- Handle player movement and collision detection
- Update monster positions
- Check win/lose conditions
- Manage score tracking
- Provide game state to UI

**Game States:**
- `Running` - Active gameplay
- `Won` - Player reached exit
- `Lost` - Player hit by monster

---

#### **Form1.cs**
WinForms UI implementation

**Features:**
- **Rendering**: Graphics-based maze display with colors
  - Green = Player (P)
  - Red = Monsters (M)
  - Gold = Exit (E)
  - Dark Gray = Walls (#)
  - White = Empty space

- **Controls**:
  - Arrow Keys or WASD - Move player
  - R - Restart game
  - H - View high scores

- **Game Loop**: Timer-based updates every 100ms
- **Double Buffering**: Smooth rendering without flicker

---

## How to Play

1. **Start the game** - Run the application
2. **Navigate the maze** - Use Arrow Keys or WASD to move
3. **Reach the exit (E)** - Get there before monsters catch you
4. **Avoid monsters (M)** - They use BFS to hunt you down intelligently
5. **Win** - Reach the exit and your time is recorded

---

## Game Rules

- **Player (P)**: Must reach the exit without touching monsters
- **Monsters (M)**: Hunt the player using intelligent pathfinding
- **Walls (#)**: Block movement for both player and monsters
- **Exit (E)**: Goal location - reach it to win

---

## Data Structures Summary

| Data Structure | Purpose | Usage |
|---|---|---|
| **Dictionary** | Maze storage (Hash Map) | O(1) cell access |
| **Graph** | Maze connectivity | Pathfinding foundation |
| **Queue** | BFS pathfinding | Monster AI - shortest path |
| **Stack** | DFS pathfinding | Alternative monster AI |
| **List** | High-score storage | Dynamic array of scores |
| **Set/HashSet** | Visited tracking | BFS/DFS cycle prevention |

---

## Algorithm Complexity Analysis

### BFS (MonsterBFS)
- **Time Complexity**: O(V + E) where V = cells, E = edges
  - In maze: O(width × height × 4) ? O(width × height)
- **Space Complexity**: O(width × height) for queue and visited set
- **Pathfinding Quality**: Optimal (shortest path)

### DFS (MonsterDFS)
- **Time Complexity**: O(V + E) but limited to maxDepth
- **Space Complexity**: O(maxDepth) for recursion stack
- **Pathfinding Quality**: Non-optimal (may be much longer)

### Sorting
- **QuickSort**: O(n log n) avg, O(n²) worst case, O(log n) space
- **MergeSort**: O(n log n) guaranteed, O(n) space
- **Built-in Sort**: O(n log n), typically O(1) or O(log n) space

---

## Learning Outcomes

This project demonstrates:
1. **Hash Map/Dictionary usage** for efficient data storage and retrieval
2. **Graph theory** - Maze as a graph with nodes and edges
3. **BFS algorithm** - Finding shortest paths in unweighted graphs
4. **DFS algorithm** - Alternative pathfinding strategy
5. **Queue data structure** - FIFO behavior for level-order traversal
6. **Stack data structure** - LIFO behavior for depth-first traversal
7. **Sorting algorithms** - Multiple approaches and trade-offs
8. **Game loop architecture** - Typical game development patterns
9. **WinForms graphics** - Custom rendering in C#

---

## Future Enhancements

- Add difficulty levels (faster monsters, larger maze)
- Implement multiple maze files
- Add power-ups (temporary invincibility)
- Implement different monster AI strategies (random, hybrid)
- Add sound effects and animations
- Multiplayer support (local co-op)
- Procedural maze generation
- A* pathfinding (heuristic-based)

---

## Technical Notes

- Target Framework: .NET 10
- Platform: Windows (WinForms)
- Language: C# 12
- No external dependencies (uses .NET Framework classes only)
