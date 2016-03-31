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
        private int etat; //0 non joué, 1 joué, 2 partie fini;
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

            if (damier.Joueur)
            {
                if (keyboard.IsKeyDown(Keys.Right) && xJoueur < damier.X && toucheUp)
                {
                    xJoueur++;
                    toucheUp = false;
                }
                else if (keyboard.IsKeyDown(Keys.Left) && xJoueur > 0 && toucheUp)
                {
                    xJoueur--;
                    toucheUp = false;
                }
                else if (keyboard.IsKeyDown(Keys.Up) && yJoueur < damier.Y && toucheUp)
                {
                    yJoueur++;
                    toucheUp = false;
                }
                else if (keyboard.IsKeyDown(Keys.Down) && yJoueur > damier.Y && toucheUp)
                {
                    yJoueur--;
                    toucheUp = false;
                }

                else if (keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.Down) && keyboard.IsKeyDown(Keys.Up) && keyboard.IsKeyDown(Keys.Space) && !toucheUp)
                    toucheUp = true;

                else if (keyboard.IsKeyDown(Keys.Space) && toucheUp)
                {
                    etat = damier.jouer(xJoueur, yJoueur);
                    toucheUp = false;
                }
            }


            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
