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

namespace Othello
{
    class UnJoueur : Jeu
    {
        private IA ordi;

        public UnJoueur() : base()
        {
            ordi = new IA(damier);
        }

        override public void updateGame()
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (etat == 0) // remplir le tableau
            {
                etat = damier.scannerPlateau();
            }
            else if (etat < 2) // jouer la piece
            { //ajout condition si tableau vide etat =1.5
                if (damier.Joueur)
                {
                    if (keyboard.IsKeyDown(Keys.Right) && xJoueur < damier.X - 1 && toucheUp)
                    {
                        xJoueur++;
                        toucheUp = false;
                    }
                    else if (keyboard.IsKeyDown(Keys.Left) && xJoueur > 0 && toucheUp)
                    {
                        xJoueur--;
                        toucheUp = false;
                    }
                    else if (keyboard.IsKeyDown(Keys.Up) && yJoueur > 0 && toucheUp)
                    {
                        yJoueur--;
                        toucheUp = false;
                    }
                    else if (keyboard.IsKeyDown(Keys.Down) && yJoueur < damier.Y - 1 && toucheUp)
                    {
                        yJoueur++;
                        toucheUp = false;
                    }
                    else if (keyboard.IsKeyDown(Keys.Space) && toucheUp)
                    {
                        etat = damier.jouer(xJoueur, yJoueur);
                        toucheUp = false;
                    }
                    else if (keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.Down) && keyboard.IsKeyUp(Keys.Up) && keyboard.IsKeyUp(Keys.Space) && !toucheUp)
                    {
                        toucheUp = true;
                    }
                }
                else
                {
                    etat = 0;
                    ordi.Jouer();
                    damier.Joueur = true; // a modifier par une IA
                }
            }
            else if (etat >= 2) // fin de partie
            {
                if (keyboard.IsKeyDown(Keys.Enter))
                {
                    damier.nouvellePartie();
                    xJoueur = 4;
                    yJoueur = 4;
                    toucheUp = true;
                    etat = 0;
                }
            }

        }

    }
}
