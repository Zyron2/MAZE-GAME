# API Documentation and Usage Examples

## Maze Class

### Creating and Loading a Maze

```csharp
// Create an empty maze
Maze maze = new Maze(21, 11);

// Load from file
if (maze.LoadFromFile("maze.txt"))
{
    Console.WriteLine("Maze loaded successfully!");
}

// Or create a simple default maze
maze.CreateSimpleMaze();
```

### Accessing Maze Cells (Dictionary/HashMap Usage)

```csharp
// Get a cell (returns char? - nullable char)
char? cell = maze.GetAt(5, 3);
if (cell == '#')
{
    Console.WriteLine("There's a wall here");
}

// Set a cell
maze.SetAt(5, 3, 'P'); // Place player

// Check if walkable
if (maze.IsWalkable(6, 3))
{
    // Can walk here
}
```

### Graph Operations (Getting Neighbors)

```csharp
// Get all walkable neighbors of a position (graph edges)
List<(int x, int y)> neighbors = maze.GetWalkableNeighbors(5, 3);

foreach (var (x, y) in neighbors)
{
    Console.WriteLine($"Can move to ({x}, {y})");
}

// Find a specific character
var playerPos = maze.FindCharacter('P');
if (playerPos.HasValue)
{
    int x = playerPos.Value.x;
    int y = playerPos.Value.y;
}
```

### Rendering

```csharp
// Get maze as ASCII string
string mazeDisplay = maze.GetMazeString();
Console.WriteLine(mazeDisplay);
```

---

## MonsterBFS Class - Queue-based Shortest Path

### Basic Usage

```csharp
// Create a monster with BFS AI at position (5, 5)
MonsterBFS monster = new MonsterBFS(maze, 5, 5);

// Move towards player at (10, 8)
monster.MoveTowardsPlayer(10, 8);

// Get monster position
int monsterX = monster.X;
int monsterY = monster.Y;
```

### BFS Algorithm Breakdown

The BFS algorithm finds the shortest path using a Queue:

```csharp
// Simplified version of the BFS algorithm
Queue<(int x, int y, List<(int x, int y)> path)> queue = 
    new Queue<(int x, int y, List<(int x, int y)> path)>();

HashSet<string> visited = new HashSet<string>();

// Start from monster position
queue.Enqueue((monsterX, monsterY, new List<(int x, int y)> { (monsterX, monsterY) }));
visited.Add($"{monsterX},{monsterY}");

while (queue.Count > 0)
{
    var (currentX, currentY, path) = queue.Dequeue();
    
    // Check if we found player
    if (currentX == playerX && currentY == playerY)
    {
        // path contains the route from monster to player
        // Return path[1] to move one step towards player
        return path[1];
    }
    
    // Explore all walkable neighbors
    foreach (var (nx, ny) in maze.GetWalkableNeighbors(currentX, currentY))
    {
        string key = $"{nx},{ny}";
        if (!visited.Contains(key))
        {
            visited.Add(key);
            var newPath = new List<(int x, int y)>(path);
            newPath.Add((nx, ny));
            queue.Enqueue((nx, ny, newPath));
        }
    }
}
```

**Why Queue for BFS?**
- FIFO order explores all neighbors at distance D before exploring distance D+1
- Guarantees shortest path in unweighted graphs
- Level-order traversal

---

## MonsterDFS Class - Stack-based Pathfinding

### Basic Usage

```csharp
// Create a monster with DFS AI
MonsterDFS monster = new MonsterDFS(maze, 5, 5);

// Move towards player (or randomly if path not found within maxDepth)
monster.MoveTowardsPlayer(10, 8);

// Random movement (if you want a "dumb" monster)
monster.MoveRandomly();
```

### DFS Algorithm Breakdown

```csharp
// Simplified DFS algorithm using Stack
Stack<(int x, int y, List<(int x, int y)> path)> stack = 
    new Stack<(int x, int y, List<(int x, int y)> path)>();

HashSet<string> visited = new HashSet<string>();

stack.Push((monsterX, monsterY, new List<(int x, int y)> { (monsterX, monsterY) }));
visited.Add($"{monsterX},{monsterY}");

while (stack.Count > 0)
{
    var (currentX, currentY, path) = stack.Pop();
    
    // Check if we found player
    if (currentX == playerX && currentY == playerY)
    {
        return path[1]; // Move one step
    }
    
    // Explore neighbors (will be processed in reverse order due to stack)
    foreach (var (nx, ny) in maze.GetWalkableNeighbors(currentX, currentY))
    {
        string key = $"{nx},{ny}";
        if (!visited.Contains(key))
        {
            visited.Add(key);
            var newPath = new List<(int x, int y)>(path);
            newPath.Add((nx, ny));
            stack.Push((nx, ny, newPath)); // LIFO - last added, first explored
        }
    }
}
```

**Why Stack for DFS?**
- LIFO order explores deep into one path before backtracking
- Takes longer, non-optimal paths
- Gets "stuck" in dead ends more often
- Creates less predictable AI

---

## HighScoreTable Class - Sorting Algorithms

### Adding Scores

```csharp
HighScoreTable scoreTable = new HighScoreTable("highscores.txt");

// Add a new score (automatically sorted and saved)
scoreTable.AddScore("Alice", 45.5);
scoreTable.AddScore("Bob", 32.1);
scoreTable.AddScore("Charlie", 78.9);

// Scores are automatically sorted by time (ascending - lower is better)
```

