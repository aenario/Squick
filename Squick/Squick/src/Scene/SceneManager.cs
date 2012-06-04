using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Squick.Control;
using Squick.Scene.Menus;
using Squick.Scene.Levels;
using Squick.Utility;

namespace Squick.Scene
{
    public class SceneManager
    {
        private Scene _currentScene;
        private Squick _game;
        private KinectInterface _gameInput;

        public SceneManager(Squick game, KinectInterface gameInput)
        {
            _game = game;
            _gameInput = gameInput;
            _currentScene = new Level1(gameInput); // Change with your level, you bitch
            //_currentScene = new DebugMenu(); // For testing purpose
        }

        public void Update(GameTime gameTime)
        {
            // If the current scene is finished, we switch to the next one
            if (_currentScene.IsSceneFinished())
            {
                if (_currentScene.GetNextScene() == null)
                    _game.Exit();
                else
                    _currentScene = _currentScene.GetNextScene();
            }
            
            _currentScene.Update(gameTime, _gameInput);
        }

        public void Render(GameTime gameTime)
        {
            RenderManager.StartRendering();
            _currentScene.Render(gameTime);
            RenderManager.EndRendering();
        }

    }
}
