using System;

namespace MazeGame_by_Parfax
{
    public class Player
    {
        // Coordinates
        public int Y { get; set; }
        public int X { get; set; }
        public void Spawn()
        {
            var rand = new Random();
            X = rand.Next(0, Game.width - 1);
            Y = rand.Next(0, Game.height - 1);
        }
    }
}