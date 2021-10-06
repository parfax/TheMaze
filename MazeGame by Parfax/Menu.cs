using System.Collections.Generic;
using System;
using static System.Console;

namespace ConsoleApp6
{
    class Menu
    {

        public static List<Option> options;
        static int index;
        public static void ReturnToTheMenu()
        {
            Title = "The Maze | Main Menu";
            CursorVisible = false;
            
            options = new List<Option>
            {
                new Option("Играть", () =>
                    {
                        Clear();
                        Game.Play();
                    }),
                new Option("Загрузить лабиринт", () => HowToPlay()),
                new Option("Мои достияжения", () => ShowAchievements()),
                new Option("Как играть", () => HowToPlay()),
                new Option("Выйти", () => Environment.Exit(0))
            };
            
            DrawMenu(options, options[index]);
            HandleOptions();
        }
        static void HowToPlay()
                {
                    Clear();
                    
                    WriteLine("Вам предстоит бродить по лабиринту в поиске выхода,");
                    WriteLine("но найти его будет не просто из-за маленького поля зрения.");
                    WriteLine("Лабиринт генерируется случайным образом\n");
                    ForegroundColor = ConsoleColor.Blue;
                    Write("   0");
                    ResetColor();
                    Write(" — главный герой\n");
                    WriteLine("   # — это стена, вы не можете проходить сквозь неё");
                    WriteLine("   x — это финиш, ваша цель\n");
                    WriteLine("Управление клавишами W A S D или ↑ ← ↓ →\n");

                    BackButton();
                }

        static void ShowAchievements()
        {
            Clear();
            
            WriteLine($"Мои достижения ( из 7):");
            
            BackButton();
        }
        
        static void HandleOptions()
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

        static void BackButton()
        {
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
                ReturnToTheMenu();
            }
        }
        static void DrawMenu(List<Option> options, Option selectedOption)
        {
            SetCursorPosition(0,0);

            foreach (Option option in options)
            {
                if (option == selectedOption)
                    Write("> ");
                else
                    Write(' ');

                WriteLine(option.Name + ' ');
            }
        }
        
        public class Option
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