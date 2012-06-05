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
    public class Message
    {
        public const int DISPLAY_NORMAL = 0;
        public const int DISPLAY_LBL = 1;
        public const int DISPLAY_BLINK = 2;
        
        private String _content;
        private int _lblCounter; // letter by letter counter (internal use)
        private int _lblStep; // internal use
        private int _lblInterval; // letter by letter time interval
        private double _lblIntervaltmp;
        private Vector2 _location;
        private Color _textCurrentColor;
        private int _displayMode;
        public bool _visible { get; set; }
        public float _scale { get; set; }

        // Default values
        private SpriteFont _font = ResourceManager.font_message;

        public Message(String content, Vector2 location,float scale = 1.0f, int DISPLAY_MODE = Message.DISPLAY_NORMAL, int interval = 50)
        {
            // Init
            _content = content;
            _lblCounter = 0;
            _lblInterval = interval; // ms
            _lblIntervaltmp = 0;
            _location = location;
            _displayMode = DISPLAY_MODE;
            _visible = true;
            _scale = scale;

            // Default values
            _textCurrentColor = Color.White;
        }

        public void SetText(String content)
        {
            _lblCounter = 0; // Reset display counter
            _content = content;
        }

        public void SetMode(int mode, int interval)
        {
            _displayMode = mode;
            _lblInterval = interval;
        }

        public void Update(GameTime gameTime)
        {
            // Normal display
            if(_displayMode == Message.DISPLAY_NORMAL)
                return;
            // Letter by letter display
            else if(_displayMode == Message.DISPLAY_LBL)
            {

                // If we reached the final caracter, we stop
                if (_lblCounter == _content.Length)
                   return;
                // We display the next caracter when the time interval is fine
                if ((gameTime.TotalGameTime.TotalMilliseconds - _lblIntervaltmp) >= _lblInterval)
                {
                    _lblCounter++;
                    _lblIntervaltmp = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }
            // Blink display
            else
            {
                if ((gameTime.TotalGameTime.TotalMilliseconds - _lblIntervaltmp) >= _lblInterval)
                {
                    _visible = !_visible;
                    _lblIntervaltmp = gameTime.TotalGameTime.TotalMilliseconds;
                }
            }
            
        }

        public void Render(GameTime gameTime)
        {
            String text;
            if (_displayMode == Message.DISPLAY_LBL)
                text = _content.Substring(0, _lblCounter);
            else
                text = _content;

            if(_visible)
                RenderManager.DrawString(_font, text, _location, _textCurrentColor,_scale);
        }
       
    }
}
