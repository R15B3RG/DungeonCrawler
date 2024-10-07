using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelLabb2.GameObjects.Enemies
{
    public class Rat : Enemy
    {
        public Rat(int x, int y) : base("Rat", 10, 'r', ConsoleColor.Red)
        {

            AttackDice = new Dice.Dice(1, 6, 3);
            DefenceDice = new Dice.Dice(1, 6, 3);
            X = x;
            Y = y;
        }

        public override void Update(Player.Player player, LevelData levelData)
        {
            Random random = new Random();
            int direction = random.Next(4);
            int deltaX = 0;
            int deltaY = 0;

            switch (direction)
            {
                case 0: deltaY = -1; break; // Flytta upp
                case 1: deltaX = -1; break; // Flytta vänster
                case 2: deltaX = 1; break;  // Flytta höger
                case 3: deltaY = 1; break;  // Flytta ner
            }

            // Anropa Move för att flytta råttan
            Move(deltaX, deltaY, levelData);

            


        }



    }
}
