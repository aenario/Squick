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
    public class MainMenu : Scene{

        private Texture2D _background;
        private TextButton _aventureBtn;
        private TextButton _level1Btn;
        private TextButton _level2Btn;
        private TextButton _quitBtn;

        private HandCursors _hc;

        public MainMenu()
        {
            _background = ResourceManager.tex_background_level1;
            _hc = new HandCursors();
            _aventureBtn = new TextButton("The Epic Quest of Squick", new Rectangle(50, 50, 600, 100));
            _level1Btn = new TextButton("Level 1", new Rectangle(50, 200, 200, 100));
            _level2Btn = new TextButton("Level 2", new Rectangle(50, 350, 200, 100));
            _quitBtn = new TextButton("Quit", new Rectangle(500, 450, 150, 100));
        }

        public override void Update(GameTime gameTime, KinectInterface gameInput)
        {
            // Update buttons state

            _hc.Update(gameTime, gameInput);
            _aventureBtn.Update(gameTime, gameInput);
            _level1Btn.Update(gameTime, gameInput);
            _level2Btn.Update(gameTime, gameInput);
            _quitBtn.Update(gameTime, gameInput);
        
            // Action
            if (_level1Btn.IsPressed() || _aventureBtn.IsPressed())
            {
                _nextScene = new Level1(gameInput); // Game will quit
                _sceneFinished = true;
            }
            else if (_level2Btn.IsPressed())
            {
                _nextScene = new Level2(gameInput); // Game will quit
                _sceneFinished = true;
            }
            else if (_quitBtn.IsPressed())
            {
                _nextScene = null; // Game will quit
                _sceneFinished = true;
            }

        }

        public override void Render(GameTime gameTime)
        {
            // Draw GameTitle
            // TODO
            RenderManager.Draw2DTexture(_background, new Vector2(0, 0), Color.White);

            // Draw UI
            _aventureBtn.Render(gameTime);
            _level1Btn.Render(gameTime);
            _level2Btn.Render(gameTime);
            _quitBtn.Render(gameTime);

            _hc.Render(gameTime);
        }
    }
}
