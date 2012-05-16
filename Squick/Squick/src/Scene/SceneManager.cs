﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Squick.Control;

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
            _currentScene = new Menu(); 
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
            _currentScene.Render(gameTime);
        }

    }
}
