using System;
using System.Diagnostics;
using ConsoleApp6;
using static System.Console;

namespace MazeGame_by_Parfax
{
    public class Game
    {
        #region Configuration

        // Map size
        public const int width = 35;
        public const int height = 23;

        // Cell array
        private static World world = new World(height, width);

        private static Player player = new Player();


        // Input axies
        private static int horizontal, vertical;

        // Symbols that fill the cells
        private static char symbol;

        // Phrases
        private static string[] phrases =
            {"Кхм... Я не привидение чтобы  проходить сквозь стены.", "Если бы всё было так просто..."};


        // Checks if
        private static bool is_walkable = true;

        // Stat
        private const int RadiusOfView = 3;
        private static int stepCount;
        private static Stopwatch timer = new Stopwatch();

        #endregion

        // Game cycle
        public static void Play()
        {
            CursorVisible = false;
            Title = "The Maze by Parfax";

            world.GenerateNew();
            player.Spawn();
            Draw();
            timer.Start();
            while (!is_game_end())
            {
                GetInput();
                Logic();
                Draw();
            }

            Clear();
            timer.Stop();
            
            ForegroundColor = ConsoleColor.DarkYellow;
            WriteLine("\n  Ура, вы прошли лабиринт!");
            ResetColor();
            WriteLine($"  Время прохождения лабиринта: {Math.Round(timer.ElapsedMilliseconds / 1000f, 2)} сек.");
            WriteLine($"  Всего сделано {stepCount} шагов.\n");
            
            timer.Reset();
            stepCount = 0;
            SetCursorPosition(2, CursorTop);
            Menu.BackButton();
        }

        private static void Draw()
        {
            // The wall in front of your nose!
            if (!is_walkable)
            {
                SetCursorPosition(0, height + 1);
                Write($"Мысли: {phrases[0]}");
                is_walkable = true;
            }
            else
            {
                for (var i = 0; i < 7 + phrases[0].Length; i++)
                {
                    SetCursorPosition(i, height + 1);
                    Write(' ');
                }
            }

            SetCursorPosition(0, 0);
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    // Placing player to the next cell
                    if (i == player.Y && j == player.X)
                    {
                        symbol = '0';
                        ForegroundColor = ConsoleColor.Blue;
                    }
                    // Fog
                    else if ((i >= player.Y - RadiusOfView && i <= player.Y + RadiusOfView) &&
                             (j >= player.X - RadiusOfView && j <= player.X + RadiusOfView))
                    {
                        symbol = world.GetElementAt(i, j);
                    }

                    else symbol = ' ';

                    Write(symbol);
                    ResetColor();
                }

                WriteLine();
            }
        }

        #region Inspections

        private static void GetInput()
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
                    for (var i = 0; i < 7 + phrases[0].Length; i++)
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

        private static void Logic()
        {
            try_go_to(player.X + horizontal, player.Y + vertical);
            is_game_end();
        }


        private static bool IsWalkable(int X, int Y)
        {
            if (world.GetElementAt(Y, X) != '#') return true;
            is_walkable = false;
            return false;
        }

        private static void try_go_to(int newX, int newY)
        {
            if (can_go_to(newX, newY))
                go_to(newX, newY);
        }

        private static bool can_go_to(int newX, int newY)
        {
            if (newX < 0 || newY < 0 || newX >= width || newY >= height)
                return false;
            return IsWalkable(newX, newY);
        }

        private static void go_to(int newX, int newY)
        {
            player.X = newX;
            player.Y = newY;
        }

        private static bool is_game_end()
        {
            return world.GetElementAt(player.Y, player.X) == 'x';
        }

        #endregion
        
        private static void CenterLine(ConsoleColor color = ConsoleColor.White,params string[] lines)
        {
            var verticalStart = (WindowHeight - lines.Length) / 2;
            var verticalPosition = verticalStart;
            foreach (var line in lines)
            {
                var horizontalStart = (WindowWidth - line.Length) / 2;
                SetCursorPosition(horizontalStart, verticalPosition);
                ForegroundColor = color;
                Write(line);
                ++verticalPosition;
            }
        }
    }
}