using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spaceattack.Helpers;

namespace spaceattack.GameObjects
{
    class Enemy : GameObject
    {
        private IEnemyStrategy strategy;
        private int shootTimeOut = RandomHelper.Next(10) + 50;
        public bool ReadyToShoot
        {
            get
            {
                return shootTimeOut == 0;
            }
        }
        public override bool OnScreen
        {
            get
            {
                return position.Y < SpaceAttackGame.Height;
            }
        }

        public Enemy(Vector2 position, float scale, IEnemyStrategy enemyStrategy)
            : base(position, scale, LoadHelper.Textures[TextureEnum.Enemy])
        {
            
            this.strategy = enemyStrategy;
        }

     
        public override void Update()
        {
            strategy.Update(ref position, ref shootTimeOut);
           
            
        }
    }
    interface IEnemyStrategy
    {
        void Update(ref Vector2 position, ref int shootTime);
    }
    class SimpleStrategy : IEnemyStrategy
    {
       

        public void Update(ref Vector2 position, ref int shootTime)
        {
            position.Y += 8;
            shootTime--;
            if (shootTime < 0)
                shootTime = RandomHelper.Next(200) + 50;



                    }
    }

    class AdvancedStrategy : IEnemyStrategy
    {
        bool firstShoot = true;
        int direct = -1;
        public void Update(ref Vector2 position, ref int shootTime)
        {
            
          //  position.X += (position.Y % 100 + direct * 80) / 8;
            position.X += direct * 10 ;
            if (position.X < 0 && direct < 0)
                direct = 1;
            if (position.X > SpaceAttackGame.Width)
                direct = -1;
            position.Y += 4;
            shootTime--;
            
            if (shootTime < 0 && firstShoot)
            {
                shootTime += 10;
                firstShoot = false;
            }
            if (shootTime < 0 && !firstShoot)
            {
                shootTime = RandomHelper.Next(200) + 50;
                firstShoot = true;
            }

        }

    }


    public enum EnemyStrategy
    {
        Simple,
        Advanced
    }
}
