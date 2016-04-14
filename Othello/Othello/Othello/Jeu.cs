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
    abstract class Jeu
    {
        protected Plateau damier;
        protected int xJoueur;
        protected int yJoueur;
        protected bool toucheUp;

        protected int etat; //0 remplir liste, 1 jouer piece, 2 partie fini;

        public Jeu()
        {
            damier = new Plateau();

            xJoueur = 0;
            yJoueur = 0;
            etat = 0;
            toucheUp = true;
        }

        public int XJoueur
        {
            get { return xJoueur; }
            set { xJoueur = value; }
        }

        public int YJoueur
        {
            get { return yJoueur; }
            set { yJoueur = value; }
        }

        public Plateau Damier
        {
            get { return damier; }
            set { damier = value; }
        }

        public int Etat
        {
            get { return etat; }
            set { etat = value; }
        }

        // signatures!!
        abstract public void updateGame();
        
    }
}
