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
                int j = i;
                Plateau plapla = plateau.copie();
                int score = chercherCase(plapla.getCaseJouable(i), plapla, 0);
                for (i = 1; i < plateau.CaseJouable.Count; i++)
                {
                    Plateau plipli = plateau.copie();
                    int score2 = chercherCase(plipli.getCaseJouable(i), plapla, 0);
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

        public int chercherCase(Case n, Plateau plateau, int tour)
        {
            int score;
            if (tour <= 0)
            {
                plateau.placer(n.X, n.Y);
                if (plateau.Joueur)
                    score = plateau.Score[0];
                else
                    score = plateau.Score[1];
                return score;
            }
            else
            {
                Plateau plapla = plateau.copie();
                plapla.placer(n.X, n.Y);
                Player bot = new BotEric(plapla);
                bot.Jouer();

                score = chercherCase(plapla.CaseJouable[0], plateau, tour - 1);
                for (int i = 1; i < plateau.CaseJouable.Count; i++)
                {
                    Plateau plipli = plapla.copie();
                    int scorePion = chercherCase(plipli.CaseJouable[i],plateau, tour - 1);
                    if (score < scorePion)
                        score = scorePion;
                }
                return score;
            }
        }        
    }
}