### Using Built-in Sort

```csharp
// Uses .NET's built-in sort (Timsort hybrid)
scoreTable.AddScore("Player", 99.9); // Internally calls scores.Sort()

// Complexity: O(n log n) time, O(1) or O(log n) space (adaptive)
```

### Custom QuickSort Implementation

```csharp
// Use QuickSort algorithm
scoreTable.SortByQuickSort();

// Algorithm breakdown:
// 1. Select a pivot element
// 2. Partition array: elements < pivot on left, > pivot on right
// 3. Recursively sort left and right partitions
// Complexity: O(n log n) average, O(n²) worst case, O(log n) space
```

### Custom MergeSort Implementation

```csharp
// Use MergeSort algorithm
scoreTable.SortByMergeSort();

// Algorithm breakdown:
// 1. Divide array in half recursively until single elements
// 2. Merge sorted halves back together
// 3. Guaranteed O(n log n) performance
// Complexity: O(n log n) guaranteed, O(n) space
```

### Getting Scores

```csharp
// Get scores as list
List<ScoreEntry> scores = scoreTable.GetScores();

// Display formatted scores
string display = scoreTable.GetScoresAsString();
Console.WriteLine(display);
// Output:
// ??? HIGH SCORES ???
//  1. Alice              45.50s 1/15/2024 10:30 AM
//  2. Bob                32.10s 1/15/2024 10:25 AM
//  3. Charlie            78.90s 1/15/2024 10:35 AM
```

---

## GameController Class

### Game Initialization and Control

```csharp
// Create game controller
GameController controller = new GameController();

// Initialize a new game
controller.InitializeGame();

// Move the player
controller.MovePlayer(1, 0);  // Move right
controller.MovePlayer(0, -1); // Move up
controller.MovePlayer(-1, 0); // Move left
controller.MovePlayer(0, 1);  // Move down

// Check game state
GameController.GameState state = controller.GetGameState();
if (state == GameController.GameState.Won)
{
    Console.WriteLine("Player won!");
    
    // Add score to high score table
    controller.AddHighScore("PlayerName");
}
else if (state == GameController.GameState.Lost)
{
    Console.WriteLine("Player lost!");
}
```

### Game Display and Status

```csharp
// Get game as string representation
string gameDisplay = controller.GetGameDisplay();
Console.WriteLine(gameDisplay);

// Get elapsed time
double seconds = controller.GetElapsedTime();
Console.WriteLine($"Elapsed: {seconds:F1}s");

// Get high scores
string scores = controller.GetHighScoresString();
Console.WriteLine(scores);
```

---

## Complete Game Loop Example

```csharp
GameController controller = new GameController();
controller.InitializeGame();

while (controller.GetGameState() == GameController.GameState.Running)
{
    // Display current state
    Console.Clear();
    Console.WriteLine(controller.GetGameDisplay());
    
    // Get input
    ConsoleKey key = Console.ReadKey(true).Key;
    
    // Process input
    switch (key)
    {
        case ConsoleKey.UpArrow:
        case ConsoleKey.W:
            controller.MovePlayer(0, -1);
            break;
        case ConsoleKey.DownArrow:
        case ConsoleKey.S:
            controller.MovePlayer(0, 1);
            break;
        case ConsoleKey.LeftArrow:
        case ConsoleKey.A:
            controller.MovePlayer(-1, 0);
            break;
        case ConsoleKey.RightArrow:
        case ConsoleKey.D:
            controller.MovePlayer(1, 0);
            break;
    }
}

// Game ended
var state = controller.GetGameState();
if (state == GameController.GameState.Won)
{
    Console.WriteLine("YOU WIN!");
    controller.AddHighScore("Player");
}
else
{
    Console.WriteLine("YOU LOST!");
}
```

---

## Performance Considerations

### Maze Storage (Dictionary)
- **Lookup**: O(1) average case
- **Insert/Update**: O(1) average case
- **Memory**: O(width × height)
- **Good for**: Large, sparse mazes

### BFS (Shortest Path)
- **Time**: O(width × height) - visits each cell once
- **Space**: O(width × height) - queue + visited set
- **Best for**: Finding guaranteed shortest paths
- **Worst for**: Large mazes (memory intensive)

### DFS (Alternative Pathfinding)
- **Time**: O(width × height) with depth limit
- **Space**: O(depth) - only call stack
- **Best for**: Memory-constrained environments
- **Worst for**: Finding optimal paths

### Sorting
- **Built-in Sort**: O(n log n), most efficient in practice
- **QuickSort**: O(n log n) average, O(n²) worst case
- **MergeSort**: O(n log n) guaranteed, higher overhead

---

## Tips for Extending the Code

1. **Multiple Monster Types**: Create subclasses with different AI strategies
2. **Power-ups**: Add special maze cells that grant temporary invincibility
3. **Difficulty Levels**: Modify monster speed, maze complexity, number of monsters
4. **Procedural Generation**: Use algorithms to create random mazes
5. **Heuristic Search**: Implement A* using Manhattan distance as heuristic
6. **Performance Optimization**: Use object pooling for frequent allocations
