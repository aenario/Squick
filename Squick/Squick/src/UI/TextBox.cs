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
    public class TextBox
    {
        private String _buttonText;
        private Vector2 textSize;
        private Rectangle _boundingBox;

        private Color _textColor;

        private SpriteFont _buttonFont;

        public const float BUTTON_SELECTION_TIME = 3.0f; // Time in seconds before a button press is performed.

        public TextBox(String text, Vector2 location)
            :this(text, new Rectangle((int) location.X,(int) location.Y, 100, 50)){}

        public TextBox(String text, Rectangle box)
        {
            // Init
            _buttonText = text;

            // Default values
            _buttonFont = ResourceManager.font_UI;
            textSize = _buttonFont.MeasureString(text);
            _textColor = Color.BurlyWood;
            _boundingBox = box;
        }

        public void Update(GameTime gameTime, KinectInterface gameInput)
        {
            // Cursors positions
            
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
