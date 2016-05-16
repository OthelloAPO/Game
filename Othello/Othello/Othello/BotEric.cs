namespace Othello
{
    class BotEric : Player 
    {
         public BotEric(Plateau plateau) : base(plateau)
        {
        }

        public override int Jouer()
        {
            if (plateau.CaseJouable.Count != 0)
            {
                int j = 0;
                Case n;
                int retourner = 0;

                for (int i = 0; i < plateau.CaseJouable.Count; i++)
                {
                    n = plateau.getCaseJouable(i);
                    if (retourner < plateau.scanRetourner(n.X, n.Y))
                    {
                        retourner = plateau.scanRetourner(n.X, n.Y);
                        j = i;
                    }
                }
                return plateau.placer(plateau.getCaseJouable(j).X, plateau.getCaseJouable(j).Y);
            }
            else
            {
                return 25;
            }
        }
    }
}
