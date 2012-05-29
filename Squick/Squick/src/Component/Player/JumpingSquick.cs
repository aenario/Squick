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

        private static Vector2 leftOrigin = new Vector2(56, 21) ;
        private static Vector2 rightOrigin = new Vector2(15, 21);
      
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

            _speed.Y = _speed.Y + 2; // gravity

        }
        override public void Render(GameTime gameTime)
        {
            RenderManager.Draw2DTexture(ResourceManager.tex_squick_tail, _pos, Color.White);

            Vector2 leftArmPos = new Vector2(_pos.X + 90, _pos.Y + 100);
            Vector2 rightArmPos = new Vector2(_pos.X + 130, _pos.Y + 100);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_rightArm, rightArmPos, Color.White);
            RenderManager.Draw2DTexture(ResourceManager.tex_squick_body, _pos, Color.White);
            RenderManager.Draw2DTexture(ResourceManager.tex_squick_leftArm, leftArmPos, Color.White);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_leftLeg, _pos, Color.White);
            RenderManager.Draw2DTexture(ResourceManager.tex_squick_rightLeg, _pos, Color.White);

            RenderManager.Draw2DTexture(ResourceManager.tex_squick_head, _pos, Color.White);
            
        }
    }
}
