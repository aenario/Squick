using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Squick.Utility;
using Microsoft.Xna.Framework.Graphics;


namespace Squick.Component.Collectible
{
    class Bomb : Collectible
    {
        private const double _animationRate = 50;
        private double _animationTimer;
       
        private const int _animationMaxFrame = 4;
        private int _animationFrame;

        private int _spriteId;
        private const int _maxSpriteId = 4;
        private Texture2D _bombTex;
        private Texture2D _fuseTex;
        private Rectangle _fusePos;
        
        public Bomb()
        {
            // Default properties
            _bonus = -500;
            _speedFactor = 0.5f;
            _movementPattern = MOVEMENT_FALL;
            _animationTimer = 0;
            _animationFrame = 0;
            
            // Pick a bomb sprite at random
            _spriteId = (int)(new Random().Next(1, 5));
            switch (_spriteId)
            {
                case 1:
                    _bombTex = ResourceManager.tex_bomb1;
                    break;
                case 2:
                    _bombTex = ResourceManager.tex_bomb2;
                    break;
                case 3:
                    _bombTex = ResourceManager.tex_bomb3;
                    break;
                default:
                    _bombTex = ResourceManager.tex_bomb4;
                    break;
            }
            // Fuse default sprite
            _fuseTex = ResourceManager.tex_fuse1;
            // Bounding box
            _boundingBox = ResourceManager.tex_bomb1.Bounds;
        }

        public override void Render(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // Update animation frame
            if (gameTime.TotalGameTime.TotalMilliseconds - _animationTimer >= _animationRate)
            {
                // Animation keyframe
                if (_animationFrame < _animationMaxFrame)
                    _animationFrame++;
                else
                    _animationFrame = 0;
                // Animation texture
                switch (_animationFrame)
                {
                    case 1:
                        _fuseTex = ResourceManager.tex_fuse1;
                        break;
                    case 2:
                        _fuseTex = ResourceManager.tex_fuse2;
                        break;
                    case 3:
                        _fuseTex = ResourceManager.tex_fuse3;
                        break;
                    default:
                        _fuseTex = ResourceManager.tex_fuse4;
                        break;
                }
                // Update timer
                _animationTimer = gameTime.TotalGameTime.TotalMilliseconds;
            }

            // Blit sprites
            RenderManager.Draw2DTexture(this._bombTex, _boundingBox, Color.White);
            _fusePos = _boundingBox;
            _fusePos.Y -= 5;
            RenderManager.Draw2DTexture(this._fuseTex, _fusePos, Color.White);
        }

        public override void Destroy(){
            if(_collideWithPlayer)
                AudioManager.PlaySound(AudioManager.sound_boom);
        }
    }
}
