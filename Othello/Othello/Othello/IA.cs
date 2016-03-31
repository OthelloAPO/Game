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
    class IA
    {
        private int niveau;
        private Random rnd;
        private Plateau damier;

        public IA(Plateau damier)
        {
            rnd = new Random();
            this.niveau = 1;
            this.damier = damier;
        }

        public int Niveau
        {
            get { return niveau; }
            set { niveau = value; }
        }


    }
}
    