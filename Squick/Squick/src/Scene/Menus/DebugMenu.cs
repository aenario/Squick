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
using Squick.Scene.Levels;

namespace Squick.Scene.Menus
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class DebugMenu : Scene{

        private TextButton _startButton;
        private TextButton _start2Button;
        private TextButton _quitButton;

        public DebugMenu()
        {
            _startButton = new TextButton("Level1", new Vector2(100, 100));
            _start2Button = new TextButton("Level2", new Vector2(300, 100));
            _quitButton = new TextButton("Quit", new Vector2(100, 300));
        }

        public override void Update(GameTime gameTime, KinectInterface gameInput)
        {
            // Update buttons state
            _startButton.Update(gameTime, gameInput);
            _start2Button.Update(gameTime, gameInput);
            _quitButton.Update(gameTime, gameInput);

            // Action
            if (_startButton.IsPressed())
            {
                _nextScene = new Level1(gameInput); // Game will quit
                _sceneFinished = true;
            }
            else if (_start2Button.IsPressed())
            {
                _nextScene = new Level2(gameInput); // Game will quit
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
            _start2Button.Render(gameTime);
            _quitButton.Render(gameTime);
        }
    }
}
