using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;

namespace Squick.Utility
{
    public static class ResourceManager
    {
        private static ContentManager _content;
       
        // Fonts
        public static SpriteFont font_UI;

        // Textures
        public static Texture2D tex_pixel; // Used to simulate a 1X1 pixel texture
        public static Texture2D tex_squick_head_normal;
        public static Texture2D tex_background_level1;
        //public static Texture2D tex_background_level2;
        //public static Texture2D tex_background_level3;

        public static void Initialize(ContentManager content,GraphicsDevice gfx)
        {
            _content = content;
            // Fonts
            font_UI = _content.Load<SpriteFont>("Fonts\\MainFont");

            // Textures
            // . Global 
            tex_squick_head_normal = _content.Load<Texture2D>("Textures\\Squick_head_normal");

            // . Level 1
            tex_background_level1 = _content.Load<Texture2D>("Textures\\Background_level_1");

            // . Misc
            tex_pixel = new Texture2D(gfx, 1, 1);
            tex_pixel.SetData(new Color[] { Color.Red });
        }

    }
}
