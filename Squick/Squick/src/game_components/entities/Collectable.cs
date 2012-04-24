using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Squick
{
    public class Collectable :  Entity
    {

        protected int _bonusValue;

        public Collectable(Game game,Player player) : base(game,player)
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
