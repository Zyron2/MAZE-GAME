using System;
using System.Collections.Generic;

namespace WinFormsApp1
{
    /// <summary>
    /// Main game controller managing game logic, state, and flow
    /// </summary>
    public class GameController
    {
        public enum GameState { Running, Won, Lost }

        private Maze maze;
        private int playerX, playerY;
        private List<MonsterBFS> monsters;
        private int exitX, exitY;
        private GameState gameState;
        private DateTime startTime;
        private HighScoreTable scoreTable;

        public GameController()
        {
            maze = new Maze(27, 15);
            monsters = new List<MonsterBFS>();
            scoreTable = new HighScoreTable();
        }

        /// <summary>
        /// Initialize a new game
        /// </summary>
        public void InitializeGame()
        {
            maze.CreateSimpleMaze();
            gameState = GameState.Running;
            startTime = DateTime.Now;

            // Find player position
            var playerPos = maze.FindCharacter('P');
            if (playerPos.HasValue)
            {
                playerX = playerPos.Value.x;
                playerY = playerPos.Value.y;
            }

            // Find exit position
            var exitPos = maze.FindCharacter('E');
            if (exitPos.HasValue)
            {
                exitX = exitPos.Value.x;
                exitY = exitPos.Value.y;
            }

            // Create monsters at 'M' positions
            monsters.Clear();
            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    if (maze.GetAt(x, y) == 'M')
                    {
                        monsters.Add(new MonsterBFS(maze, x, y));
                    }
                }
            }
        }

        /// <summary>
        /// Move the player in a direction
        /// </summary>
        public void MovePlayer(int dx, int dy)
        {
            if (gameState != GameState.Running)
                return;

            int newX = playerX + dx;
            int newY = playerY + dy;

            if (maze.IsWalkable(newX, newY))
            {
                // Clear old position
                maze.SetAt(playerX, playerY, ' ');

                // Update position
                playerX = newX;
                playerY = newY;

                // Check win condition
                if (playerX == exitX && playerY == exitY)
                {
                    gameState = GameState.Won;
                    return;
                }

                // Update maze display
                maze.SetAt(playerX, playerY, 'P');

                // Move monsters
                UpdateMonsters();

                // Check lose condition
                CheckMonsterCollision();
            }
        }

        /// <summary>
        /// Update all monster positions
        /// </summary>
        private void UpdateMonsters()
        {
            foreach (var monster in monsters)
            {
                // Remove monster from old position
                maze.SetAt(monster.X, monster.Y, ' ');

                // Move towards player
                monster.MoveTowardsPlayer(playerX, playerY);

                // Update maze
                maze.SetAt(monster.X, monster.Y, 'M');
            }
        }

        /// <summary>
        /// Check if player collided with any monster
        /// </summary>
        private void CheckMonsterCollision()
        {
            foreach (var monster in monsters)
            {
                if (monster.X == playerX && monster.Y == playerY)
                {
                    gameState = GameState.Lost;
                    break;
                }
            }
        }

        /// <summary>
        /// Get current game state as string for display
        /// </summary>
        public string GetGameDisplay()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(maze.GetMazeString());
            
            double elapsed = DateTime.Now.Subtract(startTime).TotalSeconds;
            sb.AppendLine($"Time: {elapsed:F1}s");

            return sb.ToString();
        }

        /// <summary>
        /// Get the current game state
        /// </summary>
        public GameState GetGameState() => gameState;

        /// <summary>
        /// Get elapsed time in seconds
        /// </summary>
        public double GetElapsedTime()
        {
            return DateTime.Now.Subtract(startTime).TotalSeconds;
        }

        /// <summary>
        /// Add score to high score table
        /// </summary>
        public void AddHighScore(string playerName)
        {
            scoreTable.AddScore(playerName, GetElapsedTime());
        }

        /// <summary>
        /// Get high scores as string (Top 10 Leaderboard)
        /// </summary>
        public string GetHighScoresString()
        {
            return scoreTable.GetScoresAsString();
        }

        /// <summary>
        /// Get ALL scores as string (Complete game history)
        /// </summary>
        public string GetAllScoresString()
        {
            return scoreTable.GetAllScoresAsString();
        }

        /// <summary>
        /// Get game statistics
        /// </summary>
        public (int totalGames, double avgTime, double bestTime, double worstTime) GetGameStatistics()
        {
            return scoreTable.GetStatistics();
        }

        /// <summary>
        /// Get all score objects
        /// </summary>
        public List<ScoreEntry> GetAllScores()
        {
            return scoreTable.GetAllScores();
        }

        /// <summary>
        /// Get top score objects
        /// </summary>
        public List<ScoreEntry> GetTopScores()
        {
            return scoreTable.GetTopScores();
        }

        public Maze Maze => maze;
        public int PlayerX => playerX;
        public int PlayerY => playerY;
        public List<MonsterBFS> Monsters => monsters;
        public HighScoreTable ScoreTable => scoreTable;
    }
}
