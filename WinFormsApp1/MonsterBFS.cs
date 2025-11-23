using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsApp1
{
    /// <summary>
    /// Monster AI using Breadth-First Search (BFS) to find the shortest path to the player.
    /// BFS is implemented using a Queue.
    /// This version has been made EASIER with slower movement speed.
    /// </summary>
    public class MonsterBFS
    {
        private Maze maze;
        private int x, y;
        private Queue<(int x, int y, List<(int x, int y)> path)> queue;
        private int moveCounter = 0;
        private const int MOVE_DELAY = 2;  // Monster only moves every 2 player moves

        public MonsterBFS(Maze maze, int startX, int startY)
        {
            this.maze = maze;
            this.x = startX;
            this.y = startY;
            queue = new Queue<(int x, int y, List<(int x, int y)> path)>();
        }

        /// <summary>
        /// Find the shortest path to the player using BFS
        /// </summary>
        public (int x, int y)? FindShortestPathToPlayer(int playerX, int playerY)
        {
            queue.Clear();
            var visited = new HashSet<string>();
            queue.Enqueue((x, y, new List<(int x, int y)> { (x, y) }));
            visited.Add($"{x},{y}");

            while (queue.Count > 0)
            {
                var (currentX, currentY, path) = queue.Dequeue();

                // Found the player
                if (currentX == playerX && currentY == playerY)
                {
                    // Return the next position in the path (move one step towards player)
                    if (path.Count > 1)
                        return path[1];
                    return null;
                }

                // Explore neighbors
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

            return null; // Player not found
        }

        /// <summary>
        /// Move the monster one step towards the player (with delay for easier gameplay)
        /// </summary>
        public void MoveTowardsPlayer(int playerX, int playerY)
        {
            // Monster only moves every MOVE_DELAY player moves
            moveCounter++;
            if (moveCounter < MOVE_DELAY)
                return;

            moveCounter = 0;

            var nextPos = FindShortestPathToPlayer(playerX, playerY);
            if (nextPos.HasValue)
            {
                x = nextPos.Value.x;
                y = nextPos.Value.y;
            }
        }

        public int X => x;
        public int Y => y;
    }
}
