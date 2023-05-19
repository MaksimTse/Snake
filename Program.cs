using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Snake
{
    class Program
    {
        private int Score;
        static void Main(string[] args)
        {

            int Speed = 140;

            Console.SetWindowSize(80, 25);

            Music game = new Music();
            ConsoleKeyInfo nupp = new ConsoleKeyInfo();
            _ = game.Background_music("../../../Background_Music.mp3");

            Walls walls = new Walls(80, 25);
            walls.Draw();

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.ForegroundColor = ConsoleColor.Green;
            Point p = new Point(4, 5, '●');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Draw();

            Console.ForegroundColor = ConsoleColor.Yellow;
            FoodCreator foodCreator = new FoodCreator(80, 25, '¤');
            Point food = foodCreator.CreateFood();
            food.Draw();

            FoodCreator foodCreator2 = new FoodCreator(80, 25, '$');
            Point food2 = foodCreator2.CreateFood();
            food2.Draw();

            FoodCreator foodCreator3 = new FoodCreator(80, 25, '%');
            Point food3 = foodCreator3.CreateFood();
            food.Draw();

            int Count = 0;
            int Count2 = 0;
        
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
                    Speed -= 10;
                    Count2 = 0;
                    Score += 1;
                }

                if (snake.Eat3(food3))
                {
                    Speed -= 10;
                    Count2 = 0;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    snake.Move();
                }

                Thread.Sleep(Speed);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
            }
            WriteGameOver();
            Console.ReadLine();
            static void WriteGameOver()
            {
                int xOffset = 25;
                int yOffset = 8;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(xOffset, yOffset++);
                WriteText("============================", xOffset, yOffset++);
                WriteText("G A M E   O V E R?", xOffset + 5, yOffset++);
                yOffset++;
                WriteText("Autor: Someone", xOffset + 6, yOffset++);
                WriteText("Special for TTHK", xOffset + 5, yOffset++);
                WriteText("============================", xOffset, yOffset++);

                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(xOffset + 2, yOffset + 3);
                Console.Write("Enter your name: ");
                string playerName = Console.ReadLine();

                string leaderboardFile = "LeaderBoard.txt";

                string points = $"{playerName}: {Score}";
                try
                {
                    using (StreamWriter writer = new StreamWriter(leaderboardFile, true))
                    {
                        writer.WriteLine(playerName);
                    }
                    Console.WriteLine("Name written to the leaderboard");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error writing to LeaderBoard.txt: " + ex.Message);
                }
            }


            static void WriteText(String text, int xOffset, int yOffset)
            {
                Console.SetCursorPosition(xOffset, yOffset);
                Console.WriteLine(text);
            }

        }
    }
}