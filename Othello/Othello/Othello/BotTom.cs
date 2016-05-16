using System;

namespace Othello
{
    class BotTom : Player
    {
        public BotTom(Plateau plateau) : base(plateau)
        {
        }

        public override int Jouer()
        {
            if (plateau.CaseJouable.Count != 0)
            {
                Random rnd = new Random();
                int n = rnd.Next(0, plateau.CaseJouable.Count);
                Case c = plateau.getCaseJouable(n);
                return plateau.placer(c.X, c.Y);
            }
            return 25;
        }
    }
}
