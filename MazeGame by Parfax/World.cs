using System;

namespace MazeGame_by_Parfax
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

            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    var symbol = rand.Next(1, 100) < 28 ? '#' : ' ';

                    Grid[i, j] = symbol;
                }
            }

            var finishX = rand.Next(0, width - 1);
            var finishY = rand.Next(0, height - 1);
            Grid[finishY, finishX] = 'x';
        }

        public char GetElementAt(int y, int x)
        {
            return Grid[y, x];
        }
    }
}