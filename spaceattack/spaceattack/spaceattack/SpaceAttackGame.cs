using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;
using spaceattack.GameObjects;
using spaceattack.Helpers;
using System.IO;
namespace spaceattack
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class SpaceAttackGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static int Width;
        public static int Height;

        private GameManager manager;
        private RecordList rList;

        public static GameState State = GameState.MainMenu;
        private MainMenu menu;
        private EditScreen eScreen;

        public SpaceAttackGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Width = graphics.PreferredBackBufferWidth = 700;
            Height = graphics.PreferredBackBufferHeight = 800;
         
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadHelper.Load(Content); 
            rList = new RecordList("record.xml");
            manager = new GameManager(rList);
            menu = new MainMenu(manager);
          
            eScreen = new EditScreen(rList);
           
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            switch(State)
            {
                case GameState.MainMenu:
                   menu.Update();
                   break;
                case GameState.RecordList:
                   rList.Update();
                   break;  
                case GameState.Exit:
                     AudioManager.StopMainTheme();
                     this.Exit();
                     break;
                case GameState.Game:
          //           KeyboardInput();
          //           MouseInput();
                     manager.Update();
                     break;
                case GameState.EnterName:
                     eScreen.Update();
                     
                     break;
        }
            AudioManager.Update();
            base.Update(gameTime);
        }


  /*      private void MouseInput()
        {
            MouseState mouse = Mouse.GetState();

            int deltaX = mouse.X - Width / 2;
            int deltaY = mouse.Y - Height / 2;

            manager.MoveShip(deltaX / 3, deltaY / 3);

            Mouse.SetPosition(Width / 2, Height / 2);

            if (mouse.LeftButton == ButtonState.Pressed)
                manager.ShipShoot();
        }
        private void KeyboardInput()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Escape))
            {
                State = GameState.MainMenu;
                MainMenu.bEscPressed = true;
            }
 
            if (keyboard.IsKeyDown((Keys.Left)))
                manager.MoveShip(-7, 0);
            if (keyboard.IsKeyDown((Keys.Right)))
                manager.MoveShip(7, 0);
            if (keyboard.IsKeyDown((Keys.Up)))
                manager.MoveShip(0, -7);
            if (keyboard.IsKeyDown((Keys.Down)))
                manager.MoveShip(0, 7);
            if (keyboard.IsKeyDown((Keys.Space)))
                manager.ShipShoot();
        }
        */
        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Indigo);
            switch(State)
            { 
                case GameState.MainMenu:
                  menu.Draw(spriteBatch);
                break;
                case GameState.Game:
                  manager.Draw(spriteBatch);
                break;
                case GameState.RecordList:
                   rList.Draw(spriteBatch);
                break;
                case GameState.EnterName:
                eScreen.Draw(spriteBatch);
                break;
        }
            base.Draw(gameTime);
        }
    }
}
