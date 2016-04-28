namespace Othello
{
    abstract class Player
    {
        protected Plateau plateau;

        public Player(Plateau plateau)
        {
            this.plateau = plateau;
        }

        public abstract int Jouer();
    }
}
