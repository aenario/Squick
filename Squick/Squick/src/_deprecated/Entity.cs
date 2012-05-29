using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Squick.deprecated
{
    public abstract class Entity :  Microsoft.Xna.Framework.DrawableGameComponent
    {
        // protected Sprite _sprite;

        public Entity(Game game) : base(game)
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

        public abstract bool CollideWithPlayer();
    }
}
