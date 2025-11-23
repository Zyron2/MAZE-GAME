using System;
using System.Collections.Generic;

namespace WinFormsApp1
{
    /// <summary>
    /// Example code demonstrating all data structures and algorithms
    /// Used for testing and learning purposes
    /// </summary>
    public class ExampleUsage
    {
        public static void DemonstrateMaze()
        {
            Console.WriteLine("=== MAZE DEMO (Dictionary/HashMap) ===\n");
            
            Maze maze = new Maze(21, 11);
            maze.CreateSimpleMaze();
            
            Console.WriteLine("Maze created with Dictionary<string, char>:");
            Console.WriteLine("Key format: \"x,y\" | Value: cell character\n");
            
            // Display maze
            Console.WriteLine(maze.GetMazeString());
            
            // Demonstrate dictionary operations
            Console.WriteLine("Maze dimensions: " + maze.Width + "x" + maze.Height);
            Console.WriteLine("Total cells in dictionary: " + maze.MazeMap.Count + "\n");
            
            // Find positions
            var playerPos = maze.FindCharacter('P');
            var exitPos = maze.FindCharacter('E');
            
            if (playerPos.HasValue)
                Console.WriteLine($"Player found at: {playerPos.Value}");
            if (exitPos.HasValue)
                Console.WriteLine($"Exit found at: {exitPos.Value}");
        }

        public static void DemonstrateBFS()
        {
            Console.WriteLine("\n=== BFS DEMO (Queue-based Monster AI) ===\n");
            
            Maze maze = new Maze(21, 11);
            maze.CreateSimpleMaze();
            
            // Create a monster using BFS
            MonsterBFS monster = new MonsterBFS(maze, 5, 5);
            int playerX = 1, playerY = 1;
            
            Console.WriteLine("BFS Monster starting at (5, 5)");
            Console.WriteLine($"Player at ({playerX}, {playerY})");
            Console.WriteLine("Monster will find shortest path using Queue (BFS)\n");
            
            // Simulate movement towards player
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Step {i + 1}: Monster at ({monster.X}, {monster.Y})");
                monster.MoveTowardsPlayer(playerX, playerY);
            }
            
            Console.WriteLine($"\nFinal monster position: ({monster.X}, {monster.Y})");
            Console.WriteLine("BFS guarantees shortest path!");
        }

