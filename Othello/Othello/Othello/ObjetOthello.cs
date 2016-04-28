using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Othello
{
    class ObjetOthello
    {
        private Texture2D texture;
        private Vector2 position;
        private Vector2 size;


        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector2 Site
        {
            get { return size; }
            set { size = value; }
        }

        public ObjetOthello(Texture2D texture, Vector2 position, Vector2 size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
        }

    }
}
