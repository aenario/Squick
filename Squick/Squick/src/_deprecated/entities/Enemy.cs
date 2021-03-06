﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Squick.deprecated
{
    public class Enemy :  Entity
    {

        protected int _damageValue;
        protected bool _criticalHit;

        public Enemy(Game game) : base(game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        public override bool CollideWithPlayer()
        {
            return false;
        }
    }
}
