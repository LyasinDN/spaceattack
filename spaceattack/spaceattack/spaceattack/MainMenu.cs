using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using spaceattack.Helpers;
using spaceattack.GameObjects;
using System.IO;

namespace spaceattack
{
    class MainMenu
    {
       
        private int selected = 0;
        GameManager mgr;
        static public bool bEscPressed = false;
        static public bool bEnterPressed = false;
        private bool arrowPressed = true;
        public MainMenu(GameManager m)
        {
            mgr=m;
        }

        public void Update()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown((Keys.Up)) && !arrowPressed)
                prev();
            if (keyboard.IsKeyDown((Keys.Down)) && !arrowPressed)
                next();

            if (keyboard.IsKeyDown((Keys.Enter)) && !bEnterPressed)
              switch (selected)
                {
                    case 0:
                      SpaceAttackGame.State = GameState.Game;
                      mgr.startGame();
                      AudioManager.StartMainTheme();
                    break;
                    case 1:
                         SpaceAttackGame.State = GameState.RecordList;
                    break;
                    case 2:
                         SpaceAttackGame.State = GameState.Exit;
                    break;
                }
            if (keyboard.IsKeyUp(Keys.Escape) && bEscPressed)
                bEscPressed = false;
            if (keyboard.IsKeyUp(Keys.Enter) && bEnterPressed)
                bEnterPressed = false;
            if (keyboard.IsKeyDown(Keys.Escape) && !bEscPressed)
                SpaceAttackGame.State = GameState.Exit;
            if (keyboard.IsKeyUp((Keys.Up)) && keyboard.IsKeyUp((Keys.Down)))
                arrowPressed = false;
            else
                arrowPressed = true;
        }
        public void next()
        {
            selected++;
            if (selected > 2)
                selected = 0;
        }

        public void prev()
        {
            selected--;
            if (selected == -1)
                selected = 2;
        }

        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spritebatch.DrawString(LoadHelper.Fonts[FontEnum.Test],
            "space attack",
            new Vector2(50, 150), Color.DarkOrange);
            spritebatch.DrawString(LoadHelper.Fonts[FontEnum.Menu],
            "Tunnels of the Milky Way",
            new Vector2(50, 250), Color.DarkOrange);
            spritebatch.DrawString(LoadHelper.Fonts[FontEnum.Menu],
               "Start game",
            new Vector2(215, 500), selected == 0 ? Color.Gold : Color.DarkSeaGreen);
                spritebatch.DrawString(LoadHelper.Fonts[FontEnum.Menu],
                "Hall of fame",
            new Vector2(200, 560), selected == 1 ? Color.Gold : Color.DarkSeaGreen);
                spritebatch.DrawString(LoadHelper.Fonts[FontEnum.Menu],
                "Exit",
            new Vector2(290, 620), selected == 2 ? Color.Gold : Color.DarkSeaGreen);
           
            spritebatch.End();
        }
    }
}
