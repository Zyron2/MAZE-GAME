using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private GameController gameController;
        private System.Windows.Forms.Timer gameTimer;
        private const int CELL_SIZE = 20; // pixels per maze cell (perfect for Pac-Man style maze)

        public Form1()
        {
            InitializeComponent();
            this.Text = "Pac-Man Style Maze Game";
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;

            gameController = new GameController();
            gameController.InitializeGame();

            // Setup game timer for rendering updates
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 100; // Update every 100ms
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            this.Paint += Form1_Paint;
            this.DoubleBuffered = true;

            // Size the window to fit the maze nicely
            int mazePixelWidth = gameController.Maze.Width * CELL_SIZE + 40;
            int mazePixelHeight = gameController.Maze.Height * CELL_SIZE + 100;
            this.ClientSize = new Size(mazePixelWidth, mazePixelHeight);
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            this.Invalidate(); // Trigger paint
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameController.GetGameState() != GameController.GameState.Running)
            {
                if (e.KeyCode == Keys.R)
                {
                    ResetGame();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.H)
                {
                    ShowHighScores();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.A)  // A for All scores
                {
                    ShowAllScores();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.S)  // S for Statistics
                {
                    ShowStatistics();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.N)  // N for save score (on win)
                {
                    SaveGameScore();
                    e.Handled = true;
                }
                return;
            }

            // Arrow keys or WASD for movement
            switch (e.KeyCode)
            {
                case Keys.Up:
                case Keys.W:
                    gameController.MovePlayer(0, -1);
                    e.Handled = true;
                    break;
                case Keys.Down:
                case Keys.S:
                    gameController.MovePlayer(0, 1);
                    e.Handled = true;
                    break;
                case Keys.Left:
                case Keys.A:
                    gameController.MovePlayer(-1, 0);
                    e.Handled = true;
                    break;
                case Keys.Right:
                case Keys.D:
                    gameController.MovePlayer(1, 0);
                    e.Handled = true;
                    break;
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);

            // Draw maze
            DrawMaze(e.Graphics);

            // Draw game status
            DrawGameStatus(e.Graphics);

            // Draw game over message
            if (gameController.GetGameState() != GameController.GameState.Running)
            {
                DrawGameOverMessage(e.Graphics);
            }
        }

        private void DrawMaze(Graphics g)
        {
            var maze = gameController.Maze;
            
            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    char? cell = maze.GetAt(x, y);
                    if (!cell.HasValue)
                        continue;

                    int screenX = x * CELL_SIZE + 20;
                    int screenY = y * CELL_SIZE + 20;

                    switch (cell.Value)
                    {
                        case '#': // Wall
                            g.FillRectangle(Brushes.DarkSlateGray, screenX, screenY, CELL_SIZE, CELL_SIZE);
                            // Draw border around wall for visibility
                            g.DrawRectangle(Pens.Black, screenX, screenY, CELL_SIZE, CELL_SIZE);
                            break;
                        case 'P': // Player
                            g.FillRectangle(Brushes.LimeGreen, screenX, screenY, CELL_SIZE, CELL_SIZE);
                            g.DrawRectangle(Pens.DarkGreen, screenX, screenY, CELL_SIZE, CELL_SIZE);
                            g.DrawString("P", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, screenX + 2, screenY + 1);
                            break;
                        case 'E': // Exit
                            g.FillRectangle(Brushes.Gold, screenX, screenY, CELL_SIZE, CELL_SIZE);
                            g.DrawRectangle(Pens.DarkGoldenrod, screenX, screenY, CELL_SIZE, CELL_SIZE);
                            g.DrawString("E", new Font("Arial", 12, FontStyle.Bold), Brushes.Black, screenX + 2, screenY + 1);
                            break;
                        case 'M': // Monster
                            g.FillRectangle(Brushes.Red, screenX, screenY, CELL_SIZE, CELL_SIZE);
                            g.DrawRectangle(Pens.DarkRed, screenX, screenY, CELL_SIZE, CELL_SIZE);
                            g.DrawString("M", new Font("Arial", 12, FontStyle.Bold), Brushes.White, screenX + 2, screenY + 1);
                            break;
                        case ' ': // Empty
                            g.FillRectangle(Brushes.White, screenX, screenY, CELL_SIZE, CELL_SIZE);
                            g.DrawRectangle(Pens.LightGray, screenX, screenY, CELL_SIZE, CELL_SIZE);
                            break;
                    }
                }
            }
        }

        private void DrawGameStatus(Graphics g)
        {
            int mazePixelHeight = gameController.Maze.Height * CELL_SIZE + 40;
            double elapsedTime = gameController.GetElapsedTime();
            
            var statusFont = new Font("Arial", 12, FontStyle.Bold);
            g.DrawString($"Time: {elapsedTime:F1}s", statusFont, Brushes.White, 20, mazePixelHeight);
            g.DrawString("Controls: Arrow Keys or WASD | R: Restart | H: High Scores | A: All Scores | S: Stats", new Font("Arial", 9), Brushes.White, 20, mazePixelHeight + 25);
        }

        private void DrawGameOverMessage(Graphics g)
        {
            var gameState = gameController.GetGameState();
            string message = gameState == GameController.GameState.Won ? "YOU WIN!" : "YOU LOST!";
            Color messageColor = gameState == GameController.GameState.Won ? Color.Gold : Color.Red;

            var font = new Font("Arial", 48, FontStyle.Bold);
            var brush = new SolidBrush(messageColor);
            
            var size = g.MeasureString(message, font);
            float x = (this.ClientSize.Width - size.Width) / 2;
            float y = (this.ClientSize.Height - size.Height) / 2 - 50;

            // Draw semi-transparent background
            g.FillRectangle(new SolidBrush(Color.FromArgb(220, 0, 0, 0)), 
                x - 30, y - 30, size.Width + 60, size.Height + 60);

            g.DrawString(message, font, brush, x, y);

            // Draw time completed
            double elapsedTime = gameController.GetElapsedTime();
            var timeFont = new Font("Arial", 20, FontStyle.Bold);
            string timeText = $"Time: {elapsedTime:F2}s";
            var timeSize = g.MeasureString(timeText, timeFont);
            g.DrawString(timeText, timeFont, Brushes.Yellow, 
                (this.ClientSize.Width - timeSize.Width) / 2, y + size.Height + 10);

            // Draw instructions
            var smallFont = new Font("Arial", 14);
            string instructionText;
            if (gameState == GameController.GameState.Won)
            {
                instructionText = "(R) Restart  (H) High Scores  (N) Save Score";
            }
            else
            {
                instructionText = "(R) Restart  (H) High Scores";
            }
            var instrSize = g.MeasureString(instructionText, smallFont);
            g.DrawString(instructionText, smallFont, Brushes.White, 
                (this.ClientSize.Width - instrSize.Width) / 2, y + size.Height + 50);
        }

        private void ResetGame()
        {
            gameController.InitializeGame();
            this.Invalidate();
        }

        private void ShowHighScores()
        {
            MessageBox.Show(gameController.GetHighScoresString(), "?? Top 10 High Scores", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowAllScores()
        {
            MessageBox.Show(gameController.GetAllScoresString(), "?? Complete Game History", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowStatistics()
        {
            var (totalGames, avgTime, bestTime, worstTime) = gameController.GetGameStatistics();
            
            string stats = $"Game Statistics\n" +
                          $"???????????????????????\n" +
                          $"Total Games: {totalGames}\n" +
                          $"Average Time: {avgTime:F2}s\n" +
                          $"Best Time: {bestTime:F2}s\n" +
                          $"Worst Time: {worstTime:F2}s\n";
            
            MessageBox.Show(stats, "?? Game Statistics", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SaveGameScore()
        {
            if (gameController.GetGameState() != GameController.GameState.Won)
            {
                MessageBox.Show("You can only save scores for wins!", "Cannot Save Score", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Prompt for player name
            string playerName = PromptForPlayerName();
            if (string.IsNullOrWhiteSpace(playerName))
            {
                MessageBox.Show("Score not saved (no name entered).", "Score Save Cancelled", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Save the score
            gameController.AddHighScore(playerName);
            
            // Show confirmation
            double elapsedTime = gameController.GetElapsedTime();
            MessageBox.Show($"? Score saved!\n\nPlayer: {playerName}\nTime: {elapsedTime:F2}s", 
                "Score Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string PromptForPlayerName()
        {
            Form prompt = new Form()
            {
                Text = "Save Your Score",
                Width = 300,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label label = new Label() { Left = 20, Top = 20, Text = "Enter your name:", Width = 250 };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 250 };
            Button okButton = new Button() 
            { 
                Text = "OK", Left = 120, Width = 80, Top = 80, 
                DialogResult = DialogResult.OK 
            };
            Button cancelButton = new Button() 
            { 
                Text = "Cancel", Left = 200, Width = 70, Top = 80, 
                DialogResult = DialogResult.Cancel 
            };

            prompt.Controls.Add(label);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(okButton);
            prompt.Controls.Add(cancelButton);
            prompt.AcceptButton = okButton;
            prompt.CancelButton = cancelButton;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : null;
        }
    }
}
