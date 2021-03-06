﻿using System;
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
            _bonus = 500;
            _speedFactor = 1.5f;
            _movementPattern = MOVEMENT_FALL;
            _boundingBox = ResourceManager.tex_goldenNut.Bounds;
        }

        public override void Render(Microsoft.Xna.Framework.GameTime gameTime)
        {
            RenderManager.Draw2DTexture(ResourceManager.tex_goldenNut, _boundingBox, Color.Gold);
        }

        public override void Destroy() {
            if (_collideWithPlayer)
                AudioManager.PlaySound(AudioManager.sound_bonus);
        }
    }
}
