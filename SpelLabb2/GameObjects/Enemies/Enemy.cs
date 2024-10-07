using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelLabb2.GameObjects.Enemies
{
    public abstract class Enemy : LevelElement.LevelElement
    {
        public string Name { get; set; }
        public int HP { get; set; }
        public Dice.Dice AttackDice { get; set; }
        public Dice.Dice DefenceDice { get; set; }

        public bool IsAlive { get; private set; } = true;
        public bool ShouldDraw { get; set; } = true;

        public Enemy(string name, int hp, char symbol, ConsoleColor color)
            
        {
            Name = name;
            HP = hp;
            Symbol = symbol;
            Color = color;
            
        }

        


        public abstract void Update(Player.Player player, LevelData levelData);


    }
}
