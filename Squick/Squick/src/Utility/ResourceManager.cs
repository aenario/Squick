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
        public static SpriteFont font_score;
        public static SpriteFont font_message;
        public static SpriteFont font_bigUI;

        // Textures
        public static Texture2D tex_pixel; // Used to simulate a 1X1 pixel texture
        public static Texture2D tex_squick_head_normal;
        public static Texture2D tex_background_level1;
        public static Texture2D tex_background_level2;

        public static Texture2D tex_squick_body;
        public static Texture2D tex_squick_leftArm;
        public static Texture2D tex_squick_rightArm;
        public static Texture2D tex_squick_head;
        public static Texture2D tex_squick_tail;
        public static Texture2D tex_squick_leftLeg;
        public static Texture2D tex_squick_rightLeg;

        public static Texture2D tex_nut;
        public static Texture2D tex_goldenNut;
        public static Texture2D tex_pine;
        public static Texture2D tex_branch;
        //public static Texture2D tex_background_level2;
        //public static Texture2D tex_background_level3;

        public static void Initialize(ContentManager content,GraphicsDevice gfx)
        {
            _content = content;
            // Fonts
            font_UI = _content.Load<SpriteFont>("Fonts\\MainFont");
            font_score = _content.Load<SpriteFont>("Fonts\\ScoreFont");
            font_message = _content.Load<SpriteFont>("Fonts\\MessageFont");
            font_bigUI = _content.Load<SpriteFont>("Fonts\\BigFont");


            // Textures
            // . Global 
            tex_squick_head_normal = _content.Load<Texture2D>("Textures\\teteCut");

            tex_squick_body = _content.Load<Texture2D>("Textures\\body");
            tex_squick_leftArm = _content.Load<Texture2D>("Textures\\leftArm");
            tex_squick_rightArm = _content.Load<Texture2D>("Textures\\rightArm");
            tex_squick_head = _content.Load<Texture2D>("Textures\\head");
            tex_squick_tail = _content.Load<Texture2D>("Textures\\tail");
            tex_squick_leftLeg = _content.Load<Texture2D>("Textures\\leftLeg");
            tex_squick_rightLeg = _content.Load<Texture2D>("Textures\\rightLeg");

            // . Level 1
            tex_background_level1 = _content.Load<Texture2D>("Textures\\Background_level_1");
            tex_nut = _content.Load<Texture2D>("Textures\\nut");
            tex_goldenNut = _content.Load<Texture2D>("Textures\\goldenNut");
            tex_pine = _content.Load<Texture2D>("Textures\\pine");
            
            // . Level 2
            tex_background_level2 = _content.Load<Texture2D>("Textures\\Background_level_2");
            tex_branch = _content.Load<Texture2D>("Textures\\branch");

            // . Misc
            tex_pixel = new Texture2D(gfx, 1, 1);
            tex_pixel.SetData(new Color[] { Color.White });
        }

    }
}
