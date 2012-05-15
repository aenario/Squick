using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Squick.Control;
using Squick.UI;
using Squick.Utility;

namespace Squick.Scene
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class Menu : Scene{

        private TextButton _startButton;
        private TextButton _quitButton;

        public Menu()
        {
            _startButton = new TextButton("Start", new Vector2(100, 100));
            _quitButton = new TextButton("Quit", new Vector2(100, 300));
        }

        public override void Update(GameTime gameTime, KinectInterface gameInput)
        {
            // Update buttons state
            _startButton.Update(gameTime, gameInput);
            _quitButton.Update(gameTime, gameInput);

            // Action
            if (_startButton.IsPressed())
            {
                _nextScene = new Level1(); // Game will quit
                _sceneFinished = true;
            }
            else if (_quitButton.IsPressed())
            {
                _nextScene = null; // Game will quit
                _sceneFinished = true;
            }

        }

        public override void Render(GameTime gameTime)
        {
            // Draw GameTitle
            // TODO

            // Draw UI
            _startButton.Render(gameTime);
            _quitButton.Render(gameTime);
        }
    }
}
