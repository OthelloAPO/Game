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
                int k = 0;
                int score = 0;
                for (int i = 0; i < plateau.CaseJouable.Count; i++)
                {
                    Plateau plapla = plateau.copie();
                    plapla.placer(plapla.getCaseJouable(i).X, plapla.getCaseJouable(i).Y);
                    int score2 = Min(plapla, 4);
                    if (score < score2)
                    {
                        score = score2;
                        k = i;
                    }
                }
                return plateau.placer(plateau.getCaseJouable(k).X, plateau.getCaseJouable(k).Y);
            }
            else
            {
                return 25;
            }
        }

        public int Min(Plateau plapla, int tour)
        {
            int score;
            int minscore = 1000;
            if (tour <= 0)
            {
                return eval(plapla);
            }
            else
            {
                for (int i = 0; i < plapla.CaseJouable.Count; i++)
                {
                    Plateau plipli = plapla.copie();
                    plipli.placer(plipli.CaseJouable[i].X, plipli.CaseJouable[i].Y);
                    
                    score = Max(plipli, tour - 1);

                    if(score < minscore)
                    {
                        minscore = score;
                    }
                }
            }
            return minscore;
        }

        public int Max(Plateau plapla, int tour)
        {
            int score;
            int maxscore = -1000;
            if (tour <= 0)
            {
                return eval(plapla);
            }
            else
            {
                for (int i = 0; i < plapla.CaseJouable.Count; i++)
                {
                    Plateau plipli = plapla.copie();
                    plipli.placer(plipli.CaseJouable[i].X, plipli.CaseJouable[i].Y);
                    
                    score = Min(plipli, tour - 1);

                    if(score > maxscore)
                    {
                        maxscore = score;
                    }
                }
            }
            return maxscore;
        }

        public int eval(Plateau plapla)
        {
            int[,] grilleEval = new int[8, 8]
            {
                { 100,-10, 4, 2, 2, 4,-10, 100},
                {-10,-20, 1,-1,-1, 1,-20,-10},
                { 4, 1, 3, 0, 0, 3, 1, 4},
                { 2,-1, 0, 2, 2, 0,-1, 2},
                { 2,-1, 0, 2, 2, 0,-1, 2},
                { 4, 1, 3, 0, 0, 3, 1, 4},
                {-10,-20, 1,-1,-1, 1,-20,-10},
                { 100,-10, 4, 2, 2, 4,-10, 100}
            };
            
            int pion;
            int score;

            if (plapla.Joueur)
            {

                score = plapla.Score[1];
                pion = 2;
            }
            else
            {

                score = plapla.Score[0];
                pion = 1;
            }

            for (int i = 0; i < plapla.X; i++)
            {
                for (int j = 0; j < plapla.Y; j++)
                {
                    if (plapla.getCase(i, j) == pion)
                    {
                        score += 2*grilleEval[i, j];
                    }
                }
            }
            return score;
        }
    }
}