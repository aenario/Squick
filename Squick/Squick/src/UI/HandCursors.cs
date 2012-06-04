using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Kinect;

using Squick.Control;
using Squick.Utility;

namespace Squick.UI
{
    public class HandCursors
    {
        private Vector2 _left;
        private static Vector2 _leftO = new Vector2(20, 20);
        private Vector2 _right;
        private static Vector2 _rightO = new Vector2(40, 20);
        
        
        public HandCursors()
        {
            // Init
            _left = Vector2.Zero;
            _right = Vector2.Zero;
        }

        public void Update(GameTime gameTime, KinectInterface gameInput)
        {
            Vector2[] pos = gameInput.GetLatestCoordinates();
            _left = pos[KinectInterface.LEFT_HAND];
            _right = pos[KinectInterface.RIGHT_HAND];
        }

        public void Render(GameTime gameTime)
        {
            RenderManager.Draw2DTexture(ResourceManager.tex_squick_leftArm, _left, Color.White, MathHelper.PiOver4, _leftO);
            RenderManager.Draw2DTexture(ResourceManager.tex_squick_rightArm, _right, Color.White, -MathHelper.PiOver4, _rightO);
        }
    }
}