        public static void DemonstrateDFS()
        {
            Console.WriteLine("\n=== DFS DEMO (Stack-based Monster AI) ===\n");
            
            Maze maze = new Maze(21, 11);
            maze.CreateSimpleMaze();
            
            // Create a monster using DFS
            MonsterDFS monster = new MonsterDFS(maze, 5, 5);
            int playerX = 1, playerY = 1;
            
            Console.WriteLine("DFS Monster starting at (5, 5)");
            Console.WriteLine($"Player at ({playerX}, {playerY})");
            Console.WriteLine("Monster uses Stack (DFS) - may take longer paths\n");
            
            // Simulate movement
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Step {i + 1}: Monster at ({monster.X}, {monster.Y})");
                monster.MoveTowardsPlayer(playerX, playerY);
            }
        }

        public static void DemonstrateHighScores()
        {
            Console.WriteLine("\n=== HIGH SCORE TABLE DEMO (Sorting) ===\n");
            
            HighScoreTable scoreTable = new HighScoreTable("example_scores.txt");
            
            // Add some scores
            scoreTable.AddScore("Alice", 45.5);
            scoreTable.AddScore("Bob", 32.1);
            scoreTable.AddScore("Charlie", 78.9);
            scoreTable.AddScore("Diana", 28.4);
            scoreTable.AddScore("Eve", 56.2);
            
            Console.WriteLine("Added 5 scores to the table:");
            Console.WriteLine(scoreTable.GetScoresAsString());
            
            Console.WriteLine("\n--- Using QuickSort ---");
            scoreTable.SortByQuickSort();
            Console.WriteLine("Sorted using QuickSort algorithm");
            
            Console.WriteLine("\n--- Using MergeSort ---");
            scoreTable.SortByMergeSort();
            Console.WriteLine("Sorted using MergeSort algorithm");
        }

        public static void DemonstrateGameController()
        {
            Console.WriteLine("\n=== GAME CONTROLLER DEMO ===\n");
            
            GameController controller = new GameController();
            controller.InitializeGame();
            
            Console.WriteLine("Game initialized!");
            Console.WriteLine($"State: {controller.GetGameState()}");
            Console.WriteLine($"Elapsed time: {controller.GetElapsedTime():F2}s");
            
            // Simulate some moves
            Console.WriteLine("\nSimulating player movement...");
            for (int i = 0; i < 5; i++)
            {
                controller.MovePlayer(1, 0); // Move right
                Console.WriteLine($"Move {i + 1}: Player moved right");
                
                if (controller.GetGameState() != GameController.GameState.Running)
                {
                    Console.WriteLine($"Game ended: {controller.GetGameState()}");
                    break;
                }
            }
            
            Console.WriteLine($"\nFinal game state: {controller.GetGameState()}");
            Console.WriteLine($"Total elapsed: {controller.GetElapsedTime():F2}s");
        }

        public static void DemonstrateAlgorithmComplexity()
        {
            Console.WriteLine("\n=== ALGORITHM COMPLEXITY SUMMARY ===\n");
            
            Console.WriteLine("MAZE STORAGE (Dictionary/HashMap):");
            Console.WriteLine("  Operation    | Time      | Space");
            Console.WriteLine("  -------------|-----------|----------");
            Console.WriteLine("  Get Cell     | O(1)      | -");
            Console.WriteLine("  Set Cell     | O(1)      | -");
            Console.WriteLine("  Total        | -         | O(w×h)");
            
            Console.WriteLine("\nBFS (Queue-based Pathfinding):");
            Console.WriteLine("  Time:  O(width × height) - visits each cell once");
            Console.WriteLine("  Space: O(width × height) - queue + visited set");
            Console.WriteLine("  Quality: OPTIMAL (guaranteed shortest path)");
            
            Console.WriteLine("\nDFS (Stack-based Pathfinding):");
            Console.WriteLine("  Time:  O(width × height) limited to depth");
            Console.WriteLine("  Space: O(depth) - recursion/stack only");
            Console.WriteLine("  Quality: NON-OPTIMAL (takes longer paths)");
            
            Console.WriteLine("\nSORT ALGORITHMS (for high scores):");
            Console.WriteLine("  Algorithm | Time Avg    | Time Worst | Space");
            Console.WriteLine("  ----------|-------------|------------|----------");
            Console.WriteLine("  QuickSort | O(n log n)  | O(n²)      | O(log n)");
            Console.WriteLine("  MergeSort | O(n log n)  | O(n log n) | O(n)");
            Console.WriteLine("  Built-in  | O(n log n)  | O(n log n) | O(1/log n)");
        }

        public static void DemonstrateGraphConcepts()
        {
            Console.WriteLine("\n=== GRAPH CONCEPTS IN MAZE ===\n");
            
            Maze maze = new Maze(7, 5);
            maze.CreateSimpleMaze();
            
            Console.WriteLine("Maze as a Graph:");
            Console.WriteLine("- Nodes: Walkable cells (empty spaces, player, exit)");
            Console.WriteLine("- Edges: Adjacency between walkable cells\n");
            
            // Show neighbors for a specific cell
            int testX = 3, testY = 2;
            var neighbors = maze.GetWalkableNeighbors(testX, testY);
            
            Console.WriteLine($"Neighbors of cell ({testX}, {testY}):");
            foreach (var (x, y) in neighbors)
            {
                Console.WriteLine($"  - ({x}, {y})");
            }
            
            Console.WriteLine("\nGraph properties:");
            Console.WriteLine("- Unweighted: All moves cost 1");
            Console.WriteLine("- Grid-based: Max 4 neighbors per cell");
            Console.WriteLine("- Connected components: Separate areas isolated by walls");
        }

        /// <summary>
        /// Run this to see demonstrations of all data structures and algorithms
        /// Usage: DemonstrateAllFeaturesConsole.DemonstrateMaze();
        /// </summary>
        public static void RunAllDemonstrations()
        {
            Console.WriteLine("??????????????????????????????????????????????????????????????????");
            Console.WriteLine("?         MAZE GAME - DATA STRUCTURES & ALGORITHMS DEMO           ?");
            Console.WriteLine("??????????????????????????????????????????????????????????????????");
            
            try
            {
                DemonstrateMaze();
                DemonstrateGraphConcepts();
                DemonstrateBFS();
                DemonstrateDFS();
                DemonstrateHighScores();
                DemonstrateGameController();
                DemonstrateAlgorithmComplexity();
                
                Console.WriteLine("\n" + new string('?', 64));
                Console.WriteLine("Demo completed successfully!");
                Console.WriteLine(new string('?', 64));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during demo: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
