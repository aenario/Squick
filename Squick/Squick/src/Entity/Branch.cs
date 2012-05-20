using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Squick.Control;
using Squick.Utility;
using Microsoft.Kinect;

namespace Squick.src.Entity
{
    class Branch : Entity
    {
        private static Vector2 branchOrigin = new Vector2(33, 88);
        private static Vector2 branchEnd = new Vector2(472, 94);
        private static int branchLength = 436;

        private Texture2D _texture;
        private KinectInterface _gameInput;

        private bool _fromLeftTrunk = true;
        private bool _active;
        private float _coefDir;
        public float CoefDir { get { return _coefDir; } }

        private float _origin;
        public float Origin { get { return _origin; } }
        
        private float _scale;
        private float _angle;
        public float Angle { get { return _angle; } }
        
        private Vector2 _normal;
        public Vector2 Normal { get { return _normal; } }

        private Vector2 _pos2;
        private Vector2 left = new Vector2(0, 0) ;
        private Vector2 right = new Vector2(0, 0);

        public Branch(KinectInterface gameInput) : base()
        {
            _gameInput = gameInput;
            _active = true;
            _texture = ResourceManager.tex_branch;
        }

        public void Fix()
        {
            _active = false;
        }

        private bool isAboveOrBelow(Vector2 dot)
        {
            return _fromLeftTrunk ? dot.X < _pos2.X : dot.X > _pos2.X;
        }

        public bool isBelow(Vector2 dot)
        {
            return isAboveOrBelow(dot) && dot.Y < _origin + _coefDir * dot.X;
        }
        public bool isAbove(Vector2 dot)
        {
            return isAboveOrBelow(dot) && dot.Y > _origin + _coefDir * dot.X;
        }

        new public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!_active) return;


            DepthImagePoint[] dip = _gameInput.GetLatestCoordinates();
            left = new Vector2(dip[KinectInterface.LEFT_HAND].X, dip[KinectInterface.LEFT_HAND].Y);
            right = new Vector2(dip[KinectInterface.RIGHT_HAND].X, dip[KinectInterface.RIGHT_HAND].Y);

            _fromLeftTrunk = left.X + right.X < 800;

            // equation de la droite qui passe par les deux main y = ax + b
            _coefDir = (right.Y - left.Y) / (right.X - left.X); // a = (y2-y1)/(x2-x1)
            _origin = left.Y - _coefDir * left.X; // b = y1 - a* x1

            
            if (_fromLeftTrunk)
            {
                _pos.X = 0;
                _pos.Y = _origin; // y3 = a* 0 + b = b = y1 - a* x1
                _pos2 = right;
            }
            else
            {
                _pos.X = 800;
                _pos.Y = _coefDir * 800 + _origin;
                _pos2 = left;
            }

            _scale = Vector2.Subtract(_pos2, _pos).Length() / branchLength;
            _angle = (float) Math.Atan2( _pos2.Y - _pos.Y,_pos2.X - _pos.X);
            _normal = new Vector2(_pos.Y - _pos2.Y, _pos2.X - _pos.X);
            _normal.Normalize();
        }
        override public void Render(SpriteBatch spriteBatch){
            float alpha = _active ? 0.5f : 1f;
            spriteBatch.Draw(_texture, _pos, null, Color.White * alpha, _angle, branchOrigin, new Vector2(_scale, 1), SpriteEffects.None, 0);

            

            /*

            spriteBatch.Draw(ResourceManager.tex_pixel, new Rectangle((int) left.X, (int) left.Y, 10, 10), Color.Red);
            spriteBatch.Draw(ResourceManager.tex_pixel, new Rectangle((int) right.X, (int) right.Y, 10, 10), Color.Red);
             * */
        }
    }
}
