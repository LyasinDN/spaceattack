using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spaceattack.Helpers;

namespace spaceattack.GameObjects
{
    class Asteroid : GameObject
    {
        private float rotationSpeed;
        public Asteroid(Vector2 position, float scale) : 
            base(position, scale, LoadHelper.Textures[TextureEnum.Asteroid])
        {
            rotationSpeed = MathHelper.ToRadians(RandomHelper.Next(10)+1);
        }

        public override void Update()
        {
            position.Y += 10;
            Rotate( rotationSpeed );
        }
    }
}
