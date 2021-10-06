using System;
using static System.Console;

namespace ConsoleApp6
{
    public class Game
    {
        #region Configuration

        // Map size
        public static int width = 35;
        public static int height = 23;

        // Cell array
        static World world = new World(height, width);

        static Player player = new Player();


        // Input axies
        static int horizontal, vertical;

        // Symbols that fill the cells
        static char symbol;

        // Phrases
        static string[] phrases =
            {"Кхм... Я не привидение чтобы  проходить сквозь стены.", "Если бы всё было так просто..."};


        // Checks if
        private static bool is_wallkable = true;

        static int radiusOfView = 3;

        // Stat
        private static int stepCount;

        #endregion

        // Game cycle
        public static void Play()
        {
            CursorVisible = false;
            Title = "The Maze by Parfax";

            world.GenerateNew();
            player.Spawn();
            Draw();
            while (!is_game_end())
            {
                GetInput();
                logic();
                Draw();
            }
            
            Clear();
            WriteLine($@"Ура, вы прошли лабиринт!
Всего сделано {stepCount} шагов.
");

            BackgroundColor = ConsoleColor.Gray;
            ForegroundColor = ConsoleColor.Black;
            Write("> Вернутся в главное меню ");
            ConsoleKeyInfo input;
            do
                input = ReadKey(true);
            while (input.Key != ConsoleKey.Enter);

            // Resetting everything on Enter
            if (input.Key == ConsoleKey.Enter)
            {
                ResetColor();
                Clear();
                stepCount = 0;
                Menu.ReturnToTheMenu();
            }
        }

        private static void Draw()
        {
            // The wall in front of your nose!
            if (!is_wallkable)
            {
                SetCursorPosition(0, height + 1);
                Write($"Мысли: {phrases[0]}");
                is_wallkable = true;
            }
            else
            {
                for (int i = 0; i < 7 + phrases[0].Length; i++)
                {
                    SetCursorPosition(i, height + 1);
                    Write(' ');
                }
            }

            SetCursorPosition(0, 0);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    // Placing player to the next cell
                    if (i == player.Y && j == player.X)
                    {
                        symbol = '0';
                        ForegroundColor = ConsoleColor.Blue;
                    }
                    // Fog
                    else if ((i >= player.Y && i <= player.Y + radiusOfView) &&
                             (j >= player.X && j <= player.X + radiusOfView))
                        symbol = world.GetElementAt(i, j);

                    else if ((i <= player.Y && i >= player.Y - radiusOfView) &&
                             (j <= player.X && j >= player.X - 3))
                        symbol = world.GetElementAt(i, j);

                    else if ((i <= player.Y && i >= player.Y - radiusOfView) &&
                             (j >= player.X && j <= player.X + 3))
                        symbol = world.GetElementAt(i, j);

                    else if ((i >= player.Y && i <= player.Y + radiusOfView) &&
                             (j <= player.X && j >= player.X - 3))
                        symbol = world.GetElementAt(i, j);

                    else
                        symbol = ' ';

                    Write(symbol);
                    ResetColor();
                }

                WriteLine();
            }
        }

        #region Inspections

        static void GetInput()
        {
            horizontal = 0;
            vertical = 0;
            var input = ReadKey(true);
            switch (input.Key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    vertical--;
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    vertical++;
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    horizontal--;
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    horizontal++;
                    break;
                case ConsoleKey.Escape:
                    for (int i = 0; i < 7 + phrases[0].Length; i++)
                    {
                        SetCursorPosition(i, height + 1);
                        Write(' ');
                    }

                    SetCursorPosition(0, height + 1);
                    Write($"Мысли: {phrases[1]}");
                    GetInput();
                    return;
                default:
                    return;
            }

            stepCount++;
        }

        static void logic()
        {
            try_go_to(player.X + horizontal, player.Y + vertical);
            is_game_end();
        }


        static bool is_walkable(int X, int Y)
        {
            if (world.GetElementAt(Y, X) == '#')
            {
                is_wallkable = false;
                return false;
            }

            return true;
        }

        static void try_go_to(int newX, int newY)
        {
            if (can_go_to(newX, newY))
                go_to(newX, newY);
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
            player.X = newX;
            player.Y = newY;
        }

        static bool is_game_end()
        {
            if (world.GetElementAt(player.Y, player.X) == 'x')
                return true;
            return false;
        }

        #endregion
    }
}