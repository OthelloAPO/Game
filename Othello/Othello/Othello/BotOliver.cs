using System;

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
