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
        private const Color _textNormalColor = Color.White;
        private const Color _textScoreDecreased = Color.Red;
        private const Color _textScoreIncreased = Color.Gold;
        private const SpriteFont _buttonFont = ResourceManager.font_UI;

        public Score(Vector2 location)
        {
            // Init
            _value = 0;
            _location = location;
        

            // Default values
            _buttonFont = ResourceManager.font_UI;
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
                    _value++;

                }
                // Decreasing                   
                else if (_value > score)
                {
                    _textCurrentColor = _textScoreDecreased;
                    _value--;
                }
                // EQUAL
                else
                {
                    _textCurrentColor = _textNormalColor;
                }
            }
        }

        public void Render(GameTime gameTime)
        {
            RenderManager.DrawBox(_boundingBox);
            RenderManager.DrawString(_buttonFont, _buttonText, _location, _textCurrentColor);
        }

       
    }
}
