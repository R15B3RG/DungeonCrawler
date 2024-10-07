﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpelLabb2.GameObjects.Enemies.Level
{
    public class Wall : LevelElement.LevelElement
    {
        public bool IsDiscovered { get; set; }

        public Wall(int x, int y) 
        {
            Symbol = '#';

            Color = ConsoleColor.Gray;

            X = x;
            Y = y;

            IsDiscovered = false;
          
        }
    }
}
