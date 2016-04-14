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
    public class Othello : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private ObjetOthello cadre;
        private ObjetOthello pionBlanc;
        private ObjetOthello pionNoir;
        private ObjetOthello jetonJoueur;

        private Boolean enJeu; // false si menu - true si en jeu
        private Jeu jeu;

        public Othello()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            enJeu = false;
            jeu = new UnJoueur();
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        } // vide
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            graphics.PreferredBackBufferWidth = 8 * 57 - 1;
            graphics.PreferredBackBufferHeight = 8 * 57 - 1;
            graphics.ApplyChanges();

            cadre = new ObjetOthello(Content.Load<Texture2D>("objets\\cadre"), new Vector2(0f, 0f), new Vector2(100f, 100f));
            pionBlanc = new ObjetOthello(Content.Load<Texture2D>("objets\\pionblanc"), new Vector2(0f, 0f), new Vector2(100f, 100f));
            pionNoir = new ObjetOthello(Content.Load<Texture2D>("objets\\pionnoir"), new Vector2(0f, 0f), new Vector2(100f, 100f));
            jetonJoueur = new ObjetOthello(Content.Load<Texture2D>("objets\\jetonjoueur"), new Vector2(0f, 0f), new Vector2(100f, 100f));
        } // a voir!

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }  // vide
        
        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            if (enJeu)
            {
                //menu
            }
            else
            {
                jeu.updateGame();
            }
            
            
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            // TODO: Add your drawing code here
            int offsetX = 0;
            int offsetY = 0;
            //SpriteFont _font;
            // _font = Content.Load<SpriteFont>("AfficherText");
            Vector2 text = new Vector2(160, 0);

            for (int x = 0; x < jeu.Damier.X; x++)
            {
                for (int y = 0; y < jeu.Damier.Y; y++)
                {
                    int xpos, ypos;
                    xpos = offsetX + x * 57;
                    ypos = offsetY + y * 57;
                    Vector2 pos = new Vector2(xpos, ypos);
                    if (jeu.Damier.getCase(x, y) == 0)
                        spriteBatch.Draw(cadre.Texture, pos, Color.White);
                    else if (jeu.Damier.getCase(x, y) == 2)
                        spriteBatch.Draw(pionNoir.Texture, pos, Color.White);
                    else if (jeu.Damier.getCase(x, y) == 1)
                        spriteBatch.Draw(pionBlanc.Texture, pos, Color.White);
                    if ( x == jeu.XJoueur && y == jeu.YJoueur)
                        spriteBatch.Draw(jetonJoueur.Texture, pos, Color.White);
                }
            }
            if (jeu.Etat == 2)
            {
                // spriteBatch.DrawString(_font, "Fin.", text, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);


        }
    }
}
