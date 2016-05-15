namespace Othello
{
    class BotAsh : Player
    {
        public BotAsh(Plateau plateau) : base(plateau)
        {
        }

        public override int Jouer()
        {
            if (plateau.CaseJouable.Count != 0)
            {
                int i = 0;
                int j = 0;
                Plateau plapla = plateau.copie();
                int score = chercherCase(plapla.getCaseJouable(i), plapla, 1);

                for (i = 0; i < plateau.CaseJouable.Count; i++)
                {
                    plapla = plateau.copie();
                    int score2 = chercherCase(plapla.getCaseJouable(i), plapla, 1);
                    if (score < score2)
                    {
                        j = i;
                        score = score2;
                    }
                }
                return plateau.placer(plateau.getCaseJouable(j).X, plateau.getCaseJouable(j).Y);
            }
            else return 25;
        }

        public int chercherCase(Case n, Plateau plapla, int tour)
        {
            int score;
            if (tour <= 0)
            {
                plapla.placer(n.X, n.Y);
                return simpleEval(plapla); // choix de l'eval
            }
            else
            {
                Plateau plipli = plapla.copie();
                plipli.placer(n.X, n.Y);
                Player bot = new BotEric(plipli);
                bot.Jouer();
                score = chercherCase(plipli.CaseJouable[0], plipli, tour -1 );

                for (int i = 0; i < plapla.CaseJouable.Count; i++)
                {
                    plipli = plapla.copie();
                    int scorePion = chercherCase(plipli.CaseJouable[i],plipli, tour -1);
                    if (score < scorePion)
                        score = scorePion;
                }
                return score;
            }
        }

        public int simpleEval(Plateau plapla)
        {
            if (plapla.Joueur)
                return plapla.Score[0];
            else
                return plapla.Score[1];
        }

        public int normalEval(Plateau plapla)
        {
            int[,] grilleEval = new int[8, 8]{
                {4,-3,2,2,2,2,-3,4},
                {-3,-4,-1,-1,-1,-1,-4,-3},
                {2,-1,1,0,0,1,-1,2},
                {2,-1,0,1,1,0,-1,2},
                {2,-1,0,1,1,0,-1,2},
                {2,-1,1,0,0,1,-1,2},
                {-3,-4,-1,-1,-1,-1,-4,-3},
                {4,-3,2,2,2,2,-3,4} 
            };

            int score = 0;
            int pion;
            if (plapla.Joueur)
                pion = 1;
            else
                pion = 2;

            for (int i = 0; i < plapla.X; i++)
            {
                for (int j = 0; j < plapla.Y; j++)
                {
                    if (plapla.getCase(i,j) == pion)
                        score += 10*grilleEval[i, j];
                }
            }
            return score;
        }
    }
}
