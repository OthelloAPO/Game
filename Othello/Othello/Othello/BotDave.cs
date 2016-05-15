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
                Plateau plapla = plateau.copie();
                int score = 0;
                for (int i = 0; i < plapla.CaseJouable.Count; i++)
                {
                    int score2 = Max(plapla, 2);
                    if (score < score2)
                    {
                        score = score2;
                        k = i;
                    }
                }
                Console.WriteLine(eval(plateau));
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

                    if(score > minscore)
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

                    if(score < maxscore)
                    {
                        maxscore = score;
                    }
                }
            }
            return maxscore;
        }

        public int eval(Plateau plapla)
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
            for (int i = 0; i < plapla.X; i++)
            {
                for (int j = 0; j < plapla.Y; j++)
                {
                    if (plapla.getCase(i, j) == 2)
                    {
                        score += 10 * grilleEval[i, j];
                    }
                    else if (plapla.getCase(i, j) == 1)
                    {
                        score -= 10 * grilleEval[i, j];
                    }
                }
            }
            return score;
        }
    }
}