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

using Squick.Scene;
using Squick.Control;
using Squick.Utility;

namespace Squick
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Squick : Microsoft.Xna.Framework.Game
    {
        public static String version = "0.1";
        private GraphicsDeviceManager _graphicsDevice;
        private SpriteBatch spriteBatch;
        private KinectManager _kinectsManager;
        private KinectInterface _gameInput;
        private SceneManager _sceneManager;
        private SpriteFont baseFont;

        public Squick()
        {
            _graphicsDevice = new GraphicsDeviceManager(this);
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
            // Set graphic options
            _graphicsDevice.PreferredBackBufferWidth = 800;
            _graphicsDevice.PreferredBackBufferHeight = 600;
            _graphicsDevice.IsFullScreen = false;
            _graphicsDevice.ApplyChanges();

            // Managers (singleton static classes)
            RenderManager.Initialize(GraphicsDevice);
            ResourceManager.Initialize(Content,GraphicsDevice);

            // Initialize Kinect and its interface
            _kinectsManager = new KinectManager();
            _gameInput = new KinectInterface(_kinectsManager, KinectInterface.MODE_HANDS);

            _sceneManager = new SceneManager(this,_gameInput);
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
            baseFont = ResourceManager.font_UI;

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
            _sceneManager.Update(gameTime);

            //base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _sceneManager.Render(gameTime);

            // TEST
            spriteBatch.Begin();
            // . Draw hands coordinates
            DepthImagePoint[] dip = _gameInput.GetLatestCoordinates();
            String left = "["+dip[KinectInterface.LEFT_HAND].X.ToString() + ","
                + dip[KinectInterface.LEFT_HAND].Y.ToString() + " ] ";
            Vector2 vLeft = new Vector2(dip[KinectInterface.LEFT_HAND].X, dip[KinectInterface.LEFT_HAND].Y);
            String right = "[" + dip[KinectInterface.RIGHT_HAND].X.ToString() + ","
                + dip[KinectInterface.RIGHT_HAND].Y.ToString() + "]";
            Vector2 vRight = new Vector2(dip[KinectInterface.RIGHT_HAND].X, dip[KinectInterface.RIGHT_HAND].Y);
            spriteBatch.DrawString(baseFont, left, vLeft, Color.White);
            spriteBatch.DrawString(baseFont, right, vRight, Color.Yellow);
            spriteBatch.DrawString(baseFont, _kinectsManager.connectedStatus, new Vector2(10.0f), Color.Red);
            spriteBatch.End();

            // . Draw hand cursors bounding boxes
            RenderManager.StartRendering();
            Rectangle[] cursors = _gameInput.GetHandCursorsBoundingBoxes();
            RenderManager.DrawBox(cursors[KinectInterface.LEFT_HAND]);
            RenderManager.DrawBox(cursors[KinectInterface.RIGHT_HAND]);
            RenderManager.EndRendering();
            // END TEST
        }
    }
}
