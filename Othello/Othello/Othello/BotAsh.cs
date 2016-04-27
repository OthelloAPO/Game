using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                int score = chercherCase(plapla.getCaseJouable(i), plapla, 4);

                for (i = 1; i < plateau.CaseJouable.Count; i++)
                {
                    plapla = plateau.copie();
                    int score2 = chercherCase(plapla.getCaseJouable(i), plapla, 4);
                    if (score < score2)
                    {
                        j = i;
                        score = score2;
                    }
                }
                return plateau.placer(plateau.getCaseJouable(j).X, plateau.getCaseJouable(j).Y);
            }
            return 25;
        }

        public int chercherCase(Case n, Plateau plapla, int tour)
        {
            int score;
            if (tour <= 0)
            {
                plapla.placer(n.X, n.Y);
                if (plapla.Joueur)
                    score = plapla.Score[0];
                else
                    score = plapla.Score[1];
                return score;
            }
            else
            {
                Plateau plipli = plapla.copie();
                plipli.placer(n.X, n.Y);
                Player bot = new BotEric(plipli);
                bot.Jouer();
                score = chercherCase(plipli.CaseJouable[0], plipli, tour - 1);

                for (int i = 1; i < plapla.CaseJouable.Count; i++)
                {
                    plipli = plapla.copie();
                    int scorePion = chercherCase(plipli.CaseJouable[i],plipli, tour - 1);
                    if (score < scorePion)
                        score = scorePion;
                }
                return score;
            }
        }        
    }
}
