using System;
using System.Collections.Generic;
using System.Linq;

namespace WinFormsApp1
{
    /// <summary>
    /// Represents a high-score entry with completion time
    /// </summary>
    public class ScoreEntry : IComparable<ScoreEntry>
    {
        public string PlayerName { get; set; }
        public double Time { get; set; } // in seconds
        public DateTime Date { get; set; }

        public ScoreEntry(string playerName, double time)
        {
            PlayerName = playerName;
            Time = time;
            Date = DateTime.Now;
        }

        public int CompareTo(ScoreEntry other)
        {
            // Sort by time (ascending - lower times are better)
            return this.Time.CompareTo(other.Time);
        }

        public override string ToString()
        {
            return $"{PlayerName,-20} {Time:F2}s {Date:g}";
        }
    }

    /// <summary>
    /// High-score table with sorting capabilities.
    /// Demonstrates sorting algorithms for game completion times.
    /// Keeps a COMPLETE HISTORY of all game attempts.
    /// </summary>
    public class HighScoreTable
    {
        private List<ScoreEntry> scores;
        private List<ScoreEntry> allScores;  // ALL scores - complete history
        private const int MAX_TOP_SCORES = 10;  // Top 10 for display
        private string filePath;
        private string allScoresFilePath;

        public HighScoreTable(string filePath = "highscores.txt", string allScoresFilePath = "all_scores.txt")
        {
            this.filePath = filePath;
            this.allScoresFilePath = allScoresFilePath;
            scores = new List<ScoreEntry>();
            allScores = new List<ScoreEntry>();
            LoadScores();
        }

        /// <summary>
        /// Add a new score to the table
        /// </summary>
        public void AddScore(string playerName, double time)
        {
            var newScore = new ScoreEntry(playerName, time);
            
            // Add to all scores (complete history)
            allScores.Add(newScore);
            
            // Add to top scores list
            scores.Add(newScore);
            scores.Sort(); // Use built-in sorting (typically uses QuickSort or MergeSort)
            
            // Keep only top scores for the leaderboard
            if (scores.Count > MAX_TOP_SCORES)
            {
                scores = scores.Take(MAX_TOP_SCORES).ToList();
            }

            // Save both lists
            SaveTopScores();
            SaveAllScores();
        }

        /// <summary>
        /// Get all scores (complete history)
        /// </summary>
        public List<ScoreEntry> GetAllScores()
        {
            return new List<ScoreEntry>(allScores);
        }

        /// <summary>
        /// Get only top scores (leaderboard)
        /// </summary>
        public List<ScoreEntry> GetTopScores()
        {
            return new List<ScoreEntry>(scores);
        }

        /// <summary>
        /// Get all scores
        /// </summary>
        public List<ScoreEntry> GetScores()
        {
            return new List<ScoreEntry>(scores);
        }

