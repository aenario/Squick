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
        public static Texture2D tex_squick_headCut;
        public static Texture2D tex_squick_tail;
        public static Texture2D tex_squick_leftLeg;
        public static Texture2D tex_squick_rightLeg;

        public static Texture2D tex_nut;
        public static Texture2D tex_goldenNut;
        public static Texture2D tex_pine;
        public static Texture2D tex_anvil;
        public static Texture2D tex_branch;
        public static Texture2D tex_bomb1;
        public static Texture2D tex_bomb2;
        public static Texture2D tex_bomb3;
        public static Texture2D tex_bomb4;
        public static Texture2D tex_fuse1;
        public static Texture2D tex_fuse2;
        public static Texture2D tex_fuse3;
        public static Texture2D tex_fuse4;
        public static Texture2D tex_fuse5;
        public static Texture2D tex_fuse6;
        public static Texture2D tex_fuse7;

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
            tex_squick_headCut = _content.Load<Texture2D>("Textures\\teteCut");
            tex_squick_tail = _content.Load<Texture2D>("Textures\\tail");
            tex_squick_leftLeg = _content.Load<Texture2D>("Textures\\leftLeg");
            tex_squick_rightLeg = _content.Load<Texture2D>("Textures\\rightLeg");

            // . Level 1
            tex_background_level1 = _content.Load<Texture2D>("Textures\\Background_level_1");
            tex_nut = _content.Load<Texture2D>("Textures\\nut");
            tex_goldenNut = _content.Load<Texture2D>("Textures\\goldenNut");
            tex_pine = _content.Load<Texture2D>("Textures\\pine");
            tex_anvil = _content.Load<Texture2D>("Textures\\anvil");
            tex_bomb1 = _content.Load<Texture2D>("Textures\\bomb1");
            tex_bomb2 = _content.Load<Texture2D>("Textures\\bomb2");
            tex_bomb3 = _content.Load<Texture2D>("Textures\\bomb3");
            tex_bomb4 = _content.Load<Texture2D>("Textures\\bomb4");
            tex_fuse1 = _content.Load<Texture2D>("Textures\\fuse1");
            tex_fuse2 = _content.Load<Texture2D>("Textures\\fuse2");
            tex_fuse3 = _content.Load<Texture2D>("Textures\\fuse3");
            tex_fuse4 = _content.Load<Texture2D>("Textures\\fuse4");
            tex_fuse5 = _content.Load<Texture2D>("Textures\\fuse5");
            tex_fuse6 = _content.Load<Texture2D>("Textures\\fuse6");
            tex_fuse7 = _content.Load<Texture2D>("Textures\\fuse7");

            // . Level 2
            tex_background_level2 = _content.Load<Texture2D>("Textures\\Background_level_2");
            tex_branch = _content.Load<Texture2D>("Textures\\branch");

            // . Misc
            tex_pixel = new Texture2D(gfx, 1, 1);
            tex_pixel.SetData(new Color[] { Color.White });
        }

    }
}
