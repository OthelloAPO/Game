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
        private Plateau damier;

        private ObjetOthello jetonJoueur;
        private int etat; //0 remplir liste, 1 jouer piece, 2 partie fini;
        private int xJoueur;
        private int yJoueur;
        private bool toucheUp;

        private IA ordi;

        public Othello()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            damier = new Plateau();
            xJoueur = 4;
            yJoueur = 4;
            toucheUp = true;

            ordi = new IA(damier);
            etat = 0;
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }
        
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

        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
        protected override void Update(GameTime gameTime)
        {
            //Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            KeyboardState keyboard = Keyboard.GetState();

            if (etat == 0) // remplir le tableau
            {
                etat = 1;
            }
            else if (etat == 1) // jouer la piece
            {
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
                    ordi.Niveau++;
                }
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

            for (int x = 0; x < damier.X; x++)
            {
                for (int y = 0; y < damier.Y; y++)
                {
                    int xpos, ypos;
                    xpos = offsetX + x * 57;
                    ypos = offsetY + y * 57;
                    Vector2 pos = new Vector2(xpos, ypos);
                    if (damier.getCase(x, y) == 0)
                        spriteBatch.Draw(cadre.Texture, pos, Color.White);
                    else if (damier.getCase(x, y) == 2)
                        spriteBatch.Draw(pionNoir.Texture, pos, Color.White);
                    else if (damier.getCase(x, y) == 1)
                        spriteBatch.Draw(pionBlanc.Texture, pos, Color.White);
                    if (x == xJoueur && y == yJoueur)
                        spriteBatch.Draw(jetonJoueur.Texture, pos, Color.White);
                }
            }
            if (etat == 2)
            {
               // spriteBatch.DrawString(_font, "Fin.", text, Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);


        }
    }
}
