using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelLabb2.Dice
{
    public class Dice
    {
        public int NumberOfDice { get; set; }
        public int SidesPerDie { get; set; }
        public int Modifier { get; set; }

        public Dice(int numberOfDice, int sidesPerDie, int modifier)
        {
            NumberOfDice = numberOfDice;
            SidesPerDie = sidesPerDie;
            Modifier = modifier;
        }

        public int Throw()
        {
            Random rand = new Random();
            int result = 0;

            for (int i = 0; i < NumberOfDice; i++)
            {
                result += rand.Next(NumberOfDice, SidesPerDie + Modifier);
            }

            result += Modifier;
            return result;
        }

        public override string ToString()
        {
            return $"{NumberOfDice}d{SidesPerDie}+{Modifier}";
        }
    }
}
