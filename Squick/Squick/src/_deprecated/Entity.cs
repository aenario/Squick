using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Squick.deprecated
{
    public abstract class Entity :  Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected Player _player;
        // protected Sprite _sprite;
        

        public Entity(Game game, Player player) : base(game)
        {
            this._player = player;
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
