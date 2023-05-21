using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Threading;

namespace Snake
{
    class Program
    {
        private static int Score;
        private static Snake snake;
        private static FoodCreator foodCreator;
        private static FoodCreator foodCreator2;
        private static FoodCreator foodCreator3;
        private static FoodCreator foodCreator4;
        private static FoodCreator foodCreator5;
        private static Point food;
        private static Point food2;
        private static Point food3;
        private static Point food4;
        private static Point food5;

        static void Main(string[] args)
        {
            int Speed = 140;

            Console.SetWindowSize(80, 25);

            Music game = new Music();
            _ = game.Background_music("../../../Background_Music.mp3");

            Walls walls = new Walls(80, 25);
            walls.Draw();

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Green;
            Point p = new Point(4, 5, '●');
            snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();

            Console.ForegroundColor = ConsoleColor.Yellow;
            foodCreator = new FoodCreator(80, 25, '¤');
            food = foodCreator.CreateFood();
            food.Draw();

            foodCreator2 = new FoodCreator(80, 25, '$');
            food2 = foodCreator2.CreateFood();
            food2.Draw();

            foodCreator3 = new FoodCreator(80, 25, '%');
            food3 = foodCreator3.CreateFood();
            food3.Draw();

            foodCreator4 = new FoodCreator(80, 25, '-');
            food4 = foodCreator4.CreateFood();

            foodCreator5 = new FoodCreator(80, 25, '#');
            food5 = foodCreator5.CreateFood();

            int Count = 0;
            int Count2 = 0;
            int Count3 = 0;

            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }

                if (snake.Eat(food))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Count += 1;
                    Count2 += 1;
                    Count3 += 1;
                    Score += 1;
                    food = foodCreator.CreateFood();
                    food.Draw();
                }
                else if (Count == 2)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    food2 = foodCreator2.CreateFood();
                    food2.Draw();
                    Count = 0;
                }

                if (snake.Eat2(food2))
                {
                    Count = 0;
                    Count3 += 1;
                    Score += 2;
                }
                else if (Count2 == 4)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    food3 = foodCreator3.CreateFood();
                    food3.Draw();
                    Count2 = 0;
                }

                if (snake.Eat3(food3))
                {
                    Speed -= 20;
                    Count2 = 0;
                    Count3 += 1;
                    Score += 2;
                }

                if (Count3 >= 5)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Random random = new Random();
                    int foodType = random.Next(2);
                    Count3 = 0;

                    if (foodType == 0)
                    {
                        food4 = foodCreator4.CreateFood();
                        food4.Draw();
                    }
                    else
                    {
                        food5 = foodCreator5.CreateFood();
                        food5.Draw();
                    }
                }
                if (snake.Eat4(food4))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Score -= 3;
                    food4 = foodCreator4.CreateFood();
                    food4.Draw();
                }

                if (snake.Eat5(food5))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Score += 1;
                    Speed += 10;
                    food5 = foodCreator5.CreateFood();
                    food5.Draw();
                }

                Console.ForegroundColor = ConsoleColor.Green;
                snake.Move();

                Thread.Sleep(Speed);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }

            WriteGameOver();
            Console.ReadLine();
        }

        static void WriteGameOver()
            {
            int xOffset = 25;
            int yOffset = 8;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            WriteText("G A M E   O V E R", xOffset + 5, yOffset++);
            WriteText($"You got {Score} Points!", xOffset + 5, yOffset++);
            yOffset++;
            WriteText("Autor: Someone", xOffset + 6, yOffset++);
            WriteText("Special for TTHK", xOffset + 5, yOffset++);
            WriteText("============================", xOffset, yOffset++);

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(xOffset - 8, yOffset + 3);
            Console.Write("Enter your name to add you into the leaderboard! : ");
            string playerName = Console.ReadLine();

            string leaderboardFile = "LeaderBoard.txt";

            string points = $"{playerName}: {Score} Points";
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

        static void WriteText(string text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
    }
}
