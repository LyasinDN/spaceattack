using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spaceattack.Helpers;

namespace spaceattack.GameObjects
{
    class Star : GameObject
    {
        private int speed;

        public Star(Vector2 position, float scale)
            : base(position, scale, LoadHelper.Textures[TextureEnum.Star])
        {
            speed = (int) (50 * scale);
        }

        public override void Update()
        {
            //base.Update();
            position.Y += speed;
        }
    }
}
