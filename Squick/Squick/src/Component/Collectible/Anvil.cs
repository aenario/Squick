using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Squick.Utility;


namespace Squick.Component.Collectible
{
    class Anvil : Collectible
    {
        private double _speedTimer;
        
        public Anvil()
        {
            // Default properties
            _bonus = -1000;
            _speedFactor = 1.0f;
            _movementPattern = MOVEMENT_FALL;
            _speedTimer = 0;
            _boundingBox = ResourceManager.tex_anvil.Bounds;

            // Play a sound at startup
            AudioManager.PlaySound(AudioManager.sound_fall);
        }

        override public void Update(GameTime gameTime)
        {
            // Increase speed every 0.5 seconds
            if (gameTime.TotalGameTime.TotalMilliseconds - _speedTimer >= 50)
            {
                _speed.Y += 1;
                _speedTimer = gameTime.TotalGameTime.TotalMilliseconds;
            }
            base.Update(gameTime);
        }

        public override void Render(Microsoft.Xna.Framework.GameTime gameTime)
        {
            RenderManager.Draw2DTexture(ResourceManager.tex_anvil, _boundingBox, Color.White);
        }

        public override void Destroy() {
            if (_collideWithPlayer)
                AudioManager.PlaySound(AudioManager.sound_hurt);
        }
    }
}
