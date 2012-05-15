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

namespace Squick.Scene
{
    public abstract class Scene
    {

        protected bool _sceneFinished;
        protected Scene _nextScene;
        
        public Scene()
        {
            this._sceneFinished = false;
            this._nextScene = null;
        }

        public abstract void Update(GameTime gameTime, KinectInterface gameInput);
        public abstract void Render(GameTime gameTime);

        public bool IsSceneFinished(){ return _sceneFinished; }
        public Scene GetNextScene() { return _nextScene; }

    }
}
