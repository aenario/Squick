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
using Microsoft.Kinect;


namespace Squick
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Squick : Microsoft.Xna.Framework.Game
    {
        public static String version = "0.1";
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private KinectManager _kinectsManager;
        private KinectInterface _kinectInterface;
        private Screen _currentLevel { get; set; }
        private SpriteFont baseFont;

        public Squick()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
           
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // set graphic options
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();

            // get kinect
            _kinectsManager = new KinectManager();
            _kinectInterface = new KinectInterface(_kinectsManager, KinectInterface.modeHands);

            
            _currentLevel = new Menu(this);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            baseFont = Content.Load<SpriteFont>("baseFont");

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            

            /* Update logic */
            // Screen change?
            if (this._currentLevel.IsScreenFinished())
                this._currentLevel = this._currentLevel.GetNextScreen();


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            DepthImagePoint[] dip =  _kinectInterface.getLatestCoordinates();

            String left = "["+dip[KinectInterface.leftHand].X.ToString() + ","
                + dip[KinectInterface.leftHand].Y.ToString() + " ] ";
            Vector2 vLeft = new Vector2(dip[KinectInterface.leftHand].X, dip[KinectInterface.leftHand].Y);
            String right = "[" + dip[KinectInterface.rightHand].X.ToString() + ","
                + dip[KinectInterface.rightHand].Y.ToString() + "]";
            Vector2 vRight = new Vector2(dip[KinectInterface.rightHand].X, dip[KinectInterface.rightHand].Y);

            spriteBatch.DrawString(baseFont, left, vLeft, Color.White);
            spriteBatch.DrawString(baseFont, right, vRight, Color.Yellow);
            spriteBatch.DrawString(baseFont, _kinectsManager.connectedStatus, new Vector2(10.0f), Color.Red);

            // TODO: Add your drawing code here

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
