using System;

namespace ConsoleApp6
{
    public class Player
    {
        // Coordinates
        public int Y { get; set; }
        public int X { get; set; }
        public void Spawn()
        {
            Random rand = new Random();
            X = rand.Next(0, Game.width - 1);
            Y = rand.Next(0, Game.height - 1);
        }
    }
}