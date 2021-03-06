using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Othello
{
    public class Othello : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont _font;

        private ObjetOthello cadre;
        private ObjetOthello pionBlanc;
        private ObjetOthello pionNoir;
        private ObjetOthello jetonJoueur1;
        private ObjetOthello jetonJoueur2;
        private ObjetOthello casejouable;

        private Plateau plateau;
        private int etat; //0 Menu de jeu, 10 remplir la liste, 20 il faut jouer , 25 � pas pu jouer, 30 fini les amis;
        private Player p1;
        private Player p2;

        public Othello()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            plateau = new Plateau();
            etat = 10;
            p1 = new Human(plateau);
            p2 = new BotOliver(plateau);
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
            jetonJoueur1 = new ObjetOthello(Content.Load<Texture2D>("objets\\jetonjoueur1"), new Vector2(0f, 0f), new Vector2(100f, 100f));
            jetonJoueur2 = new ObjetOthello(Content.Load<Texture2D>("objets\\jetonjoueur2"), new Vector2(0f, 0f), new Vector2(100f, 100f));
            casejouable = new ObjetOthello(Content.Load<Texture2D>("objets\\caseJouable"), new Vector2(0f, 0f), new Vector2(100f, 100f));

            _font = Content.Load<SpriteFont>("objets\\MyFont");
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }  // vide

        protected override void Update(GameTime gameTime)
        {
            // TODO: Add your update logic here
            switch (etat)
            {
                case 0:  //menu
                    break;
                case 10: // remplir liste
                    etat = plateau.scannerPlateau();
                    break;
                case 20: // choisir joueur et jouer
                    if (plateau.Joueur)
                        etat = p1.Jouer();
                    else
                    {
                        etat = p2.Jouer();
                    }
                    break;
                case 25: // un as pas pu jouer
                    etat = plateau.scannerPlateau();
                    if (etat == 25)
                        etat = 30;
                    break;
                case 30: // fini les amis
                    break;
                default:// beug 
                    break;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            // TODO: Add your drawing code here
            if (etat == 0) // affiche menu
            {
                GraphicsDevice.Clear(Color.ForestGreen);
                spriteBatch.Begin();
                spriteBatch.DrawString(_font, "Menu", new Vector2(185, 20), Color.White);
                spriteBatch.DrawString(_font, "Un joueur", new Vector2(160, 100), Color.White);
                spriteBatch.DrawString(_font, "Deux joueurs", new Vector2(145, 140), Color.White);
            }
            else if (etat == 30) // afficher score
            {
                GraphicsDevice.Clear(Color.ForestGreen);
                spriteBatch.Begin();
                spriteBatch.DrawString(_font, "Blanc :  " + plateau.Score[0], new Vector2(160, 100), Color.White);
                spriteBatch.DrawString(_font, "Noir :  " + plateau.Score[1], new Vector2(160, 140), Color.White);

                if (plateau.Score[0] > plateau.Score[1])
                    spriteBatch.DrawString(_font, "Les blancs remportent !", new Vector2(110, 20), Color.White);
                else if (plateau.Score[0] < plateau.Score[1])
                    spriteBatch.DrawString(_font, "Les noirs remportent !", new Vector2(110, 20), Color.White);
                else
                    spriteBatch.DrawString(_font, "Egalite...", new Vector2(160, 20), Color.White);
            }
            else // affiche grille
            {
                GraphicsDevice.Clear(Color.DarkGray);
                spriteBatch.Begin();
                int offsetX = 0;
                int offsetY = 0;
                Vector2 text = new Vector2(160, 0);

                for (int x = 0; x < plateau.X; x++)
                {
                    for (int y = 0; y < plateau.Y; y++)
                    {
                        int xpos, ypos;
                        xpos = offsetX + x * 57;
                        ypos = offsetY + y * 57;
                        Vector2 pos = new Vector2(xpos, ypos);
                        if (plateau.dansListe(x, y))
                        {
                            if (p1.GetType() == typeof(Human) && plateau.Joueur)
                                spriteBatch.Draw(casejouable.Texture, pos, Color.White);
                            else if (p2.GetType() == typeof(Human) && !plateau.Joueur)
                                spriteBatch.Draw(casejouable.Texture, pos, Color.White);
                            else
                                spriteBatch.Draw(cadre.Texture, pos, Color.White);
                        }
                        else if (plateau.getCase(x, y) == 0)
                            spriteBatch.Draw(cadre.Texture, pos, Color.White);
                        else if (plateau.getCase(x, y) == 2)
                            spriteBatch.Draw(pionNoir.Texture, pos, Color.White);
                        else if (plateau.getCase(x, y) == 1)
                            spriteBatch.Draw(pionBlanc.Texture, pos, Color.White);
                        if (p1.GetType() == typeof(Human) && plateau.Joueur)
                        {
                            Human h = (Human)p1;
                            if (x == h.XJoueur && y == h.YJoueur)
                                spriteBatch.Draw(jetonJoueur1.Texture, pos, Color.White);
                        }
                        if (p2.GetType() == typeof(Human) && !plateau.Joueur)
                        {
                            Human h = (Human)p2;
                            if (x == h.XJoueur && y == h.YJoueur)
                                spriteBatch.Draw(jetonJoueur2.Texture, pos, Color.White);
                        }
                    }
                }
            }


            spriteBatch.End();

            base.Draw(gameTime);


        }
    }
}

