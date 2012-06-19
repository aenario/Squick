using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Squick.Utility;

namespace Squick.Scene.Effects
{
    public class Fade
    {
        public enum EFFECT{ FADE_OUT };

        private Boolean _hasFinished = false;
        private Boolean _isRunning = false;

        private float _transparency = 1.0f;
        private EFFECT _currentEffect = EFFECT.FADE_OUT;
        private Color _currentColor = Color.Black;
        private double _durationTime = 100; // 1 sec
        private double _interval;
        private double _stepTimer;
        private int _step;

        public void Start(Fade.EFFECT effect, Color color, double durationTime)
        {
            // Reset global state
            _hasFinished = false;
            _isRunning = true;
            _step = 0; // Reset (100 = finished)

            // Default parameters
            _currentEffect = effect;
            if (effect == EFFECT.FADE_OUT)
                _transparency = 1.0f; // -> 0
            else
                _transparency = 0.0f; // -> 1 (Fade_in)
            _durationTime = durationTime;

            _currentColor = color;

            // Compute step
            _interval = _durationTime / 100;
            
        }

        public void Stop()
        {
            this._isRunning = false;
        }

        public void Update(GameTime gameTime)
        {
            if (!_isRunning || _hasFinished)
                return;

            // Increase speed every 0.5 seconds
            if (gameTime.TotalGameTime.TotalMilliseconds - _stepTimer >= _interval)
            {
                if (_currentEffect == EFFECT.FADE_OUT)
                    _transparency -= 0.01f;
                else
                    _transparency += 0.01f;
                
                // State machine
                if (_step++ >= 100)
                    _hasFinished = true;

                // Update timer
                _stepTimer = gameTime.TotalGameTime.TotalMilliseconds;

                //Console.WriteLine(_step);
            }
        }
        public void Render(GameTime gameTime)
        {
            if (!_isRunning)
                return;

            // Draw a plain rectangle screen wide
            RenderManager.DrawBox(new Rectangle(0,0,Squick.SCREEN_WIDTH,Squick.SCREEN_HEIGHT),_currentColor,_transparency);
        }

        public Boolean HasFinished() { return _hasFinished; }
    }
}
