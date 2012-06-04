using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Squick.Utility;
using Squick.Component.Player;

namespace Squick.Component.Collectible
{
    abstract class Collectible : Entity
    {
        public const int MOVEMENT_NONE = 0;
        public const int MOVEMENT_FALL = 1;
        
        protected int _bonus;
        protected float _speedFactor;
        protected int _movementPattern;
        public int MovementPattern
        {
            get{ return _movementPattern;}
            set { _movementPattern = value; }
        }
        protected bool _destroyed;

        public Collectible()
        {
            _destroyed = false;
        }

        new public void Update(GameTime gameTime)
        {
<<<<<<< HEAD
            if (_movementPattern == MOVEMENT_NONE)
                return;
            
            //var t = gameTime.ElapsedGameTime.Milliseconds / 1000f;

            if (_movementPattern == MOVEMENT_FALL)
                _pos.Y += _speedFactor * _speed.Y;
=======

            if (_movementPattern == MOVEMENT_FALL)
            {
                var t = gameTime.ElapsedGameTime.Milliseconds / 1000f;
                _pos.Y += t * _speedFactor * _speed.Y;
            }

>>>>>>> a33f3a9a62c94f64d6a8b1f48d8479c96dccd7cd
            _boundingBox.X = (int)_pos.X;
            _boundingBox.Y = (int)_pos.Y; 
        }

        public int GetBonus() { return _bonus; }

        public abstract void Destroy();

    }
}
