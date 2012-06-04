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
        private Color _fillColor;

        private Rectangle _gauge;
        private Rectangle _fill;

        public Vector2 Top
        {
            get{ return new Vector2(_fill.X + _fill.Width/2, _fill.Y); }
        }

        public Gauge(Rectangle pos, float max, Color color, Color fillColor)
        {
            // Init
            _max = max;
            _gaugeColor = color;
            _fillColor = fillColor;
            _position = new Vector2(pos.X, pos.Y);
            _current = 0;
            _gauge = pos;
            _fill = new Rectangle(pos.X, pos.Y, pos.Width, pos.Height);
            _fill.Inflate(-2, 0);
        }

        public void Update(float current)
        {
            // Cursors positions
            _current = current;
            _fill.Y = (int) (_gauge.Y + _gauge.Height * (1 - (_current / _max)));
            _fill.Height = (int) ((_current / _max) * _gauge.Height);
        }

        public void Render(GameTime gameTime)
        {
            RenderManager.DrawBox(_gauge, _gaugeColor);
            RenderManager.DrawBox(_fill, _fillColor);
            
        }
    }
}
