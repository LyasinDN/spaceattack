using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using spaceattack.Helpers;
using spaceattack.GameObjects;

namespace spaceattack
{
    class EditScreen
    {
        string newName="";
        Keys pressedKey=Keys.Add;
        private RecordList rList;

        public EditScreen(RecordList rl)
        { rList = rl; }
        public void Update()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Enter))
            {
                SpaceAttackGame.State = GameState.MainMenu;
                MainMenu.bEnterPressed = true;
                rList.addResult(rList.playerScore, newName, DateTime.Now.ToShortDateString());
                newName = "";
            }
            if (keyboard.IsKeyUp(pressedKey))
                pressedKey = Keys.Add;
            for (Keys k = Keys.A; k <= Keys.Z; k++) 
              if (keyboard.IsKeyDown(k) && newName.Length<10 && pressedKey==Keys.Add)
              {
                  pressedKey = k;
                  newName += k;
              }
            if (keyboard.IsKeyDown(Keys.Back) && pressedKey == Keys.Add && newName.Length >0)
            {
                pressedKey = Keys.Back;
                newName = newName.Substring(0, newName.Length - 1);
            }

        }
        public void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            spritebatch.DrawString(LoadHelper.Fonts[FontEnum.Menu],
            "You score: "+rList.playerScore.ToString(),
            new Vector2(50, 100), Color.DarkOrange);
            spritebatch.DrawString(LoadHelper.Fonts[FontEnum.Menu],
            "Enter your name:",
            new Vector2(50, 250), Color.DarkSeaGreen);
            spritebatch.DrawString(LoadHelper.Fonts[FontEnum.Test],
           newName,
           new Vector2(50, 400), Color.DarkOrange);


            spritebatch.End();
        }

    }
}
