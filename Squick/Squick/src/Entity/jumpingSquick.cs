using System;
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
    class jumpingSquick : Entity
    {
        /* PLUS COMPLEXE, gestion des bras */

        private Rectangle _boundingBox;
        private KinectInterface _gameInput;

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

        public jumpingSquick(KinectInterface gameInput)
            : base()
        {
            _gameInput = gameInput;
            _boundingBox = new Rectangle(0, 0, ResourceManager.tex_squick_tail.Width, ResourceManager.tex_squick_tail.Height);
        }

        new public void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            _speed.Y -= 3;

            _boundingBox.X = (int) _pos.X;
            _boundingBox.Y = (int) _pos.Y;
            //Console.WriteLine("Taille = (" + Width + ";" + Height + ") ");
        }
        override public void Render(SpriteBatch spriteBatch){
            spriteBatch.Draw(ResourceManager.tex_squick_tail, _boundingBox, Color.White);

            Vector2 leftArmPos = new Vector2(_boundingBox.X + 90, _boundingBox.Y + 100);
            Vector2 rightArmPos = new Vector2(_boundingBox.X + 130, _boundingBox.Y + 100);
            
            spriteBatch.Draw(ResourceManager.tex_squick_rightArm, rightArmPos, Color.White);
            spriteBatch.Draw(ResourceManager.tex_squick_body, _boundingBox, Color.White);
            spriteBatch.Draw(ResourceManager.tex_squick_leftArm, rightArmPos, Color.White);
            
            spriteBatch.Draw(ResourceManager.tex_squick_leftLeg, _boundingBox, Color.White);
            spriteBatch.Draw(ResourceManager.tex_squick_rightLeg, _boundingBox, Color.White);
            
            spriteBatch.Draw(ResourceManager.tex_squick_head, _boundingBox, Color.White);
            
        }

        
    }
}
