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
        private static Vector2 bottom = new Vector2(100, 180);

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
            _boundingBox = new Rectangle((int)Pos.X + 80, (int)Pos.Y + 60, Width - 120, Height - 100);
        }
        override public void Render(GameTime gameTime)
        {
            /*
            RenderManager.Draw2DTexture(ResourceManager.tex_squick_tail, _pos, Color.White);

            Vector2 leftArmPos = new Vector2(_pos.X + 90, _pos.Y + 100);
            Vector2 rightArmPos = new Vector2(_pos.X + 130, _pos.Y + 100);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_rightArm, rightArmPos, Color.White);
            RenderManager.Draw2DTexture(ResourceManager.tex_squick_body, _pos, Color.White);
            RenderManager.Draw2DTexture(ResourceManager.tex_squick_leftArm, leftArmPos, Color.White);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_leftLeg, _pos, Color.White);
            RenderManager.Draw2DTexture(ResourceManager.tex_squick_rightLeg, _pos, Color.White);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_head, _pos, Color.White);
            */

            Vector2 _leftArmPos = Vector2.Add(_pos, leftArmPos);
            Vector2 _rightArmPos = Vector2.Add(_pos, rightArmPos);
            Vector2 _leftLegPos = Vector2.Add(_pos, leftLegPos);
            Vector2 _rightLegPos = Vector2.Add(_pos, rightLegPos);
            Vector2 _tailPos = Vector2.Add(_pos, tailPos);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_tail, _tailPos, Color.White,
                0, tailOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_leftLeg, _leftLegPos, Color.White,
                0, leftLegOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_rightLeg, _rightLegPos, Color.White,
                0, rightLegOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_rightArm, _rightArmPos, Color.White,
                0, rightArmOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_body, _pos, Color.White);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_leftArm, _leftArmPos, Color.White,
                0, leftArmOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_leftLeg, _leftLegPos, Color.White,
                0, leftLegOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_rightLeg, _rightLegPos, Color.White,
                0, rightLegOrigin);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_head, _pos, Color.White);
            
        }
    }
}
