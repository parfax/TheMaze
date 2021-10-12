using System;
using System.Collections.Generic;
using static System.Console;

namespace MazeGame_by_Parfax
{
    class Menu
    {
        private static List<Option> options;
        private static int index;

        public static void ReturnToTheMenu()
        {
            Title = "The Maze | Main Menu";
            CursorVisible = false;
            
            Write(@"  
  _____ _          __  __   by @Parfax        
 |_   _| |_  ___  |  \/  |__ _ ______    
   | | | ' \/ -_) | |\/| / _` |_ / -_)
   |_| |_||_\___| |_|  |_\__,_/__\___|
                                      
");

            options = new List<Option>
            {
                new Option("Играть", () =>
                {
                    Clear();
                    Game.Play();
                }),
                new Option("Как играть", () => HowToPlay()),
                new Option("Выйти", () => Environment.Exit(0))
            };

            DrawMenu(options, options[index]);
            HandleOptions();
        }

        private static void HowToPlay()
        {
            Clear();
            
            WriteLine();
            WriteLine("  Вам предстоит бродить по лабиринту в поиске выхода,");
            WriteLine("  но найти его будет не просто из-за маленького поля зрения.");
            WriteLine("  Лабиринт генерируется случайным образом\n");
            ForegroundColor = ConsoleColor.Blue;
            Write("    0");
            ResetColor();
            Write(" — главный герой\n");
            WriteLine("    # — это стена, вы не можете проходить сквозь неё");
            WriteLine("    x — это финиш, ваша цель\n");
            WriteLine("  Управление клавишами W A S D или ↑ ← ↓ →\n");
            
            SetCursorPosition(2, CursorTop);
            BackButton();
        }


        private static void HandleOptions()
        {
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = ReadKey(true);
                if (keyinfo.Key == ConsoleKey.DownArrow || keyinfo.Key == ConsoleKey.S)
                    if (index + 1 < options.Count)
                        index++;

                if (keyinfo.Key == ConsoleKey.UpArrow || keyinfo.Key == ConsoleKey.W)
                    if (index - 1 >= 0)
                        index--;

                DrawMenu(options, options[index]);

                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    options[index].Selected.Invoke();
                    break;
                }
            } while (keyinfo.Key != ConsoleKey.Escape);
        }

        public static void BackButton()
        {
            BackgroundColor = ConsoleColor.Gray;
            ForegroundColor = ConsoleColor.Black;
            Write("> Вернутся в главное меню ");
            ConsoleKeyInfo input;
            do
                input = ReadKey(true);
            while (input.Key != ConsoleKey.Enter);

            ResetColor();
            Clear();
            ReturnToTheMenu();
        }

        private static void DrawMenu(List<Option> options, Option selectedOption)
        {
            SetCursorPosition(0, 6);

            foreach (Option option in options)
            {
                if (option == selectedOption)
                    Write("  > ");
                else
                    Write("  ");

                WriteLine(option.Name + "  ");
            }
        }

        private class Option
        {
            public string Name { get; }
            public Action Selected { get; }

            public Option(string name, Action selected)
            {
                Name = name;
                Selected = selected;
            }
        }
    }
}