using System;

namespace ConsoleApp6
{
    public class Game
    {
        
        #region Configuration
        
        // Map size
        static int width = 35, height = 23;
        
        // Player coordinates
        static int position_X, position_Y;
        
        // Input axies
        static int horizontal, vertical;
        
        // The finish coordinates
        static int finishX, finishY;
        
        // Cell array
        static char[,] field = new char[height, width];
        
        // Symbols that fill the cells
        static char symbol, player = '0';
        
        // Phrases
        static string phrase = "Мысли: Кхм... Я не привидение чтобы  проходить сквозь стены.";
        
        // Checks if
        static bool is_game_end, is_wallkable = true;

        #endregion
        
        public static void Play()
        {
            Console.CursorVisible = false;
            Console.Title = "MazeGame by Parfax";
            GenerateMap();
            player_place();
            draw();
            while (!is_game_end)
            {
                GetInput();
                logic();
                draw();
            }

            Console.WriteLine("Wow, you won!");
        }

        static void GenerateMap()
        {
            Random rand = new Random();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (rand.Next(1, 100) < 28)
                        symbol = '#';
                    else
                        symbol = ' ';

                    field[i, j] = symbol;
                }
            }

            finishX = rand.Next(0, width - 1);
            finishY = rand.Next(0, height - 1);
            field[finishY, finishX] = 'x';
        }

        static void draw()
        {
            int indent = width + 5;
            if (!is_wallkable)
            {
                Console.SetCursorPosition(indent, height - 13);
                Console.Write(phrase);
                is_wallkable = true;
            }
            else
            {
                for (int i = indent; i < indent+phrase.Length; i++)
                {
                    Console.SetCursorPosition(i, height - 13);
                    Console.Write(" ");
                }
            }

            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == position_Y && j == position_X)
                    {
                        symbol = player;
                        Console.ForegroundColor = ConsoleColor.Blue;
                    }
                    else
                        symbol = field[i, j];

                    Console.Write(symbol);
                    Console.ResetColor();
                }

                Console.WriteLine();
            }
        }

        static void player_place()
        {
            Random rand = new Random();
            position_X = rand.Next(0, width - 1);
            position_Y = rand.Next(0, height - 1);
        }

        static void logic()
        {
            try_go_to(position_X + horizontal, position_Y + vertical);
            check_finish();
        }

        static void GetInput()
        {
            horizontal = 0;
            vertical = 0;
            var input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.W:
                    vertical--;
                    break;
                case ConsoleKey.S:
                    vertical++;
                    break;
                case ConsoleKey.A:
                    horizontal--;
                    break;
                case ConsoleKey.D:
                    horizontal++;
                    break;
            }
        }

        static bool is_walkable(int X, int Y)
        {
            if (field[Y, X] == '#')
            {
                is_wallkable = false;
                return false;
            }

            return true;
        }

        static bool can_go_to(int newX, int newY)
        {
            if (newX < 0 || newY < 0 || newX >= width || newY >= height)
                return false;
            if (!is_walkable(newX, newY))
                return false;
            return true;
        }

        static void go_to(int newX, int newY)
        {
            position_X = newX;
            position_Y = newY;
        }

        static void try_go_to(int newX, int newY)
        {
            if (can_go_to(newX, newY))
                go_to(newX, newY);
        }

        static void check_finish()
        {
            if (position_X == finishX && position_Y == finishY)
                is_game_end = true;
        }
    }
}