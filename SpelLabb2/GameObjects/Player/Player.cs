﻿
using SpelLabb2.GameObjects.Enemies;

namespace SpelLabb2.GameObjects.Player
{
    public class Player : LevelElement.LevelElement
    {
        public string Name = "Player";

        public int Health = 100;

        public int Moves = 0;
        public Dice.Dice AttackDice { get; set; }
        public Dice.Dice DefenceDice { get; set; }

        public Player(int x, int y)
        {
            

            AttackDice = new Dice.Dice(2, 6, 2);
            DefenceDice = new Dice.Dice(2, 6, 2);
            Symbol = '@';
            Color = ConsoleColor.Yellow;

            X = x;
            Y = y; 


        }
        public void Inputs(ConsoleKey key, LevelData levelData)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow: Move(0, -1, levelData); break;
                case ConsoleKey.DownArrow: Move(0, 1, levelData); break;
                case ConsoleKey.LeftArrow: Move(-1, 0, levelData); break;
                case ConsoleKey.RightArrow: Move(1, 0, levelData); break;
                case ConsoleKey.Spacebar: Move(0, 0, levelData); break;
            }

            Moves++;

            int deltaX = 0;
            int deltaY = 0;

            Move(deltaX, deltaY, levelData);
        }

    }
}
