using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Othello
{
    abstract class Player
    {
        protected Plateau plateau;

        public Player(Plateau plateau)
        {
            this.plateau = plateau;
        }

        public abstract int Jouer();
    }
}
