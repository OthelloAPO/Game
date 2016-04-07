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
        private ArrayList caseJouable;
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
            caseJouable = new ArrayList();
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
        
        public bool Joueur
        {
            get { return joueur; }
            set { joueur = value; }
        }

        public int X
        {
            get { return _X; }
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

        public int jouer(int x, int y)
        {
            //if je peux manger
            if (this.damier[x, y] != 0)
                return 0;
            else if (joueur)
                this.damier[x, y] = 1;
            else
                this.damier[x, y] = 2;

            retourner(x, y);
            joueur = !joueur;
            if (testFin())
                return 2;
            else
                return 1;
        }

        public bool testFin() //true si fini
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
                score[0] += points;
                score[1] -= points;
            }
            else
            {
                score[0] -= points;
                score[1] += points;
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
                            damier[j, y] = couleur;
                            compteur++;
                    }
                }
            }
            //a droite
            if (x < _X-1)
            {
                for (int i = x + 1; i <= _X; i++)
                {
                    if (damier[i, y] == 0)
                        break;
                    if (damier[i, y] == couleur)
                    {
                        for (int j = x + 1; j < i; j++)
                            damier[j, y] = couleur;
                            compteur++;
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
                            damier[x, j] = couleur;
                            compteur++;
                    }
                }
            }
            //en bas
            if (y < _Y - 1)
            {
                for (int i = y + 1; i <= _Y; i++)
                {
                    if (damier[x, i] == 0)
                        break;
                    if (damier[x, i] == couleur)
                    {
                        for (int j = y + 1; j < i; j++)
                            damier[x, j] = couleur;
                            compteur++;
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
                while(i != _X || j != 0)
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
                while (i != 0 || j != _Y)
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
                while (i != 0 || j != 0)
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
                while (i != _X || j != _Y)
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
            caseJouable = new ArrayList();
            for (int i = 0; i < _X; i++)
            {
                for (int j = 0; j < _Y; j++)
                {
                    if (damier[i, j] == 0 && scanRetourner(i, j) != 0)
                    {
                        caseJouable.Add(i);
                        caseJouable.Add(j);
                    }
                }
            }
            return 1;
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

            int nbRetourne = 0;
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
                            nbRetourne++;
                    }
                }
            }
            //a droite
            if (x < _X - 1)
            {
                for (int i = x + 1; i <= _X; i++)
                {
                    if (damier[i, y] == 0)
                        break;
                    if (damier[i, y] == couleur)
                    {
                        for (int j = x + 1; j < i; j++)
                            nbRetourne++;
                    }
                }
            }
            return nbRetourne;
        }
        public int scanerColonne(int x, int y)
        {
            int couleur;
            if (joueur)
                couleur = 1;
            else
                couleur = 2;

            int nbRetourne = 0;
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
                            nbRetourne++;
                    }
                }
            }
            //en bas
            if (y < _Y - 1)
            {
                for (int i = y + 1; i <= _Y; i++)
                {
                    if (damier[x, i] == 0)
                        break;
                    if (damier[x, i] == couleur)
                    {
                        for (int j = y + 1; j < i; j++)
                            nbRetourne++;
                    }
                }
            }
            return nbRetourne;
        }
        public int scanerDiagD(int x, int y)
        {
            int couleur;
            if (joueur)
                couleur = 1;
            else
                couleur = 2;

            int nbRetourne = 0;
            //en haut
            if (y > 1 && x < _X - 1)
            {
                int i = x + 1;
                int j = y - 1;
                while (i != _X || j != 0)
                {
                    if (damier[i, j] == 0)
                        break;
                    if (damier[i, j] == couleur)
                    {
                        for (int k = x - i + 1; k < 0; k++)
                        {
                            nbRetourne++;
                        }
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
                while (i != 0 || j != _Y)
                {
                    if (damier[i, j] == 0)
                        break;
                    if (damier[i, j] == couleur)
                    {
                        for (int k = x - i - 1; k > 0; k--)
                        {
                            nbRetourne++;
                        }
                    }
                    i--;
                    j++;
                }
            }
            return nbRetourne;
        }
        public int scanerDiagG(int x, int y)
        {
            int couleur;
            if (joueur)
                couleur = 1;
            else
                couleur = 2;

            int nbRetourne = 0;
            //en haut
            if (y > 1 && x > 1)
            {
                int i = x - 1;
                int j = y - 1;
                while (i != 0 || j != 0)
                {
                    if (damier[i, j] == 0)
                        break;
                    if (damier[i, j] == couleur)
                    {
                        for (int k = x - i - 1; k > 0; k--)
                        {
                            nbRetourne++;
                        }
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
                while (i != _X || j != _Y)
                {
                    if (damier[i, j] == 0)
                        break;
                    if (damier[i, j] == couleur)
                    {
                        for (int k = x - i + 1; k < 0; k++)
                        {
                            nbRetourne++;
                        }
                    }
                    i++;
                    j++;
                }
            }
            return nbRetourne;
        }


    }
}
