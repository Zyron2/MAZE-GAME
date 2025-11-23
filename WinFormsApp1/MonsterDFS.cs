using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsApp1
{
    /// <summary>
    /// Monster AI using Depth-First Search (DFS) to pathfind.
    /// DFS is implemented using a Stack. This creates a "dumber" monster
    /// that takes strange paths and gets lost in dead ends.
    /// </summary>
    public class MonsterDFS
    {
        private Maze maze;
        private int x, y;
        private Stack<(int x, int y, List<(int x, int y)> path)> stack;

        public MonsterDFS(Maze maze, int startX, int startY)
        {
            this.maze = maze;
            this.x = startX;
            this.y = startY;
            stack = new Stack<(int x, int y, List<(int x, int y)> path)>();
        }

        /// <summary>
        /// Find a path to the player using DFS (not necessarily shortest)
        /// </summary>
        public (int x, int y)? FindPathToPlayer(int playerX, int playerY, int maxDepth = 50)
        {
            stack.Clear();
            var visited = new HashSet<string>();
            stack.Push((x, y, new List<(int x, int y)> { (x, y) }));
            visited.Add($"{x},{y}");
            int explorations = 0;

            while (stack.Count > 0 && explorations < maxDepth)
            {
                explorations++;
                var (currentX, currentY, path) = stack.Pop();

                // Found the player
                if (currentX == playerX && currentY == playerY)
                {
                    // Return the next position in the path (move one step towards player)
                    if (path.Count > 1)
                        return path[1];
                    return null;
                }

                // Explore neighbors (in reverse order so they're processed in order when popped)
                var neighbors = maze.GetWalkableNeighbors(currentX, currentY).ToList();
                for (int i = neighbors.Count - 1; i >= 0; i--)
                {
                    var (nx, ny) = neighbors[i];
                    string key = $"{nx},{ny}";
                    if (!visited.Contains(key))
                    {
                        visited.Add(key);
                        var newPath = new List<(int x, int y)>(path);
                        newPath.Add((nx, ny));
                        stack.Push((nx, ny, newPath));
                    }
                }
            }

            return null; // Player not found within maxDepth
        }

        /// <summary>
        /// Move the monster one step (using DFS pathfinding)
        /// </summary>
        public void MoveRandomly()
        {
            // If DFS path not found, just move randomly to a neighbor
            var neighbors = maze.GetWalkableNeighbors(x, y);
            if (neighbors.Count > 0)
            {
                var random = new Random();
                var nextPos = neighbors[random.Next(neighbors.Count)];
                x = nextPos.x;
                y = nextPos.y;
            }
        }

        /// <summary>
        /// Move the monster one step towards the player using DFS
        /// </summary>
        public void MoveTowardsPlayer(int playerX, int playerY)
        {
            var nextPos = FindPathToPlayer(playerX, playerY);
            if (nextPos.HasValue)
            {
                x = nextPos.Value.x;
                y = nextPos.Value.y;
            }
            else
            {
                MoveRandomly();
            }
        }

        public int X => x;
        public int Y => y;
    }
}
