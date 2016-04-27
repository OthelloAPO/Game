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
    class BotOliver : Player
    {
        public BotOliver(Plateau plateau) : base(plateau)
        {
        }

        public override int Jouer()
        {
            if (plateau.CaseJouable.Count != 0)
            {
                Random rnd = new Random();
                int n = rnd.Next(1, plateau.CaseJouable.Count);
                Case c = plateau.getCaseJouable(n - 1);
                return plateau.placer(c.X, c.Y);
            }
            return 25;
        }
    }
}
