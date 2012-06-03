using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Squick.Utility;


namespace Squick.Component.Collectible
{
    class Nut : Collectible
    {
        public Nut()
        {
            // Default properties
            _bonus = 100;
            _speedFactor = 1.0f;
            _movementPattern = MOVEMENT_FALL;
            _boundingBox = ResourceManager.tex_nut.Bounds;
        }

        public override void Render(Microsoft.Xna.Framework.GameTime gameTime)
        {
            RenderManager.Draw2DTexture(ResourceManager.tex_nut, _boundingBox, Color.White);
        }

        public override void Destroy()
        {
            if (_destroyed)
                return; 
            _destroyed = true;

            

        }
    }
}
