using System;

namespace Othello
{
    class BotDave : Player
    {

        public BotDave(Plateau plateau) : base(plateau)
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
            else return 25;
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
                { 50,-5, 4, 2, 2, 4,-5, 50},
                {-5,-10,-1,-1,-1,-1,-10,-5},
                { 4,-1, 1, 0, 0, 1,-1, 4},
                { 2,-1, 0, 1, 1, 0,-1, 2},
                { 2,-1, 0, 1, 1, 0,-1, 2},
                { 4,-1, 1, 0, 0, 1,-1, 4},
                {-5,-10,-1,-1,-1,-1,-10,-5},
                { 50,-5, 4, 2, 2, 3,-5, 50}
            };

            int score = 0;

            for (int i = 0; i < plapla.X; i++)
            {
                for (int j = 0; j < plapla.Y; j++)
                {
                    if (plapla.getCase(i, j) == 2)
                    {
                        score += 2*grilleEval[i, j];
                    }
                }
            }
            return score;
        }
    }
}