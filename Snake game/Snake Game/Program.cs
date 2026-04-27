namespace Snake_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // SNAKE GAME
            char BG = '1';
            int height = 5;
            int width = 15;
            var map = MakeMap(height, width, BG);

            Random random = new Random();

            List<(int x, int y)> snake = new List<(int x, int y)>
            {
                (0, 0)
            };
            int x = 0;
            int y = 0;
            var direction = ConsoleKey.RightArrow;
            bool paused = false;

            int points = 0;
            int apples;
            int xApple = random.Next(0, map.GetLength(1));
            int yApple = random.Next(0, map.GetLength(0));

            while (true)
            {
                RenderMap(map, xApple, yApple);
                Console.WriteLine("Points: " + points);

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
                            Console.WriteLine(paused ? "Paused  " : "Unpaused");
                            break;
                        case ConsoleKey.Escape:
                            RenderMap(map, xApple, yApple);
                            Console.WriteLine("Game Over!");
                            Environment.Exit(0);
                            break;
                    }
                }

                if (paused)
                {
                    Thread.Sleep(100);
                    continue;
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



                if (x < 0 || x >= map.GetLength(1) || y < 0 || y >= map.GetLength(0) || map[y, x] == '0')
                {
                    Console.WriteLine("Game Over!");
                    Environment.Exit(0);
                }

                
                ChangeMap(map, x, y, snake, ref points, ref xApple, ref yApple, random, BG);
            }
        }
        // MAP MAKING
        public static char[,] MakeMap(int height, int width, char BG)
        {
            char[,] mapMake = new char[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    mapMake[i, j] = BG;
                }
            }
            return mapMake;
        }
        // SNAKE UPDATE
        public static void ChangeMap(char[,] map, int x, int y, List<(int x, int y)> snake, ref int points, ref int xApple, ref int yApple, Random random, char BG)
        {
            Console.SetCursorPosition(0, 0);

            var tail = snake[snake.Count - 1];
            map[tail.y, tail.x] = BG;
            snake.Insert(0, (x, y));
            snake.RemoveAt(snake.Count - 1);

            map[y, x] = '0';

            if (x == xApple && y == yApple)
            {
                if (x < map.GetLength(1) - 1 && y < map.GetLength(0) - 1) { snake.Add((x+1, y+1)); }
                else if (x < map.GetLength(1) - 1 && y !< map.GetLength(0) - 1) {  snake.Add((x+1,y)); }
                else if (x !< map.GetLength(1) - 1 && y < map.GetLength(0) - 1) { snake.Add((x, y + 1)); }
                points++;
                xApple = random.Next(0, map.GetLength(1));
                yApple = random.Next(0, map.GetLength(0));
            }
        }
        // MAP RENDERING
        public static void RenderMap(char[,] map, int xApple, int yApple)
        {
            Thread.Sleep(500);
            Console.SetCursorPosition(0, 0);


            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (i == yApple && j == xApple)
                    {
                        map[i, j] = '*';
                        Console.Write(map[i, j]);
                    }
                    else
                        Console.Write(map[i, j]);
                }
                Console.WriteLine();
            }

        }
    }
}
