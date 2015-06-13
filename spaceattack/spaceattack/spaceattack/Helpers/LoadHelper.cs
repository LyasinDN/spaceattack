using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace spaceattack.Helpers
{
    static class LoadHelper
    {
        public static Dictionary<TextureEnum, Texture2D> Textures;
        public static Dictionary<FontEnum, SpriteFont> Fonts;

        public static void Load(ContentManager content)
        {
            Textures = new Dictionary<TextureEnum, Texture2D>();
            Fonts = new Dictionary<FontEnum, SpriteFont>();

            Textures.Add(TextureEnum.Star, content.Load<Texture2D>(@"Textures\star"));
            Textures.Add(TextureEnum.Asteroid, content.Load<Texture2D>(@"Textures\asteroid"));
            Textures.Add(TextureEnum.Ship, content.Load<Texture2D>(@"Textures\ship"));
            Textures.Add(TextureEnum.Bullet, content.Load<Texture2D>(@"Textures\bullet"));
            Textures.Add(TextureEnum.Enemy, content.Load<Texture2D>(@"Textures\enemy"));
            Textures.Add(TextureEnum.playerLivesGraphic, content.Load<Texture2D>(@"Textures\life"));

            #region load fonts
            Fonts.Add(FontEnum.Arial22, content.Load<SpriteFont>(@"Fonts\Arial22"));
            Fonts.Add(FontEnum.Test, content.Load<SpriteFont>(@"Fonts\Test"));
            Fonts.Add(FontEnum.Menu, content.Load<SpriteFont>(@"Fonts\Menu"));
            Fonts.Add(FontEnum.RecordList, content.Load<SpriteFont>(@"Fonts\rList"));
            #endregion 
        }
    }

    public enum TextureEnum
    {
        Star,
        Asteroid,
        Ship,
        Bullet,
        Enemy,
        playerLivesGraphic
    }

    public enum FontEnum
    {
        Arial22,
        Test,
        Menu,
        RecordList
    }

}