        /// <summary>
        /// Get formatted string representation of TOP scores (leaderboard)
        /// </summary>
        public string GetScoresAsString()
        {
            if (scores.Count == 0)
                return "No high scores yet!";

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("??? TOP 10 HIGH SCORES ???");
            for (int i = 0; i < scores.Count; i++)
            {
                sb.AppendLine($"{i + 1,2}. {scores[i]}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// Get formatted string representation of ALL scores (complete history)
        /// </summary>
        public string GetAllScoresAsString()
        {
            if (allScores.Count == 0)
                return "No game history yet!";

            var sb = new System.Text.StringBuilder();
            sb.AppendLine("??? COMPLETE GAME HISTORY ???");
            sb.AppendLine($"Total Games Played: {allScores.Count}");
            sb.AppendLine("?????????????????????????????");
            
            // Sort all scores for display
            var sortedAll = allScores.OrderBy(s => s.Time).ToList();
            
            for (int i = 0; i < sortedAll.Count; i++)
            {
                sb.AppendLine($"{i + 1,3}. {sortedAll[i]}");
            }

            // Calculate and display statistics
            double avgTime = allScores.Average(s => s.Time);
            double minTime = allScores.Min(s => s.Time);
            double maxTime = allScores.Max(s => s.Time);
            
            sb.AppendLine("?????????????????????????????");
            sb.AppendLine($"Average Time: {avgTime:F2}s");
            sb.AppendLine($"Best Time:    {minTime:F2}s");
            sb.AppendLine($"Worst Time:   {maxTime:F2}s");
            
            return sb.ToString();
        }

        /// <summary>
        /// Get game statistics
        /// </summary>
        public (int totalGames, double avgTime, double bestTime, double worstTime) GetStatistics()
        {
            if (allScores.Count == 0)
                return (0, 0, 0, 0);

            return (
                allScores.Count,
                allScores.Average(s => s.Time),
                allScores.Min(s => s.Time),
                allScores.Max(s => s.Time)
            );
        }

        /// <summary>
        /// Custom QuickSort implementation for educational purposes
        /// </summary>
        public void SortByQuickSort()
        {
            QuickSort(scores, 0, scores.Count - 1);
        }

        private void QuickSort(List<ScoreEntry> list, int low, int high)
        {
            if (low < high)
            {
                int pi = Partition(list, low, high);
                QuickSort(list, low, pi - 1);
                QuickSort(list, pi + 1, high);
            }
        }

        private int Partition(List<ScoreEntry> list, int low, int high)
        {
            ScoreEntry pivot = list[high];
            int i = low - 1;

            for (int j = low; j < high; j++)
            {
                if (list[j].CompareTo(pivot) < 0)
                {
                    i++;
                    (list[i], list[j]) = (list[j], list[i]);
                }
            }
            (list[i + 1], list[high]) = (list[high], list[i + 1]);
            return i + 1;
        }

        /// <summary>
        /// Custom MergeSort implementation for educational purposes
        /// </summary>
        public void SortByMergeSort()
        {
            scores = MergeSort(scores);
        }

        private List<ScoreEntry> MergeSort(List<ScoreEntry> list)
        {
            if (list.Count <= 1)
                return list;

            int mid = list.Count / 2;
            List<ScoreEntry> left = MergeSort(list.Take(mid).ToList());
            List<ScoreEntry> right = MergeSort(list.Skip(mid).ToList());

            return Merge(left, right);
        }

        private List<ScoreEntry> Merge(List<ScoreEntry> left, List<ScoreEntry> right)
        {
            var result = new List<ScoreEntry>();
            int i = 0, j = 0;

            while (i < left.Count && j < right.Count)
            {
                if (left[i].CompareTo(right[j]) <= 0)
                {
                    result.Add(left[i++]);
                }
                else
                {
                    result.Add(right[j++]);
                }
            }

            result.AddRange(left.Skip(i));
            result.AddRange(right.Skip(j));
            return result;
        }

        /// <summary>
        /// Save top scores to file
        /// </summary>
        private void SaveTopScores()
        {
            try
            {
                var lines = scores.Select(s => $"{s.PlayerName}|{s.Time}|{s.Date}");
                System.IO.File.WriteAllLines(filePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving top scores: {ex.Message}");
            }
        }

        /// <summary>
        /// Save ALL scores to file (complete history)
        /// </summary>
        private void SaveAllScores()
        {
            try
            {
                var lines = allScores.Select(s => $"{s.PlayerName}|{s.Time}|{s.Date}");
                System.IO.File.WriteAllLines(allScoresFilePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving all scores: {ex.Message}");
            }
        }

        /// <summary>
        /// Load scores from files
        /// </summary>
        private void LoadScores()
        {
            try
            {
                // Load top scores
                if (System.IO.File.Exists(filePath))
                {
                    var lines = System.IO.File.ReadAllLines(filePath);
                    foreach (var line in lines)
                    {
                        var parts = line.Split('|');
                        if (parts.Length >= 2 && double.TryParse(parts[1], out double time))
                        {
                            scores.Add(new ScoreEntry(parts[0], time));
                        }
                    }
                    scores.Sort();
                }

                // Load all scores (complete history)
                if (System.IO.File.Exists(allScoresFilePath))
                {
                    var lines = System.IO.File.ReadAllLines(allScoresFilePath);
                    foreach (var line in lines)
                    {
                        var parts = line.Split('|');
                        if (parts.Length >= 2 && double.TryParse(parts[1], out double time))
                        {
                            allScores.Add(new ScoreEntry(parts[0], time));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading scores: {ex.Message}");
            }
        }
    }
}
