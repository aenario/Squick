using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Squick.Utility;


namespace Squick.Component.Collectible
{
    class GoldenNut : Collectible
    {
        public GoldenNut()
        {
            // Default properties
            _bonus = 1;
            _speedFactor = 1;
            _movementPattern = MOVEMENT_FALL;
            _boundingBox = ResourceManager.tex_nut.Bounds;
        }

        public override void Render(Microsoft.Xna.Framework.GameTime gameTime)
        {
            RenderManager.Draw2DTexture(ResourceManager.tex_nut, _boundingBox, Color.Gold);
        }
    }
}
