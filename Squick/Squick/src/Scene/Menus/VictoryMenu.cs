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
    public class VictoryMenu : Scene{

        private Texture2D _background;
        private TextBox _victoryMsg;
        private TextButton _backToMenuBtn;
        private TextButton _quitBtn;

        private HandCursors _hc;


        public VictoryMenu()
        {
            _background = ResourceManager.tex_background_level1;
            _hc = new HandCursors();
            _victoryMsg = new TextBox("Victory", new Rectangle(100, 50, 600, 100));
            _backToMenuBtn = new TextButton("Back to Menu", new Rectangle(100, 250, 200, 100));
            _quitBtn = new TextButton("Quit", new Rectangle(500, 450, 150, 100));
        }

        public override void Update(GameTime gameTime, KinectInterface gameInput)
        {
            // Update buttons state

            _victoryMsg.Update(gameTime, gameInput);
            _backToMenuBtn.Update(gameTime, gameInput);
            _quitBtn.Update(gameTime, gameInput);

            _hc.Update(gameTime, gameInput);
        
            // Action
            if (_backToMenuBtn.IsPressed())
            {
                _nextScene = new MainMenu(); 
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
            _victoryMsg.Render(gameTime);
            _backToMenuBtn.Render(gameTime);
            _quitBtn.Render(gameTime);

            _hc.Render(gameTime);
        }
    }
}
