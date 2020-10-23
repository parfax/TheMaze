using System;
namespace ConsoleApp6
{
    class Program2 {
        // [* CONFIGURATION *]
        static int width = 10, height = 12, 
            // Координаты игрока
            pX = 0, pY = 0,

            // Input
            playerrX = 0, playerrY = 0,

            // Финиш
            finishX = 0, finishY = 0;
        static char[,]field = new char[height, width];
        static char symbol,player = '0';

        static bool is_game_end, is_wallkable=true;

        // MENU
        public static void Main(string[] args) {
            Console.Title = "GAME MENU";
            Console.Write("Руководство по игре:\n  # это стена\n  0 это игрок\n  x это финиш.\n");
            Console.WriteLine("===============MENU===============\n" +
                " 1 Играть\n" +
                " 2 Выйти из игры\n==================================\nВаш выбор:");
            int caseSwitch = int.Parse(Console.ReadLine());

            switch (caseSwitch)
            {
                case 1:
                    GO();
                    break;
                case 2:
                    Console.WriteLine("Выходим из игры...");
                    break;
            }
        }
        static void GO()
        {
            Console.Title = "MAZE GAME";
            generate_maze();
            player_place();
            draw();
            while (!is_game_end)
            {
                get_input();
                logic();
                draw();
            }
            Console.WriteLine("УРА, ТЫ ВЫИГРАЛ!!!");
        }
        static void generate_maze()
        {
            Random rand = new Random();
            
            for (int i = 0; i < height; i++)
            {            
                for (int j = 0; j < width; j++)
                {
                    if (rand.Next(1, 100) < 28)
                        symbol ='#';
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
            Console.Clear();
            if (!is_wallkable)
            {
                Console.WriteLine("Кхм... Я не привидение, чтобы  проходить сквозь стены.");
                is_wallkable = true;
            }
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == pY && j == pX)
                        symbol = player;
                    else
                        symbol = field[i,j];
                    Console.Write(symbol);
                }
                Console.WriteLine();
            }
        }
        static void player_place()
        {
            Random rand = new Random();
            pX = rand.Next(0, width - 1);
            pY = rand.Next(0, height - 1);
        }
        static void logic()
        {
            try_go_to(pX + playerrX, pY + playerrY);
            check_finish();
        }
        static void get_input()
        {
            playerrX = 0; playerrY = 0;
            string inp = Console.ReadLine();
            if (inp.Length == 0)
                return;
            string first_symbol = inp.Split()[0];
            if (first_symbol == "W" || first_symbol == "w")
                playerrY = -1;
            if (first_symbol == "A" || first_symbol == "a")
                playerrX = -1;
            if (first_symbol == "S" || first_symbol == "s")
                playerrY = 1;
            if (first_symbol == "D" || first_symbol == "d")
                playerrX = 1;
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
            pX = newX; pY = newY;
        }
        static void try_go_to(int newX,int newY)
        {
            if (can_go_to(newX, newY))
                go_to(newX, newY);
        }
        static void check_finish()
        {
            if (pX == finishX && pY == finishY)
                is_game_end = true;
        }
    }
}
