using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using spaceattack.Helpers;
using Microsoft.Xna.Framework.Input;

namespace spaceattack.GameObjects
{
    class GameManager
    {
        public Ship ship;
        private List<Star> stars;
        private List<Asteroid> asteroids;
        private List<Bullet> bullets;
        private List<Enemy> enemies;
        public RecordList rList;
        private DateTime lastShootTime = DateTime.MinValue;
        int curLevel;
        int[] levelRanges ={300, 1200, 2300, 4500, 7000, 12500};

        public int playerScore;

        public GameManager(RecordList rl)
        {
            ship = new Ship(new Vector2(SpaceAttackGame.Width / 2, SpaceAttackGame.Height - 85), 0.5f);

            stars = new List<Star>();
            asteroids = new List<Asteroid>();
            bullets = new List<Bullet>();
            enemies = new List<Enemy>();
            rList = rl;
        }

        public void startGame()
        {
            playerScore = 0;
            curLevel = 1;
            this.ship.Lives = 5;
        }
        public void Update()
        {
            if (this.ship.Lives == 0)
                stopGame();
            UpdateStars();
            UpdateAsteroids();
            UpdateBullets();
            UpdateEnemies();
            KeyboardInput();
            MouseInput();
            manageLevel();
            //ship.Update();
        }
        void stopGame()
        {
            if (rList.checkInTop(playerScore))
            {
                SpaceAttackGame.State = GameState.EnterName;
                rList.playerScore = playerScore;
               
               
            }
            else
                SpaceAttackGame.State = GameState.MainMenu;
                
           
            stars.Clear();
            asteroids.Clear();
            bullets.Clear();
            enemies.Clear();

        }
        public void ShipShoot()
        {
            if ((DateTime.Now - lastShootTime).Milliseconds > 250)
            {
                AudioManager.PlayShootSound();
                bullets.Add(new Bullet(ship.Position, 0.05f, ShooterEnum.Player));
                lastShootTime = DateTime.Now;
            }
        }
        public void MoveShip(int dx, int dy)
        {
            ship.Move(dx, dy);
        }
        public void UpdateEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Update();
                if (enemy.ReadyToShoot)
                {
                    AudioManager.PlayShootSound();
                    bullets.Add(new Bullet(enemy.Position, 0.035f, ShooterEnum.Computer));
                }

                if (enemy.Rectangle.Intersects(ship.Rectangle))
                {
                    DestroyPlayerShip();
                    enemy.Alive = false;
                }
            }

            int i = 0;
            while (i < enemies.Count)
            {
                if (!enemies[i].OnScreen || !enemies[i].Alive)
                    enemies.RemoveAt(i);
                else i++;
            }

