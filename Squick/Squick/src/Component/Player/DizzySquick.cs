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
    class DizzySquick : Entity
    {

        /* GAMLEPLAY CONSTANTS */
        private static float maxAngle = MathHelper.Pi / 3;
        private static float maxSpeed = 400;
        private static float coefAcc = 5;

        /* ANIMATION CONSTANTS */
        private static float coefTailAngle = 0.001f;
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
        private static Vector2 headPos = new Vector2(-10, 0);


        private KinectInterface _gameInput;
        private float armAngle;
        private Rectangle _bodyTexBox;
        public int Width { get { return _bodyTexBox.Width; } }
        public int Height { get { return _bodyTexBox.Height; } }

        public DizzySquick(KinectInterface gameInput)
            : base()
        {
            _gameInput = gameInput;
            _bodyTexBox = new Rectangle(0, 0, ResourceManager.tex_squick_body.Width, ResourceManager.tex_squick_body.Height);
        }

        new public void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Vector2[] hands = _gameInput.GetLatestCoordinates();
            Vector2 left = hands[KinectInterface.LEFT_HAND];
            Vector2 right = hands[KinectInterface.RIGHT_HAND];


            if(right.X != left.X) armAngle = (right.Y - left.Y) / (right.X - left.X);
            if (armAngle > 0) armAngle = Math.Min(armAngle, maxAngle);
            if (armAngle < 0) armAngle = Math.Max(armAngle, -maxAngle);

            _speed.X = MathHelper.Clamp(_speed.X + armAngle * coefAcc, -maxSpeed, maxSpeed);

            // add maxSpeed

            // frottement
            if (_speed.X > 0) _speed.X -= 2;
            if (_speed.X < 0) _speed.X += 2;

            
            _bodyTexBox.X = (int) _pos.X;
            _bodyTexBox.Y = (int) _pos.Y;

            // Adjust boundingBox
            _boundingBox = _bodyTexBox;
            _boundingBox.Inflate(-65, -65);
            _boundingBox.X += 10;
            _boundingBox.Y -= 20;
            _boundingBox.Height += 40;
            
        }



        override public void Render(GameTime gameTime)
        {

            Vector2 _leftArmPos = Vector2.Add(_pos, leftArmPos);
            Vector2 _rightArmPos = Vector2.Add(_pos, rightArmPos);
            Vector2 _leftLegPos = Vector2.Add(_pos, leftLegPos);
            Vector2 _rightLegPos = Vector2.Add(_pos, rightLegPos);
            Vector2 _tailPos = Vector2.Add(_pos, tailPos);
            Vector2 _headPos = Vector2.Add(_pos, headPos);
            float tailAngle = MathHelper.Clamp(-maxAngle / 2, -Speed.X * coefTailAngle, maxAngle / 2);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_tail, _tailPos, Color.White, 
                tailAngle, tailOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_leftLeg, _leftLegPos, Color.White,
                0, leftLegOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_rightLeg, _rightLegPos, Color.White,
                0, rightLegOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_rightArm, _rightArmPos, Color.White, 
                armAngle, rightArmOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_body, _bodyTexBox, Color.White);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_leftArm, _leftArmPos, Color.White, 
                armAngle, leftArmOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_leftLeg, _leftLegPos, Color.White, 
                0, leftLegOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_rightLeg, _rightLegPos, Color.White, 
                0, rightLegOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_head, _headPos, Color.White);

            // Debug 
            //RenderManager.DrawBox(_boundingBox);
        }

    }
}
