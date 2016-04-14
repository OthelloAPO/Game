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
        private Plateau damier;

        public IA(Plateau damier)
        {
            this.niveau = 1;
            this.damier = damier;
        }

        public int Niveau
        {
            get { return niveau; }
            set { niveau = value; }
        }

        public void Jouer()
        {
            if (damier.CaseJouable.Count != 0)
            {
                Random rnd = new Random();
                int n = rnd.Next(1, damier.CaseJouable.Count);
                Case c = damier.getCaseJouable(n - 1);
                damier.jouer(c.X, c.Y);
            }
        }


    }
}
    