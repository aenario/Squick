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


namespace Squick.components
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class gcKinectHandler : Microsoft.Xna.Framework.GameComponent
    {

        KinectsManager kinectsManager;

        public gcKinectHandler(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            // get kinect
            kinectsManager = new KinectsManager();
            if (kinectsManager.kinectSensor != null)
            {
                kinectsManager.kinectSensor.DepthStream.Enable();
                kinectsManager.kinectSensor.SkeletonStream.Enable();
                kinectsManager.startSensor();
            }
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
    }
}
