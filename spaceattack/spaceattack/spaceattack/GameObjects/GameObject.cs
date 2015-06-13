using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spaceattack.Helpers;

namespace spaceattack.GameObjects
{
    abstract class GameObject
    {
        public Vector2 position;
        protected float scale;
        protected int width;
        protected int height;
        protected float rotation;
        protected bool alive = true;

        public Texture2D Texture;

        public bool Alive
        {
            get { return alive; }
            set { alive = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Rectangle Rectangle
        {
            get
            { return new Rectangle((int) position.X, (int) position.Y, width, height); }
        }
        public virtual bool OnScreen
        {
            get
            {
                return position.X + width > 0 && position.X < SpaceAttackGame.Width
                       && position.Y + height > 0 && position.Y < SpaceAttackGame.Height;
            }
        }

        public GameObject(Vector2 position, float scale, Texture2D texture)
        {
            this.position = position;
            this.scale = scale;
            this.Texture = texture;
            width = (int)(Texture.Width * scale);
            height = (int)(Texture.Height * scale);
        }

        public void Rotate(float angle)
        {
            rotation += angle;
        }
        public bool Collides(GameObject other)
        {
            return this.Rectangle.Intersects(other.Rectangle);
        }

        abstract public void Update();

        public virtual void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(Texture, Rectangle, null, Color.White, rotation, new Vector2(Texture.Width/2, Texture.Height/2), SpriteEffects.None, 0);
        }
    }
}
