using System;
using System.Diagnostics;
using System.Threading;

namespace Snake
{
    class Game
    {
        private Sounds sounds;
        private int Score;
        private Snake snake;
        private FoodCreator foodCreator, foodCreator2, foodCreator3, foodCreator4, foodCreator5;
        private Point food, food2, food3, food4, food5;
        private Stopwatch stopwatch;

        public void Run()
        {
            int Speed = 140;

            stopwatch = new Stopwatch();
            stopwatch.Start();

            Console.SetWindowSize(80, 25);

            sounds = new Sounds(".");
            sounds.PlayBack();

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
                    sounds.PlayEat();
                    Thread.Sleep(200);
                    sounds.PlayBack();
                }
                else if (Count == 2)
                {
                    food2.Clear();
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
                    sounds.PlayEat();
                    Thread.Sleep(200);
                    sounds.PlayBack();
                }
                else if (Count2 == 4)
                {
                    food3.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    food3 = foodCreator3.CreateFood();
                    food3.Draw();
                    Count2 = 0;
                }

                if (snake.Eat(food3))
                {
                    Speed -= 20;
                    Count2 = 0;
                    Count3 += 1;
                    Score += 2;
                    sounds.PlayEat();
                    Thread.Sleep(200);
                    sounds.PlayBack();
                }

                if (Count3 >= 5)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    Random random = new Random();
                    int foodType = random.Next(2);
                    Count3 = 0;

                    if (foodType == 0)
                    {
                        food5.Clear();
                        food4.Clear();
                        food4 = foodCreator4.CreateFood();
                        food4.Draw();
                    }
                    else
                    {
                        food4.Clear();
                        food5.Clear();
                        food5 = foodCreator5.CreateFood();
                        food5.Draw();
                    }
                }
                if (snake.Eat(food4))
                {
                    Score -= 3;
                    sounds.PlayEat();
                    Thread.Sleep(200);
                    sounds.PlayBack();
                }

                if (snake.Eat(food5))
                {
                    Score += 1;
                    Speed += 10;
                    sounds.PlayEat();
                    Thread.Sleep(200);
                    sounds.PlayBack();
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

            GameOver gameOver = new GameOver(Score, stopwatch.Elapsed);
            gameOver.WriteGameOver();

            Console.ReadLine();
        }
    }
}
