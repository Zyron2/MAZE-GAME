using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsApp1
{
    /// <summary>
    /// Represents the maze using a Map (Dictionary) data structure.
    /// Key: "x,y" coordinate string
    /// Value: Character at that position ('#', ' ', 'P', 'E', 'M')
    /// </summary>
    public class Maze
    {
        private Dictionary<string, char> mazeMap;
        private int width;
        private int height;

        public Maze(int width, int height)
        {
            this.width = width;
            this.height = height;
            mazeMap = new Dictionary<string, char>();
        }

        /// <summary>
        /// Load maze from a text file
        /// </summary>
        public bool LoadFromFile(string filePath)
        {
            try
            {
                mazeMap.Clear();
                var lines = System.IO.File.ReadAllLines(filePath);
                
                height = lines.Length;
                width = lines.Length > 0 ? lines[0].Length : 0;

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < lines[y].Length; x++)
                    {
                        string key = $"{x},{y}";
                        mazeMap[key] = lines[y][x];
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading maze: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Create a Pac-Man style compact maze with corridors and intersections
        /// </summary>
        public void CreateSimpleMaze()
        {
            mazeMap.Clear();
            width = 27;
            height = 15;

            // Initialize all cells as walkable
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    string key = $"{x},{y}";
                    mazeMap[key] = ' ';
                }
            }

            // Add border walls
            for (int x = 0; x < width; x++)
            {
                mazeMap[$"{x},0"] = '#';
                mazeMap[$"{x},{height - 1}"] = '#';
            }
            for (int y = 0; y < height; y++)
            {
                mazeMap[$"0,{y}"] = '#';
                mazeMap[$"{width - 1},{y}"] = '#';
            }

            // Pac-Man style corridors - vertical walls creating vertical passages
            // Left section - two vertical corridors
            for (int y = 1; y < height - 1; y++)
            {
                mazeMap[$"5,{y}"] = '#';
                mazeMap[$"13,{y}"] = '#';
            }

            // Create passages in vertical walls
            mazeMap["5,3"] = ' ';   // Open passage
            mazeMap["5,7"] = ' ';   // Open passage
            mazeMap["5,11"] = ' ';  // Open passage
            mazeMap["13,3"] = ' ';  // Open passage
            mazeMap["13,7"] = ' ';  // Open passage
            mazeMap["13,11"] = ' '; // Open passage

            // Horizontal corridor walls
            for (int x = 1; x < width - 1; x++)
            {
                if (x != 5 && x != 13)  // Don't block at vertical walls
                {
                    mazeMap[$"{x},5"] = '#';
                    mazeMap[$"{x},9"] = '#';
                }
            }

            // Create passages in horizontal walls
            mazeMap["2,5"] = ' ';
            mazeMap["8,5"] = ' ';
            mazeMap["15,5"] = ' ';
            mazeMap["22,5"] = ' ';
            
            mazeMap["2,9"] = ' ';
            mazeMap["8,9"] = ' ';
            mazeMap["15,9"] = ' ';
            mazeMap["22,9"] = ' ';

            // Add small corner rooms with walls
            // Top-left corner room
            for (int x = 2; x <= 4; x++)
            {
                mazeMap[$"{x},2"] = '#';
            }

            // Top-right corner room
            for (int x = 22; x <= 24; x++)
            {
                mazeMap[$"{x},2"] = '#';
            }

            // Bottom-left corner room
            for (int x = 2; x <= 4; x++)
            {
                mazeMap[$"{x},12"] = '#';
            }

            // Bottom-right corner room
            for (int x = 22; x <= 24; x++)
            {
                mazeMap[$"{x},12"] = '#';
            }

            // Center area with walls
            for (int x = 11; x <= 15; x++)
            {
                mazeMap[$"{x},6"] = '#';
                mazeMap[$"{x},8"] = '#';
            }

            // Open passages in center
            mazeMap["11,6"] = ' ';
            mazeMap["11,8"] = ' ';
            mazeMap["15,6"] = ' ';
            mazeMap["15,8"] = ' ';

            // Place player at start (left side)
            mazeMap["2,7"] = 'P';

            // Place exit (right side)
            mazeMap[$"{width - 3},7"] = 'E';

            // Place ONE monster in center area
            mazeMap["13,7"] = 'M';
        }

        /// <summary>
        /// Get the character at a specific coordinate
        /// </summary>
        public char? GetAt(int x, int y)
        {
            string key = $"{x},{y}";
            return mazeMap.ContainsKey(key) ? mazeMap[key] : null;
        }

        /// <summary>
        /// Set the character at a specific coordinate
        /// </summary>
        public void SetAt(int x, int y, char value)
        {
            string key = $"{x},{y}";
            mazeMap[key] = value;
        }

        /// <summary>
        /// Check if a position is walkable (not a wall)
        /// </summary>
        public bool IsWalkable(int x, int y)
        {
            char? cell = GetAt(x, y);
            return cell.HasValue && cell != '#';
        }

        /// <summary>
        /// Get all walkable neighbors of a position (for graph traversal)
        /// </summary>
        public List<(int x, int y)> GetWalkableNeighbors(int x, int y)
        {
            var neighbors = new List<(int x, int y)>();
            var directions = new[] { (0, -1), (0, 1), (-1, 0), (1, 0) }; // Up, Down, Left, Right

            foreach (var (dx, dy) in directions)
            {
                int nx = x + dx;
                int ny = y + dy;
                if (IsWalkable(nx, ny))
                {
                    neighbors.Add((nx, ny));
                }
            }

            return neighbors;
        }

        /// <summary>
        /// Find position of a specific character
        /// </summary>
        public (int x, int y)? FindCharacter(char character)
        {
            foreach (var kvp in mazeMap)
            {
                if (kvp.Value == character)
                {
                    var coords = kvp.Key.Split(',');
                    return (int.Parse(coords[0]), int.Parse(coords[1]));
                }
            }
            return null;
        }

        /// <summary>
        /// Get the entire maze as a string for rendering
        /// </summary>
        public string GetMazeString()
        {
            var result = new System.Text.StringBuilder();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    char? cell = GetAt(x, y);
                    result.Append(cell ?? ' ');
                }
                result.AppendLine();
            }
            return result.ToString();
        }

        public int Width => width;
        public int Height => height;
        public Dictionary<string, char> MazeMap => mazeMap;
    }
}
