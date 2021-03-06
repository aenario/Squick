﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Squick.Utility;


namespace Squick.Component.Collectible
{
    class Pine : Collectible
    {
        public Pine()
        {
            // Default properties
            _bonus = -100;
            _speedFactor = 1.0f;
            _movementPattern = MOVEMENT_FALL;
            _boundingBox = ResourceManager.tex_pine.Bounds;
        }

        public override void Render(Microsoft.Xna.Framework.GameTime gameTime)
        {
            RenderManager.Draw2DTexture(ResourceManager.tex_pine, _boundingBox, Color.White);
        }

        public override void Destroy() {
            if (_collideWithPlayer)
                AudioManager.PlaySound(AudioManager.sound_hurt);
        }
    }
}
