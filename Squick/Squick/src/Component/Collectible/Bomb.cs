using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Squick.Utility;


namespace Squick.Component.Collectible
{
    class Bomb : Collectible
    {
        public Bomb()
        {
            // Default properties
            _bonus = -500;
            _speedFactor = 0.5f;
            _movementPattern = MOVEMENT_FALL;
            _boundingBox = ResourceManager.tex_bomb.Bounds;
        }

        public override void Render(Microsoft.Xna.Framework.GameTime gameTime)
        {
            RenderManager.Draw2DTexture(ResourceManager.tex_pine, _boundingBox, Color.Red);
        }

        public override void Destroy(){}
    }
}
