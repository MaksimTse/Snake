using System;
using System.Diagnostics;
using System.IO;

namespace Snake
{
    class GameOver
    {
        private int Score;
        private TimeSpan elapsedTime;
        private Stopwatch stopwatch;

        public GameOver(int score, TimeSpan elapsedTime)
        {
            Score = score;
            this.elapsedTime = elapsedTime;
            stopwatch = new Stopwatch();
        }

        public void WriteGameOver()
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.Clear();
            stopwatch.Stop();
            string time = elapsedTime.ToString(@"mm\:ss");
            Sounds sounds = new Sounds(".");
            sounds.PlayGameOver();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            WriteText("G A M E   O V E R", xOffset + 5, yOffset++);
            WriteText($"You got {Score} Points!", xOffset + 5, yOffset++);
            WriteText($"Time spent: {time}", xOffset + 5, yOffset++);
            yOffset++;
            WriteText("Autor: Someone", xOffset + 6, yOffset++);
            WriteText("Special for TTHK", xOffset + 5, yOffset++);
            WriteText("============================", xOffset, yOffset++);

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(xOffset - 8, yOffset + 3);
            Console.Write("Enter your name to add you into the leaderboard! : ");
            string playerName = Console.ReadLine();

            string leaderboardFile = "LeaderBoard.txt";

            string points = $"{playerName}: {Score} Points, {time}";
            try
            {
                using (StreamWriter writer = new StreamWriter(leaderboardFile, true))
                {
                    writer.WriteLine(points);
                }
                Console.WriteLine("Name written to the leaderboard");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error writing to LeaderBoard.txt: " + ex.Message);
            }
        }

        private void WriteText(string text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
    }
}
