using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Kinect;

using Squick.Control;
using Squick.Utility;

namespace Squick.UI
{
    public class Score
    {
        private int _value;
        private int _bonusChain;
        private Vector2 _location;
        private Vector2 _locationBonus;
        private Color _textCurrentColor;

        // Default values
        private Color _textNormalColor = Color.White;
        private Color _textScoreDecreased = Color.Red;
        private Color _textScoreIncreased = Color.Gold;
        private SpriteFont _font = ResourceManager.font_score;

        public Score(Vector2 location)
        {
            // Init
            _value = 0;
            _bonusChain = 0;
            _location = location;
            _locationBonus = location;
            _locationBonus.Y += 70;
        
            // Default values
            _textCurrentColor = _textNormalColor;
        }

        public void Update(int score,int bonusChain = 0)
        {
            // Check if the score need to be redrawn
            if (_value != score)
            {
                // Increasing
                if (_value < score)
                {
                    _textCurrentColor = _textScoreIncreased;
                    _value += 5;//Math.Max(4, (score - _value) / 20);
                }
                // Decreasing                   
                else
                {
                    _textCurrentColor = _textScoreDecreased;
                    _value -= 5;// Math.Min(-4, (score - _value) / 20);
                }
            }
            // EQUAL
            else
            {
                _textCurrentColor = _textNormalColor;
            }

            _bonusChain = bonusChain;
           
        }

        public void Render(GameTime gameTime)
        {
            RenderManager.DrawString(_font, _value.ToString(), _location, _textCurrentColor);
            RenderManager.DrawString(_font, "x"+_bonusChain.ToString(), _locationBonus, _textNormalColor);
        }

       
    }
}
