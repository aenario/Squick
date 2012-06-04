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
        private Vector2 _location;
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
            _location = location;
        
            // Default values
            _textCurrentColor = _textNormalColor;
        }

        public void Update(int score)
        {
            // Check if the score need to be redrawn
            if (_value != score)
            {
                // Increasing
                if (_value < score)
                {
                    _textCurrentColor = _textScoreIncreased;
                    _value+=2;

                }
                // Decreasing                   
                else
                {
                    _textCurrentColor = _textScoreDecreased;
                    _value-=2;
                }
        
            }
            // EQUAL
            else
            {
                _textCurrentColor = _textNormalColor;
            }
        }

        public void Render(GameTime gameTime)
        {
            RenderManager.DrawString(_font, _value.ToString(), _location, _textCurrentColor);
        }

       
    }
}
