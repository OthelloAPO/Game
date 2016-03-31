using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Othello
{
    class Plateau
    {
        private int[,] damier;
        const int X = 8;
        const int Y = 8;
        private bool joueur; //True si joueur1

        public Plateau()
        {
            joueur = true;
            damier = new int[X, Y]{
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0}
            };
        }

        public bool Joueur
        {
            get { return joueur; }
            set { joueur = value; }
        }

        public void setCase(int x, int y, int value)
        {
            damier[x, y] = value;
        }

        public int getCase(int x, int y)
        {
            return damier[x, y];
        }


    }
}
