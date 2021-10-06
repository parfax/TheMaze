using System;

namespace ConsoleApp6
{
    public class World
    {
        private int height, width;
        private char[,] Grid;
        
        public World(int y, int x)
        {
            height = y;
            width = x;
            Grid = new char[height, width];
        }

        public void GenerateNew()
        {
            var rand = new Random();

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var symbol = rand.Next(1, 100) < 28 ? '#' : ' ';

                    Grid[i, j] = symbol;
                }
            }

            int finishX = rand.Next(0, width - 1);
            int finishY = rand.Next(0, height - 1);
            Grid[finishY, finishX] = 'x';
        }

        public char GetElementAt(int y, int x)
        {
            return Grid[y, x];
        }
    }
}