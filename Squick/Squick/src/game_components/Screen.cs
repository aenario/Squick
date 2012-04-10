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


namespace Squick
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public abstract class Screen : Microsoft.Xna.Framework.DrawableGameComponent
    {

        private bool _screenFinished;
        private Screen _nextScreen;
        
        public Screen(Game game) : base(game)
        {
            this._screenFinished = false;
            this._nextScreen = null;
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            
            base.Draw(gameTime);
        }

        public Screen GetNextScreen()
        {
            return this._nextScreen;
        }

        public void SetNextScreen(Screen screen)
        {
            this._screenFinished = true;
            this._nextScreen = screen;
        }

        public bool IsScreenFinished()
        {
            return this._screenFinished;
        }

    }
}
