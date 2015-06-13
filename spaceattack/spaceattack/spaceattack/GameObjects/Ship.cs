using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using spaceattack.Helpers;

namespace spaceattack.GameObjects
{
    class Ship : GameObject
    {
        int lives = 6;

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public Ship(Vector2 position, float scale)
            : base(position, scale, LoadHelper.Textures[TextureEnum.Ship])
        {
        }

        public void Move(int dx, int dy)
        {
            if (position.X + dx > 0 && position.X + dx + width < SpaceAttackGame.Width)
                position.X += dx;

            if (position.Y + dy > 0 && position.Y + dy + height < SpaceAttackGame.Height)
                position.Y += dy;
        }

        public void Place(int x, int y)
        {
            position.X = x;
            position.Y = y;
        }
        public override void Update()
        {
            //base.Update();
           // position.Y += speed;
        }
    }
}
