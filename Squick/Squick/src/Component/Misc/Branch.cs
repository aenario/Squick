using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Squick.Control;
using Squick.Utility;
using Microsoft.Kinect;

namespace Squick.Component.Misc
{
    class Branch : Entity
    {
        private static Vector2 branchOrigin = new Vector2(33, 88);
        private static Vector2 branchEnd = new Vector2(472, 94);
        private static int branchLength = 436;

        private Texture2D _texture;
        private KinectInterface _gameInput;

        private bool _fromLeftTrunk = true;
        private bool _active = true;
        private bool firstHit = true;
        public bool Destroyable { get { return !firstHit; } }
        private float _coefDir;
        public float CoefDir { get { return _coefDir; } }

        private float _origin;
        public float Origin { get { return _origin; } }
        
        private float _scale;
        private float _angle;
        public float Angle { get { return _angle; } }
        
        private Vector2 _normal;
        public Vector2 Normal { get { return _normal; } }

        private Vector2 _posM;
        private Vector2 _pos2;
        public float BounceLength { get { return Vector2.Subtract(_posM, _pos2).Length(); } }
        private Vector2 left = new Vector2(0, 0) ;
        private Vector2 right = new Vector2(0, 0);

        public Branch(KinectInterface gameInput) : base()
        {
            _gameInput = gameInput;
            _texture = ResourceManager.tex_branch;
        }

        public bool HitAndBreak()
        {
            if(_active) return _active = false;
            if (firstHit) return firstHit = false;
            return true;
        }

        private bool isAboveOrBelow(Vector2 dot)
        {
            if (_active)
            {
                return _fromLeftTrunk ?
                    _posM.X < dot.X && dot.X < _pos2.X :
                    _posM.X > dot.X && dot.X > _pos2.X;
            }
            else
            {
                return _fromLeftTrunk ?
                    dot.X < _pos2.X :
                    dot.X > _pos2.X;
            }
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
            if (!_active)
            {
                if(_fromLeftTrunk) _origin = _pos.Y;
                else _origin = _pos.Y - _coefDir * 800;
                return;
            }


            Vector2[] hands = _gameInput.GetLatestCoordinates();
            Vector2 left = hands[KinectInterface.LEFT_HAND];
            Vector2 right = hands[KinectInterface.RIGHT_HAND];


            // cancel hands inversion
            if (left.X > right.X)
            {
                var temp = right;
                right = left;
                left = temp;
            }

            _fromLeftTrunk = left.X + right.X < 800;

            // equation de la droite qui passe par les deux main y = ax + b
            _coefDir = (right.Y - left.Y) / (right.X - left.X); // a = (y2-y1)/(x2-x1)
            _origin = left.Y - _coefDir * left.X; // b = y1 - a* x1

            
            if (_fromLeftTrunk)
            {
                _pos.X = 0;
                _pos.Y = _origin; // y3 = a* 0 + b = b = y1 - a* x1
                _pos2 = right;
                _posM = left;
            }
            else
            {
                _pos.X = 800;
                _pos.Y = _coefDir * 800 + _origin;
                _pos2 = left;
                _posM = right;
            }

            _scale = Vector2.Subtract(_pos2, _pos).Length() / branchLength;
            _angle = (float) Math.Atan2( _pos2.Y - _pos.Y,_pos2.X - _pos.X);
            _normal = new Vector2(_pos.Y - _pos2.Y, _pos2.X - _pos.X);
            _normal.Normalize();
        }
        
        public override void Render(GameTime gameTime){
            float alpha = _active ? 0f : 1f;
            if (_active) RenderManager.DrawLine(_pos2, _posM, Color.Gold);
            RenderManager.Draw2DTexture(_texture, _pos, Color.White * alpha, _angle, branchOrigin, new Vector2(_scale, 1));
            
        }
    }
}
