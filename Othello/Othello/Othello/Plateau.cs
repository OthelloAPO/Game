using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections;

namespace Othello
{
    class Plateau
    {
        private int[,] damier;
        private const int _X = 8;
        private const int _Y = 8;
        private bool joueur; //True si joueur1
        private List<Case> caseJouable;
        private int[] score;

        public Plateau()
        {
            joueur = true;
            damier = new int[_X, _Y]{
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,1,2,0,0,0},
                {0,0,0,2,1,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0}
            };
            caseJouable = new List<Case>();
            score = new int[2] { 2, 2 };
        }

        public void nouvellePartie()
        {
            joueur = true;
            damier = new int[_X, _Y]{
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,1,2,0,0,0},
                {0,0,0,2,1,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0},
                {0,0,0,0,0,0,0,0}
            };
            score = new int[2] { 2, 2 };
        }

        public int[] Score
        {
            get { return score; }
            set { score = value; }
        }

        public bool Joueur
        {
            get { return joueur; }
            set { joueur = value; }
        }

        public int X
        {
            get { return _X; }
        }

        public List<Case> CaseJouable
        {
            get { return caseJouable; }
            set { caseJouable = value; }
        }

        public Case getCaseJouable(int x)
        {
            return caseJouable[x];
        }

        public int Y
        {
            get { return _Y; }
        }

        public void setCase(int x, int y, int value)
        {
            damier[x, y] = value;
        }

        public int getCase(int x, int y)
        {
            return damier[x, y];
        }

        public Boolean dansListe(int x, int y)
        {
            for (int i = 0; i < caseJouable.Count ; i++)
            {
                if (caseJouable[i].estEgale(x, y))
                    return true;
            }

            return false; 
        }        

        public bool testComplet() //true si fini
        {
            for (int i = 0; i < _X; i++)
            {
                for (int j = 0; j < _Y; j++)
                {
                    if (damier[i, j] == 0)
                        return false;
                }
            }
            return true;
        }

        public Plateau copie()
        {
            Plateau plapla = new Plateau();
            for(int i=0; i<_X; i++)
            {
                for(int j=0; j<_Y; j++)
                {
                    plapla.damier[i, j] = this.damier[i, j];
                }
            }
            for(int k =0; k<this.caseJouable.Count; k++)
            {
                plapla.caseJouable.Add(this.caseJouable[k]);
            }
            plapla.joueur = this.joueur;
            plapla.score[0] = this.score[0];
            plapla.score[1] = this.score[1];
            return plapla;
        }

        public int placer(int x, int y)
        {
            if (!dansListe(x, y))
                return 20;
            else if (Joueur)
                setCase(x, y, 1);
            else
                setCase(x, y, 2);

            retourner(x, y);
            Joueur = !Joueur;
            if (testComplet())
                return 30;
            else
                return 10;
        }
        /// /////////////////////////////////////////////////////////////////////////////////////////////////////
        /// retourner

        public void retourner(int x, int y)
        {
            int points= retournerLigne(x, y) + // comme ca : | 
            retournerColonne(x, y) + // comme ca : -
            retournerDiagD(x, y) + // comme ca : /
            retournerDiagG(x, y); // comme ca : \
            if (joueur)
            {
                score[0] += points+1;
                score[1] -= points;
            }
            else
            {
                score[0] -= points;
                score[1] += points+1;
            }
        }
        
        public int retournerLigne(int x, int y)
        {
            int couleur = damier[x, y];
            int compteur=0;
            //a gauche
            if (x > 1)
            {
                for (int i = x - 1; i >= 0; i--)
                {
                    if (damier[i, y] == 0)
                        break;
                    if (damier[i, y] == couleur)
                    {
                        for (int j = x - 1; j > i; j--)
                        {
                            damier[j, y] = couleur;
                            compteur++;
                        }
                        break;
                    }
                }
            }
            //a droite
            if (x < _X-1)
            {
                for (int i = x + 1; i < _X; i++)
                {
                    if (damier[i, y] == 0)
                        break;
                    if (damier[i, y] == couleur)
                    {
                        for (int j = x + 1; j < i; j++)
                        {
                            damier[j, y] = couleur;
                            compteur++;
                        }
                        break;
                    }
                }
            }
            return compteur;
        }
        public int retournerColonne(int x, int y)
        {
            int couleur = damier[x, y];
            int compteur = 0;
            //en haut
            if (y > 1)
            {
                for (int i = y - 1; i >= 0; i--)
                {
                    if (damier[x, i] == 0)
                        break;
                    if (damier[x, i] == couleur)
                    {
                        for (int j = y - 1; j > i; j--)
                        {
                            damier[x, j] = couleur;
                            compteur++;
                        }
                        break;
                    }
                }
            }
            //en bas
            if (y < _Y - 1)
            {
                for (int i = y + 1; i < _Y; i++)
                {
                    if (damier[x, i] == 0)
                        break;
                    if (damier[x, i] == couleur)
                    {
                        for (int j = y + 1; j < i; j++)
                        {
                            damier[x, j] = couleur;
                            compteur++;
                        }
                        break;
                    }
                }
            }
            return compteur;
        }
        public int retournerDiagD(int x, int y)
        {
            int couleur = damier[x, y];
            int compteur = 0;
            //en haut
            if (y > 1 && x <_X - 1)
            {
                int i = x + 1;
                int j = y - 1;
                while(i != _X && j != 0)
                {
                    if (damier[i, j] == 0)
                        break;
                    if (damier[i, j] == couleur)
                    {
                        for (int k = x - i + 1; k < 0; k++)
                        {
                            damier[x - k, y + k] = couleur;
                            compteur++; 
                        }
                        break;
                    }
                    i++;
                    j--;
                }
                 
            }
            //en bas
            if (y < _Y - 1 && x > 1)
            {
                int i = x - 1;
                int j = y + 1;
                while (i != 0 && j != _Y)
                {
                    if (damier[i, j] == 0)
                        break;
                    if (damier[i, j] == couleur)
                    {
                        for (int k = x - i - 1; k > 0; k--)
                        {
                            damier[x - k, y + k] = couleur;
                            compteur++;
                        }
                        break;
                    }
                    i--;
                    j++;
                }
            }
            return compteur;
        }
        public int retournerDiagG(int x, int y)
        {
            int couleur = damier[x, y];
            int compteur = 0;
            //en haut
            if (y > 1 && x > 1)
            {
                int i = x - 1;
                int j = y - 1;
                while (i != 0 && j != 0)
                {
                    if (damier[i, j] == 0)
                        break;
                    if (damier[i, j] == couleur)
                    {
                        for (int k = x - i - 1; k > 0; k--)
                        {
                            damier[x - k, y - k] = couleur;
                            compteur++;
                        }
                        break;
                    }
                    i--;
                    j--;
                }

            }
            //en bas
            if (y < _Y - 1 && x < _X - 1)
            {
                int i = x + 1;
                int j = y + 1;
                while (i != _X && j != _Y)
                {
                    if (damier[i, j] == 0)
                        break;
                    if (damier[i, j] == couleur)
                    {
                        for (int k = x - i + 1; k < 0; k++)
                        {
                            damier[x - k, y - k] = couleur;
                            compteur++;
                        }
                        break;
                    }
                    i++;
                    j++;
                }
            }
            return compteur;
        }

        /// /////////////////////////////////////////////////////////////////////////////////////////////////////
        /// Scan retourner

        public int scannerPlateau()
        {
            caseJouable = new List<Case>();
            for (int i = 0; i < _X; i++)
            {
                for (int j = 0; j < _Y; j++)
                {
                    if (damier[i, j] == 0 && scanRetourner(i, j) != 0)
                    {
                        Case c = new Case(i, j);
                        caseJouable.Add(c);
                    }
                }
            }
            if (caseJouable.Count != 0)
                return 20;
            else
            {
                joueur = !joueur;
                return 25;
            }
        }

        public int scanRetourner(int x, int y)
        {
            return scanerLigne(x, y) + // comme ca : | 
            scanerColonne(x, y) + // comme ca : -
            scanerDiagD(x, y) + // comme ca : /
            scanerDiagG(x, y); // comme ca : \
        }

        public int scanerLigne(int x, int y)
        {
            int couleur;
            if (joueur)
                couleur = 1;
            else
                couleur = 2;

            int compteur = 0;
            //a gauche
            if (x > 1)
            {
                for (int i = x - 1; i >= 0; i--)
                {
                    if (damier[i, y] == 0)
                        break;
                    if (damier[i, y] == couleur)
                    {
                        for (int j = x - 1; j > i; j--)
                        {
                            compteur++;
                        }
                        break;
                    }
                }
            }
            //a droite
            if (x < _X - 1)
            {
                for (int i = x + 1; i < _X; i++)
                {
                    if (damier[i, y] == 0)
                        break;
                    if (damier[i, y] == couleur)
                    {
                        for (int j = x + 1; j < i; j++)
                        {
                            compteur++;
                        }
                        break;
                    }
                }
            }
            return compteur;
        }
        public int scanerColonne(int x, int y)
        {
            int couleur;
            if (joueur)
                couleur = 1;
            else
                couleur = 2;

            int compteur = 0;
            //en haut
            if (y > 1)
            {
                for (int i = y - 1; i >= 0; i--)
                {
                    if (damier[x, i] == 0)
                        break;
                    if (damier[x, i] == couleur)
                    {
                        for (int j = y - 1; j > i; j--)
                        {
                            compteur++;
                        }
                        break;
                    }
                }
            }
            //en bas
            if (y < _Y - 1)
            {
                for (int i = y + 1; i < _Y; i++)
                {
                    if (damier[x, i] == 0)
                        break;
                    if (damier[x, i] == couleur)
                    {
                        for (int j = y + 1; j < i; j++)
                        {
                            compteur++;
                        }
                        break;
                    }
                }
            }
            return compteur;
        }
        public int scanerDiagD(int x, int y)
        {
            int couleur;
            if (joueur)
                couleur = 1;
            else
                couleur = 2;

            int compteur = 0;
            //en haut
            if (y > 1 && x < _X - 1)
            {
                int i = x + 1;
                int j = y - 1;
                while (i != _X && j != 0)
                {
                    if (damier[i, j] == 0)
                        break;
                    if (damier[i, j] == couleur)
                    {
                        for (int k = x - i + 1; k < 0; k++)
                        {
                            compteur++;
                        }
                        break;
                    }
                    i++;
                    j--;
                }

            }
            //en bas
            if (y < _Y - 1 && x > 1)
            {
                int i = x - 1;
                int j = y + 1;
                while (i != 0 && j != _Y)
                {
                    if (damier[i, j] == 0)
                        break;
                    if (damier[i, j] == couleur)
                    {
                        for (int k = x - i - 1; k > 0; k--)
                        {
                            compteur++;
                        }
                        break;
                    }
                    i--;
                    j++;
                }
            }
            return compteur;
        }
        public int scanerDiagG(int x, int y)
        {
            int couleur;
            if (joueur)
                couleur = 1;
            else
                couleur = 2;

            int compteur = 0;
            //en haut
            if (y > 1 && x > 1)
            {
                int i = x - 1;
                int j = y - 1;
                while (i != 0 && j != 0)
                {
                    if (damier[i, j] == 0)
                        break;
                    if (damier[i, j] == couleur)
                    {
                        for (int k = x - i - 1; k > 0; k--)
                        {
                            compteur++;
                        }
                        break;
                    }
                    i--;
                    j--;
                }

            }
            //en bas
            if (y < _Y - 1 && x < _X - 1)
            {
                int i = x + 1;
                int j = y + 1;
                while (i != _X && j != _Y)
                {
                    if (damier[i, j] == 0)
                        break;
                    if (damier[i, j] == couleur)
                    {
                        for (int k = x - i + 1; k < 0; k++)
                        {
                            compteur++;
                        }
                        break;
                    }
                    i++;
                    j++;
                }
            }
            return compteur;
        }


    }
}
