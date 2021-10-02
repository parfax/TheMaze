using System.Collections.Generic;
using System;

namespace ConsoleApp6
{
    class Menu
    {

        public static List<Option> options;
        static int index;
        static void Main(string[] args)
        {
            Console.Title = "The Maze | Main Menu";
            Console.CursorVisible = false;
            
            
            options = new List<Option>
            {
                new Option("Играть", () =>
                    {
                        Console.Clear();
                        Game.Play();
                    }),
                //new Option("А как играть?", () => ,
                new Option("Выйти", () => Environment.Exit(0))
            };
            
            DrawMenu(options, options[index]);

            // Store key info in here
            ConsoleKeyInfo keyinfo;
            do
            {
                keyinfo = Console.ReadKey(true);
                if (keyinfo.Key == ConsoleKey.DownArrow || keyinfo.Key == ConsoleKey.S)
                {
                    if (index + 1 < options.Count)
                    {
                        index++;
                        DrawMenu(options, options[index]);
                    }
                }
                if (keyinfo.Key == ConsoleKey.UpArrow || keyinfo.Key == ConsoleKey.W)
                {
                    if (index - 1 >= 0)
                    {
                        index--;
                        DrawMenu(options, options[index]);
                    }
                }
                
                if (keyinfo.Key == ConsoleKey.Enter)
                {
                    options[index].Selected.Invoke();
                    break;
                }
            }
            while (keyinfo.Key != ConsoleKey.Escape);
        }
        static void DrawMenu(List<Option> options, Option selectedOption)
        {
            Console.SetCursorPosition(0,0);

            foreach (Option option in options)
            {
                if (option == selectedOption)
                    Console.Write("> ");
                else
                    Console.Write(' ');

                Console.WriteLine(option.Name + ' ');
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