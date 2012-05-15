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

namespace Squick.Scene
{
    public class Level1 : Scene{

        private Texture2D _levelBackground;
        private Texture2D _squickHead; // ENCAPSULATE THAT IN A CLASS (Player)
        private Rectangle _squickBoundingBox;  // ENCAPSULATE THAT IN A CLASS (Player)

        //private Player _squick;

        public Level1()
        {
            _levelBackground = ResourceManager.tex_background_level1;
            _squickHead = ResourceManager.tex_squick_head_normal;
            _squickBoundingBox = new Rectangle(400, 400, _squickHead.Width, _squickHead.Height);
        }

        public override void Update(GameTime gameTime, KinectInterface gameInput)
        {
            int dx = (int)(ComputeHeroSpeed(gameInput) * gameTime.ElapsedGameTime.TotalMilliseconds/10);
            if (_squickBoundingBox.X + dx > -_squickBoundingBox.Width/2 && _squickBoundingBox.X + dx < 800 - _squickBoundingBox.Width/2)
                _squickBoundingBox.X += dx;
        }

        public override void Render(GameTime gameTime)
        {
            RenderManager.Draw2DTexture(_levelBackground, _levelBackground.Bounds, Color.White);
            RenderManager.Draw2DTexture(_squickHead, _squickBoundingBox, Color.White);
        }


        /**
         * Gameplay (TODO ENCAPSULATE IN A CLASS (Player?)
         **/
        public int ComputeHeroSpeed(KinectInterface gameInput)
        {
            int horizontalSpeed = 0;
            const int maxSpeed = 5;
            const int maxCoefficient = 10;

            DepthImagePoint[] dip = gameInput.GetLatestCoordinates();
            Vector2 left = new Vector2(dip[KinectInterface.LEFT_HAND].X,dip[KinectInterface.LEFT_HAND].Y);
            Vector2 right = new Vector2(dip[KinectInterface.RIGHT_HAND].X, dip[KinectInterface.RIGHT_HAND].Y);

            // Computes director coefficient of the straight line drawn between each hands
            // . Raw coefficient
            float delta,absDelta;
            if (left.X == right.X)
                delta = 0;
            else
                delta = (right.Y - left.Y) / (right.X - left.X);
            
            absDelta = Math.Abs(delta);
            // . Coefficient sign (if monotonic straight line: +1, else -1)
            int sign;
            if(delta == 0)
                sign = 0;
            else 
                sign = (int)(delta/absDelta);
            // . Computes final horizontal speed
            if (absDelta > maxCoefficient)
                horizontalSpeed = sign * maxSpeed;
            else
                horizontalSpeed = (int)(delta * maxSpeed);

            Console.WriteLine("Delta=" + delta + "|HorizontalSpeed=" + horizontalSpeed);

            return horizontalSpeed;
        }

    }
}
