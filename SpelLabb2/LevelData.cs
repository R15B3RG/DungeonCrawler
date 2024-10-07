using SpelLabb2.GameObjects.Enemies;
using SpelLabb2.GameObjects.Enemies.Level;
using SpelLabb2.GameObjects.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelLabb2
{
    public class LevelData
    {
        private List<LevelElement.LevelElement> elements = new();

        public IReadOnlyList<LevelElement.LevelElement> Elements => elements;

        public Player player { get; set; }

        public void Load(string fileName)
        {
            var lines = File.ReadAllLines(fileName);
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    char ch = lines[y][x];
                    switch (ch)
                    {
                        case '#':
                            elements.Add(new Wall(x, y));
                            break;
                        case 'r':
                            elements.Add(new Rat(x, y));
                            break;
                        case 's':
                            elements.Add(new Snake(x, y));
                            break;
                        case '@':
                            player = new Player(x, y);
                            elements.Add(player);
                            break;
                    }
                }
            }
        }
        public bool IsWall(int x, int y)
        {
            foreach (var element in elements)
            {
                if (element is Wall && element.X == x && element.Y == y)
                {
                    return true; // Det är en vägg på denna position
                }
            }
            return false;
        }

        public bool IsOccupiedByElement(int x, int y)
        {
            foreach (var element in Elements)
            {
                if (element.X == x && element.Y == y && !(element is Player)) // Undvik att kolla spelaren själv
                {
                    return true; // Något annat element är på den här positionen
                }
            }
            return false;
        }

        public void RemoveEnemy(Enemy enemy)
        {
            elements.Remove(enemy);
        }
    }
}
