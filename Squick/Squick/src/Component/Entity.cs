using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Squick.Utility;

namespace Squick.Component
{
    public abstract class Entity
    {

        protected Vector2 _pos; /** pixels **/
        protected Rectangle _boundingBox;

        public Vector2 Pos
        {
            get{ return _pos;  }
            set{ _pos = value; }
        }
        protected Vector2 _speed; /** pixels /s **/
        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        
        public Entity()
        {
            _pos = new Vector2(0, 0);
            _speed = new Vector2(0, 0);
        }
        public Entity(Vector2 pos,Vector2 speed)
        {
            _pos = pos;
            _speed = speed;
        }

        public void Update(GameTime gameTime){
            var t = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (!_speed.Equals(Vector2.Zero)) _pos = Vector2.Add(_pos, (new Vector2(_speed.X * t, _speed.Y * t)));
        }
        public abstract void Render(GameTime gameTime);

        public Rectangle GetBoundingBox() { return _boundingBox; }
        
    }
}
