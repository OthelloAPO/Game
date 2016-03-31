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
        private const int _X = 8;
        private const int _Y = 8;
        private bool joueur; //True si joueur1

        public Plateau()
        {
            joueur = true;
            damier = new int[_X, _Y]{
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,1,2,0,0,0},
                {0,0,0,2,1,0,0,0},
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

        public int X
        {
            get { return _X; }
        }

        public int Y
        {
            get { return _Y; }
        }

        public void setCase(int x, int y, int value)
        {
            damier[x, y] = value;
        }

        public int getCase(int x, int y)
        {
            return damier[x, y];
        }

        public int jouer(int x, int y)
        {
            if (this.damier[x, y] != 0)
                return 0;

            else if (joueur)
                this.damier[x, y] = 1;

            else
                this.damier[x, y] = 2;

            joueur = !joueur;
            if (testFin())
                return 2;
            else
                return 1;


        }
    }
}
