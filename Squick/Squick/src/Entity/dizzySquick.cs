﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Squick.Utility;
using Squick.Control;
using Microsoft.Kinect;

namespace Squick.src.Entity
{
    class dizzySquick : Entity
    {
        /* PLUS COMPLEXE, gestion des bras */

        private Rectangle _boundingBox;
        private KinectInterface _gameInput;
        private float armAngle;
        private float maxAngle = MathHelper.Pi / 3; 
        private static float coefAcc = 7;

        private static Vector2 leftOrigin = new Vector2(56, 21) ;
        private static Vector2 rightOrigin = new Vector2(15, 21);
      
        public int Width
        {
            get { return _boundingBox.Width; }
        }
        public int Height
        {
            get { return _boundingBox.Height; }
        }

        public dizzySquick(KinectInterface gameInput)
            : base()
        {
            _gameInput = gameInput;
            _boundingBox = new Rectangle(0, 0, ResourceManager.tex_squick_tail.Width, ResourceManager.tex_squick_tail.Height);
        }

        new public void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            DepthImagePoint[] dip = _gameInput.GetLatestCoordinates();
            Vector2 left = new Vector2(dip[KinectInterface.LEFT_HAND].X, dip[KinectInterface.LEFT_HAND].Y);
            Vector2 right = new Vector2(dip[KinectInterface.RIGHT_HAND].X, dip[KinectInterface.RIGHT_HAND].Y);


            if(right.X != left.X) armAngle = (right.Y - left.Y) / (right.X - left.X);
            if (armAngle > 0) armAngle = Math.Min(armAngle, maxAngle);
            if (armAngle < 0) armAngle = Math.Max(armAngle, -maxAngle);

            _speed.X += armAngle * coefAcc;

            // frottement
            if (_speed.X > 0) _speed.X -= 2;
            if (_speed.X < 0) _speed.X += 2;

            /*_speed.X += ComputeSpeed(_gameInput); // NOTE SURE IF THIS SHOULD NOT BE IN LEVEL*/
            _boundingBox.X = (int) _pos.X;
            _boundingBox.Y = (int) _pos.Y;
            //Console.WriteLine("Taille = (" + Width + ";" + Height + ") ");
        }
        override public void Render(SpriteBatch spriteBatch){
            spriteBatch.Draw(ResourceManager.tex_squick_tail, _boundingBox, Color.White);

            Vector2 leftArmPos = new Vector2(_boundingBox.X + 90, _boundingBox.Y + 100);
            Vector2 rightArmPos = new Vector2(_boundingBox.X + 130, _boundingBox.Y + 100);
            

            spriteBatch.Draw(ResourceManager.tex_squick_rightArm, rightArmPos, null, Color.White, armAngle, rightOrigin, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(ResourceManager.tex_squick_body, _boundingBox, Color.White);
            spriteBatch.Draw(ResourceManager.tex_squick_leftArm, leftArmPos, null, Color.White, armAngle, leftOrigin, 1f, SpriteEffects.None, 0);
            
            spriteBatch.Draw(ResourceManager.tex_squick_leftLeg, _boundingBox, Color.White);
            spriteBatch.Draw(ResourceManager.tex_squick_rightLeg, _boundingBox, Color.White);
            
            spriteBatch.Draw(ResourceManager.tex_squick_head, _boundingBox, Color.White);
            
        }

        /* changer : contrôle de l'accélération plutot que de la vitesse */
        /*
        public int ComputeSpeed(KinectInterface gameInput)
        {
            int horizontalSpeed = 0;
            const int maxSpeed = 20;
            const int maxCoefficient = 10;

            // Computes director coefficient of the straight line drawn between each hands
            // . Raw coefficient
            float delta, absDelta;
            if (left.X == right.X)
                delta = 0;
            else
                delta = (right.Y - left.Y) / (right.X - left.X);

            armAngle = delta;

            absDelta = Math.Abs(delta);
            // . Coefficient sign (if monotonic straight line: +1, else -1)
            int sign;
            if (delta == 0)
                sign = 0;
            else
                sign = (int)(delta / absDelta);
            // . Computes final horizontal speed
            if (absDelta > maxCoefficient)
                horizontalSpeed = sign * maxSpeed;
            else
                horizontalSpeed = (int)(delta * maxSpeed);

            Console.WriteLine("Delta=" + delta + "|HorizontalSpeed=" + horizontalSpeed);

            return horizontalSpeed;
        }*/
    }
}
