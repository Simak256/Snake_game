namespace Snake_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // SNAKE GAME
            int[,] map =
            {
                { 0,0,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1,1 },
                { 1,1,1,1,1,1,1,1,1,1 },
            };

            List<(int x, int y)> snake = new List<(int x, int y)>
            {
                (1, 0),
                (0, 0)
            };
            int x = 1;
            int y = 0;
            var direction = ConsoleKey.RightArrow;
            bool paused = false;

            while (true)
            {
                RenderMap(map);
                if (Console.KeyAvailable)
                {
                    var keyPressed = Console.ReadKey(true).Key;
                    switch (keyPressed)
                    {
                        case ConsoleKey.UpArrow:
                        case ConsoleKey.DownArrow:
                        case ConsoleKey.LeftArrow:
                        case ConsoleKey.RightArrow:
                            direction = keyPressed;
                            break;
                        case ConsoleKey.Spacebar:
                            paused = !paused;
                            Console.WriteLine(paused ? "Paused" : "Unpaused");
                            break;
                        case ConsoleKey.Escape:
                            RenderMap(map);
                            Console.WriteLine("Game Over!");
                            Environment.Exit(0);
                            break;
                    }
                }

                switch (direction)
                {
                    case ConsoleKey.UpArrow:
                        y--;
                        break;
                    case ConsoleKey.DownArrow:
                        y++;
                        break;
                    case ConsoleKey.LeftArrow:
                        x--;
                        break;
                    case ConsoleKey.RightArrow:
                        x++;
                        break;
                }

                if (paused)
                {
                    Thread.Sleep(100);
                    continue;
                }

                if (x < 0 || x >= map.GetLength(1) || y < 0 || y >= map.GetLength(0))
                {
                    Console.WriteLine("Game Over!");
                    Environment.Exit(0);
                }

                ChangeMap(map, x, y, snake);
            }
        }
        // SNAKE UPDATE
        public static void ChangeMap(int[,] map, int x, int y, List<(int x, int y)> snake)
        {
            Console.SetCursorPosition(0, 0);

            var tail = snake[snake.Count - 1];
            map[tail.y, tail.x] = 1;
            snake.Insert(0, (x, y));
            snake.RemoveAt(snake.Count - 1);

            map[y, x] = 0;
        }
        // MAP RENDERING
        public static void RenderMap(int[,] map)
        {
            Thread.Sleep(500);
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
