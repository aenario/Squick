using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Squick.Utility;
using Squick.Control;
using Microsoft.Kinect;

namespace Squick.Component.Player
{
    class JumpingSquick : Entity
    {
        private KinectInterface _gameInput;

        /* ANIMATION CONSTANTS */
        private static Vector2 tailOrigin = new Vector2(85, 130);
        private static Vector2 tailPos = new Vector2(100, 125);
        private static Vector2 leftArmOrigin = new Vector2(56, 21);
        private static Vector2 leftArmPos = new Vector2(90, 100);
        private static Vector2 rightArmOrigin = new Vector2(15, 21);
        private static Vector2 rightArmPos = new Vector2(125, 100);
        private static Vector2 leftLegOrigin = new Vector2(48, 6);
        private static Vector2 leftLegPos = new Vector2(90, 140);
        private static Vector2 rightLegOrigin = new Vector2(9, 7);
        private static Vector2 rightLegPos = new Vector2(125, 135);
        private static Vector2 headOrigin = new Vector2(56, 100);
        private static Vector2 headPos = new Vector2(108, 100);
        private static Vector2 eyeOrigin = new Vector2(10, 10);
        private static Vector2 leftEyePos = new Vector2(40, 68);
        private static Vector2 rightEyePos = new Vector2(75, 68);
        private static Vector2 bottom = new Vector2(100, 180);
        private static float eyesSpeed = MathHelper.Pi * 2 / 1000;

        private bool is_dizzy;
        public Boolean isDizzy{
            get { return is_dizzy;}
            set { is_dizzy = value;}
        }
        public void makeDizzy()
        {
            is_dizzy = true;
        }

        public Vector2 Bottom
        {
            get { return Vector2.Add(Pos, bottom); }
        }

        public int Width
        {
            get { return ResourceManager.tex_squick_body.Width; }
        }
        public int Height
        {
            get { return ResourceManager.tex_squick_body.Height; }
        }

        public JumpingSquick(KinectInterface gameInput)
            : base()
        {
            _gameInput = gameInput;
            _boundingBox = ResourceManager.tex_squick_body.Bounds;
        }

        new public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            // Adjust boundingBox
            if(_speed.Y < 0) is_dizzy = false;
            _boundingBox = new Rectangle((int)Pos.X + 80, (int)Pos.Y + 60, Width - 120, Height - 100);
        }
        override public void Render(GameTime gameTime)
        {

            Vector2 _leftArmPos = Vector2.Add(_pos, leftArmPos);
            Vector2 _rightArmPos = Vector2.Add(_pos, rightArmPos);
            Vector2 _leftLegPos = Vector2.Add(_pos, leftLegPos);
            Vector2 _rightLegPos = Vector2.Add(_pos, rightLegPos);
            Vector2 _tailPos = Vector2.Add(_pos, tailPos);
            Vector2 _headPos = Vector2.Add(_pos, headPos);
            Vector2 _leftEyePos = Vector2.Add(Vector2.Subtract(_headPos, headOrigin), leftEyePos);
            Vector2 _rightEyePos = Vector2.Add(Vector2.Subtract(_headPos, headOrigin), rightEyePos);

            float limbAngle = MathHelper.Clamp(_speed.Y/800f, -MathHelper.PiOver4, MathHelper.PiOver4);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_tail, _tailPos, Color.White,
                0, tailOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_rightArm, _rightArmPos, Color.White,
                -limbAngle, rightArmOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_body, _pos, Color.White);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_leftArm, _leftArmPos, Color.White,
                limbAngle, leftArmOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_leftLeg, _leftLegPos, Color.White,
                limbAngle, leftLegOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_rightLeg, _rightLegPos, Color.White,
                -limbAngle, rightLegOrigin);

            if (is_dizzy)
            {
                RenderManager.Draw2DTexture(ResourceManager.tex_squick_headNoEyes, _headPos, Color.White,
                    0, headOrigin);
                RenderManager.Draw2DTexture(ResourceManager.tex_squick_spirals, _leftEyePos, Color.White,
                    gameTime.ElapsedGameTime.Milliseconds * eyesSpeed, eyeOrigin);
                RenderManager.Draw2DTexture(ResourceManager.tex_squick_spirals, _rightEyePos, Color.White,
                    gameTime.ElapsedGameTime.Milliseconds * eyesSpeed, eyeOrigin);
            }
            else
            {
                RenderManager.Draw2DTexture(ResourceManager.tex_squick_headCut, _headPos, Color.White,
                    0, headOrigin);
            }
            
        }
    }
}
