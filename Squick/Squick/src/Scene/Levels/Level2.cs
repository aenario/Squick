using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Kinect;

using Squick.Control;
using Squick.Utility;
using Squick.src.Entity;

namespace Squick.Scene
{
    public class Level2 : Scene{

        private Texture2D _levelBackground;
        private jumpingSquick squick;
        private Vector2 leftHandPos;
        private Vector2 rightHandPos;

        //private Player _squick;

        public Level2(KinectInterface gameInput)
        {
            _levelBackground = ResourceManager.tex_background_level2;
            squick = new jumpingSquick(gameInput);
            squick.Pos = new Vector2(400, 100);
        }

        public override void Update(GameTime gameTime, KinectInterface gameInput)
        {

            DepthImagePoint[] dip = gameInput.GetLatestCoordinates();
            leftHandPos = new Vector2(dip[KinectInterface.LEFT_HAND].X, dip[KinectInterface.LEFT_HAND].Y);
            rightHandPos = new Vector2(dip[KinectInterface.RIGHT_HAND].X, dip[KinectInterface.RIGHT_HAND].Y);

            squick.Update(gameTime);
        }

        public override void Render(GameTime gameTime)
        {

            RenderManager.DrawLine(leftHandPos, rightHandPos);
            RenderManager.Draw2DTexture(_levelBackground, _levelBackground.Bounds, Color.White);
            RenderManager.DrawEntity(squick);
        }




    }
}
