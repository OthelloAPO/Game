using Microsoft.Xna.Framework.Input;

namespace Othello
{
    class Human : Player
    {

        private int xJoueur;
        private int yJoueur;
        private bool toucheUp;

        public Human(Plateau plateau) : base(plateau)
        {
            xJoueur = 0;
            yJoueur = 0;
            toucheUp = true;
        }

        public int XJoueur
        {
            get { return xJoueur; }
            set { xJoueur = value; }
        }

        public int YJoueur
        {
            get { return yJoueur; }
            set { yJoueur = value; }
        }

        public override int Jouer()
        {
            KeyboardState keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.Right) && xJoueur < plateau.X - 1 && toucheUp)
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
            else if (keyboard.IsKeyDown(Keys.Down) && yJoueur < plateau.Y - 1 && toucheUp)
            {
                yJoueur++;
                toucheUp = false;
            }
            else if (keyboard.IsKeyDown(Keys.Space) && toucheUp)
            {
                toucheUp = false;
                return plateau.placer(xJoueur, yJoueur);
            }
            else if (keyboard.IsKeyUp(Keys.Left) && keyboard.IsKeyUp(Keys.Right) && keyboard.IsKeyUp(Keys.Down) && keyboard.IsKeyUp(Keys.Up) && keyboard.IsKeyUp(Keys.Space) && !toucheUp)
            {
                toucheUp = true;
            }
            return 20;
        }
    }
}
