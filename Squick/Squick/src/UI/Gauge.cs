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
    public class Gauge
    {
        private Vector2 _position;
        private float _max;
        private float _current;

        private Color _gaugeColor;

        private Rectangle _gauge;
        private Rectangle _fill;

        private SpriteFont _font;

        public Gauge(Vector2 pos, float max, Color color)
        {
            // Init
            _max = max;
            _gaugeColor = color;
            _position = pos;
            _current = 0;
            _gauge = new Rectangle(
        }

        public void Update(float current)
        {
            // Cursors positions
            _current = current;
            
        }

        public void Render(GameTime gameTime)
        {
            RenderManager.DrawBox(_boundingBox, _textColor);
            Rectangle inner = new Rectangle(_boundingBox.X + 5,
                _boundingBox.Y + 5,
                _boundingBox.Width - 10,
                _boundingBox.Height - 10);
            
            RenderManager.DrawBox(inner, Color.Beige);
            
            RenderManager.DrawString(_buttonFont, 
                _buttonText, 
                new Vector2(
                    _boundingBox.X + (_boundingBox.Width - textSize.X)/2,
                    _boundingBox.Y + (_boundingBox.Height - textSize.Y)/2),
                _textColor);
        }
    }
}
