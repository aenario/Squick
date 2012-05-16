using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Squick.Utility;
using Microsoft.Xna.Framework.Graphics;

namespace Squick.src.Entity
{
    class SimpleEntity : Entity
    {

        private Texture2D _texture; 
        private Rectangle _boundingBox;
        public int Width
        {
            get { return _boundingBox.Width; }
        }
        public int Height
        {
            get { return _boundingBox.Height; }
        }

        public SimpleEntity(Texture2D tex) : base()
        {
            _texture = tex;
            _boundingBox = new Rectangle(0, 0, _texture.Width, _texture.Height);
        }

        new public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            _boundingBox.X = (int) _pos.X;
            _boundingBox.Y = (int) _pos.Y;
            //Console.WriteLine("Taille = (" + Width + ";" + Height + ") ");
        }
        override public void Render(SpriteBatch spriteBatch){
            spriteBatch.Draw(_texture, _boundingBox, Color.White);  
        }
    }
}
