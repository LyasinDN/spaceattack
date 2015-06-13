using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spaceattack.Helpers;

namespace spaceattack.GameObjects
{
    class Bullet : GameObject
    {
        private ShooterEnum owner;

        public ShooterEnum Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public Bullet(Vector2 position, float scale, ShooterEnum owner) : base(position, scale, LoadHelper.Textures[TextureEnum.Bullet])
        {
            this.owner = owner;
            height *= 3;
        }
        public override void Update()
        {
            if (owner == ShooterEnum.Player)
                position.Y -= 11;
            else
                position.Y += 11;
            //base.Update();
        }
    }
    public enum ShooterEnum
    {
        Player,
        Computer
    }
}
