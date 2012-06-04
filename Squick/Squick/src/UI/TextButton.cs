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
    public class TextButton
    {
        private String _buttonText;
        private Vector2 textSize;
        private Rectangle _boundingBox;

        private Color _textNormalColor;
        private Color _textSelectedColor;
        private Color _textCurrentColor;

        private SpriteFont _font;
        
        private bool _hover;
        private float _hoverTime;
        private bool _pressed;

        public const float BUTTON_SELECTION_TIME = 3.0f; // Time in seconds before a button press is performed.

        public TextButton(String text, Vector2 location)
            :this(text, new Rectangle((int) location.X,(int) location.Y, 100, 50)){}

        public TextButton(String text, Rectangle box)
        {
            // Init
            _buttonText = text;
            _hover = false;
            _pressed = false;

            // Default values

            _font = ResourceManager.font_UI;
            textSize = _font.MeasureString(text);
            _textNormalColor = Color.BurlyWood;
            _textSelectedColor = Color.Gold;
            _boundingBox = box;
        }

        public void Update(GameTime gameTime, KinectInterface gameInput)
        {
            // Cursors positions
            Rectangle[] cursors = gameInput.GetHandCursorsBoundingBoxes();
           
            // Look if user "cursor" is currently hover
            if (_boundingBox.Intersects(cursors[KinectInterface.LEFT_HAND]) || _boundingBox.Intersects(cursors[KinectInterface.RIGHT_HAND]))
            {
                _hover = true;
                _textCurrentColor = _textSelectedColor;
            }
            else
            {
                // Reset current status
                _hover = false;
                _hoverTime = 0.0f;
                _pressed = false;
                _textCurrentColor = _textNormalColor; 
            }
            // If currently hover, we measure the total time spent hover, and set the button as selected if the minimal "selection time" was reached
            if(_hover && !_pressed)
            {
                // Coompute total time
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
                _hoverTime += dt;

                // Selection time is reached !
                if (_hoverTime >= TextButton.BUTTON_SELECTION_TIME)
                {
                    _pressed = true;
                    Console.WriteLine("BUTTON " + _buttonText + " IS PRESSED");
                }
            }
        }

        public void Render(GameTime gameTime)
        {

            RenderManager.DrawBox(_boundingBox, _textCurrentColor);
            Rectangle inner = new Rectangle(_boundingBox.X + 5,
                _boundingBox.Y + 5,
                _boundingBox.Width - 10,
                _boundingBox.Height - 10);
            
            RenderManager.DrawBox(inner, Color.Beige);
            
            RenderManager.DrawString(_font, 
                _buttonText, 
                new Vector2(
                    _boundingBox.X + (_boundingBox.Width - textSize.X)/2,
                    _boundingBox.Y + (_boundingBox.Height - textSize.Y)/2),
                _textCurrentColor);
       }

        public bool IsPressed()
        {
            return _pressed;
        }
    }
}
