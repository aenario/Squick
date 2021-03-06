﻿using System;
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
        protected Boolean _collideWithPlayer;
        public int MovementPattern
        {
            get{ return _movementPattern;}
            set { _movementPattern = value; }
        }
        protected bool _destroyed;

        public Collectible()
        {
            _destroyed = false;
            _collideWithPlayer = false;
        }

        override public void Update(GameTime gameTime)
        {
            _boundingBox.X = (int)_pos.X;
            _boundingBox.Y = (int)_pos.Y; 

            if (_movementPattern == MOVEMENT_NONE)
                return;
            
            var t = gameTime.ElapsedGameTime.Milliseconds / 1000f;
            if (_movementPattern == MOVEMENT_FALL)
                _pos.Y += t * _speedFactor * _speed.Y;

        }

        public int GetBonus() { return _bonus; }

        public void CollideWithPlayer(Boolean state) { _collideWithPlayer = state; }

        public abstract void Destroy();

    }
}