            int r = RandomHelper.Next(2000);
            if (r < 30)
            {
                // choose strat
                EnemyStrategy strategy = (RandomHelper.Next(2) == 0) ? EnemyStrategy.Simple : EnemyStrategy.Advanced;
                int x = RandomHelper.Next(SpaceAttackGame.Width);
                for (int j = 0; j < curLevel; j++)
                    enemies.Add(new Enemy(new Vector2(x, -50 * (j + 1)), 0.5f, RandomHelper.Next(2) == 0 ? (IEnemyStrategy)new SimpleStrategy() : (IEnemyStrategy)new AdvancedStrategy()));
            }
        }
        private void UpdateAsteroids()
        {
            foreach (Asteroid asteroid in asteroids)
            {
                asteroid.Update();
                #region collision
                if (asteroid.Rectangle.Intersects(ship.Rectangle))
                {
                    DestroyPlayerShip();
                    asteroid.Alive = false;
                }
                #endregion
            }

            int i = 0;
            while (i < asteroids.Count)
            {
                if (!asteroids[i].OnScreen || !asteroids[i].Alive)
                    asteroids.RemoveAt(i);
                else i++;
            }

            if (RandomHelper.Next(600/curLevel) < 10)
                asteroids.Add(new Asteroid(new Vector2(RandomHelper.Next(SpaceAttackGame.Width), -40), 0.7f));
        }
        private void DestroyPlayerShip()
        {
            ship.Place(SpaceAttackGame.Width / 2, SpaceAttackGame.Height - 85);
            ship.Lives = ship.Lives - 1;
        }
        private void UpdateStars()
        {
            foreach (Star star in stars)
                star.Update();

            int i = 0;
            while (i < stars.Count)
            {
                if (!stars[i].OnScreen)
                    stars.RemoveAt(i);
                else i++;
            }

            for (int j = 0; j < RandomHelper.Next(5); j++)
                stars.Add(new Star(new Vector2(RandomHelper.Next(SpaceAttackGame.Width), 0), 1 / (float)RandomHelper.Next(5, 11)));
        }

        private void UpdateBullets()
        {
            foreach (Bullet bullet in bullets)
            {
                bullet.Update();
                foreach (Asteroid asteroid in asteroids)
                    if (bullet.Owner == ShooterEnum.Player && asteroid.Rectangle.Intersects(bullet.Rectangle))
                    {
                        asteroid.Alive = false;
                        bullet.Alive = false;
                        playerScore += 10;
                    }
                foreach (Enemy enemy in enemies)
                    if (bullet.Owner == ShooterEnum.Player && enemy.Rectangle.Intersects(bullet.Rectangle))
                    {
                        enemy.Alive = false;
                        bullet.Alive = false;
                        playerScore += 50;
                    }
                if (bullet.Owner == ShooterEnum.Computer && bullet.Rectangle.Intersects(ship.Rectangle))
                {
                    DestroyPlayerShip();
                    bullet.Alive = false;
                }
            }
            int i = 0;
            while (i < bullets.Count)
            {
                if (!bullets[i].OnScreen || !bullets[i].Alive)
                    bullets.RemoveAt(i);
                else i++;
            }
        }

        private void MouseInput()
        {
            MouseState mouse = Mouse.GetState();

            int deltaX = mouse.X - SpaceAttackGame.Width / 2;
            int deltaY = mouse.Y - SpaceAttackGame.Height / 2;

            MoveShip(deltaX / 3, deltaY / 3);

            Mouse.SetPosition(SpaceAttackGame.Width / 2, SpaceAttackGame.Height / 2);

            if (mouse.LeftButton == ButtonState.Pressed)
               ShipShoot();
        }

        private void KeyboardInput()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
            {
                SpaceAttackGame.State = GameState.MainMenu;
                MainMenu.bEscPressed = true;
            }

            if (keyboard.IsKeyDown((Keys.Left)))
                MoveShip(-7, 0);
            if (keyboard.IsKeyDown((Keys.Right)))
               MoveShip(7, 0);
            if (keyboard.IsKeyDown((Keys.Up)))
                MoveShip(0, -7);
            if (keyboard.IsKeyDown((Keys.Down)))
                MoveShip(0, 7);
            if (keyboard.IsKeyDown((Keys.Space)))
                ShipShoot();
        }
        private void manageLevel()
        {
            if (curLevel<7 && levelRanges[curLevel - 1] < playerScore )
                curLevel++;

        }

        #region draw
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);

            #region stars
            foreach (Star star in stars)
            {
                star.Draw(spriteBatch);
            }
            #endregion

            #region asteroids
            foreach (Asteroid asteroid in asteroids)
            {
                asteroid.Draw(spriteBatch);
            }
            #endregion

            #region bullets
            foreach (Bullet bullet in bullets)
            {
                bullet.Draw(spriteBatch);
            }
            #endregion

            #region enemy
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            #endregion
            ship.Draw(spriteBatch);

            #region score
            spriteBatch.DrawString(LoadHelper.Fonts[FontEnum.Arial22],
                                    "Score:" + playerScore.ToString(),
                                   new Vector2(0, 0), Color.White);
            spriteBatch.DrawString(LoadHelper.Fonts[FontEnum.Arial22],
                                   "Level:" + curLevel.ToString(),
                                  new Vector2(0, 30), Color.White);
            for (int i = 0; i < ship.Lives; i++)
                spriteBatch.Draw(LoadHelper.Textures[TextureEnum.playerLivesGraphic], new Rectangle(40 * i + 10, 60, LoadHelper.Textures[TextureEnum.playerLivesGraphic].Width, LoadHelper.Textures[TextureEnum.playerLivesGraphic].Height), Color.White);

            #endregion
            spriteBatch.End();
        }
        #endregion
    }
}
