using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                int retourner = 0;
                int j = 0;
                for (int i = 0; i < plateau.CaseJouable.Count; i++)
                {
                    Case n = plateau.getCaseJouable(i);
                    if (retourner < plateau.scanRetourner(n.X, n.Y))
                    {
                        retourner = plateau.scanRetourner(n.X, n.Y);
                        j = i;
                    }
                }
                return placer(plateau.getCaseJouable(j).X, plateau.getCaseJouable(j).Y);
            }
            return 25;
        }

        public int placer(int x, int y)
        {
            if (!plateau.dansListe(x, y))
                return 20;
            else if (plateau.Joueur)
                plateau.setCase(x, y, 1);
            else
                plateau.setCase(x, y, 2);

            plateau.retourner(x, y);
            plateau.Joueur = !plateau.Joueur;
            if (plateau.testComplet())
                return 30;
            else
                return 10;
        }
    }
}
