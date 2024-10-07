using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelLabb2.GameObjects.Enemies
{
    public class Snake : Enemy
    {
        public Snake(int x, int y) : base("Snake", 25, 's', ConsoleColor.Green)
        {
            
            AttackDice = new Dice.Dice(2, 6, 2);
            DefenceDice = new Dice.Dice(2, 6, 1);
            X = x;
            Y = y;
            
        }

        public override void Update(Player.Player player, LevelData levelData)
        {
            int distance = (int)Math.Sqrt(Math.Pow(player.X - X, 2) + Math.Pow(player.Y - Y, 2));

            // Om spelaren är för nära, rör sig ormen bort
            if (distance <= 2)
            {
                int deltaX = 0;
                int deltaY = 0;

                // Bestäm rörelse bort från spelaren
                if (player.X < X) deltaX = 1; // Flytta höger
                else if (player.X > X) deltaX = -1; // Flytta vänster

                if (player.Y < Y) deltaY = 1; // Flytta ner
                else if (player.Y > Y) deltaY = -1; // Flytta upp

                // Försök att flytta
                Move(deltaX, deltaY, levelData);
            }
        }
    }
}
