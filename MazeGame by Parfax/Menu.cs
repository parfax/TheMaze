using System;

namespace ConsoleApp6
{
    class Menu
    {

        // MENU
        public static void Main(string[] args)
        {
            Console.Title = "GAME MENU";
            Console.Write("Руководство по игре:\n  # это стена\n  0 это игрок\n  x это финиш.\n");
            Console.WriteLine("===============MENU===============\n" +
                              " 1 Играть\n" +
                              " 2 Выйти из игры\n==================================\nВаш выбор:");
            int caseSwitch = int.Parse(Console.ReadLine());

            switch (caseSwitch)
            {
                case 1:
                    Console.Clear();
                    Game.Play();
                    break;
                case 2:
                    Console.WriteLine("Выходим из игры...");
                    break;
            }
        }
    }
}