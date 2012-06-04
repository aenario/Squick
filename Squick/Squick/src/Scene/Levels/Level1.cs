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

using Squick.Control;
using Squick.Component;
using Squick.Component.Player;
using Squick.Component.Collectible;
using Squick.Scene;
using Squick.Utility;
using Squick.UI;
using Squick.Scene.Menus;

namespace Squick.Scene.Levels
{
    public class Level1 : Scene{

        private static int GravitySpeed = 2;
        private static int leftBound = 35;
        private static int rightBound = 700;

        // HUD
        private Score _HUD_score;
        private TextButton _backToMenu;
        private Message _message;
        
        // Game components
        private Texture2D _levelBackground;
        private DizzySquick squick;
        private List<Entity> items = new List<Entity>();
    
        // Player score
        private int _score;
        private int _chainBonus;
            
        // Others
        private bool justStarted = true;// CHANGE TO COMPLEXE ENTITY
       
        //private Player _squick;

        public Level1(KinectInterface gameInput)
        {
            _levelBackground = ResourceManager.tex_background_level1;

            _HUD_score = new Score(new Vector2(10,10));
            _backToMenu = new TextButton("Menu", new Vector2(690, 10));

            squick = new DizzySquick(gameInput);
            squick.Pos = new Vector2(400, 400);

            _message = new Message("SALUT JE SUIS un PUTAIN DE \n LONG TEXTE ET JE T'EMMERDE!", new Vector2(20, 200));


            // Level score
            _score = 0;
            _chainBonus = 0;
        }

        public override void Update(GameTime gameTime, KinectInterface gameInput)
        {
            _backToMenu.Update(gameTime, gameInput);
            if (_backToMenu.IsPressed())
            {
                _nextScene = new DebugMenu(); 
                _sceneFinished = true;
            }

            if (justStarted)
            {
                //Level1Spawn.startNow(gameTime);
                justStarted = false;
            }

            // spawn items 
            foreach (EntityFactory se in Level1CollectibleFactory.getSpawnAt(gameTime))
            {
                Entity item = se.asEntity();
                item.Speed = new Vector2(0, GravitySpeed);
                items.Add(item);
            }
            
            // destroy items below screen
            List<Collectible> toBeDestroy = new List<Collectible>();
            foreach (Collectible item in items) // quick & dirty
            {
                item.Update(gameTime);

                if (item.Pos.Y > 600)
                {
                    toBeDestroy.Add(item);
                    continue;
                }

                // Collisions
                if (item.GetBoundingBox().Intersects(squick.GetBoundingBox()))
                {
                    if (item.GetBonus() > 0)
                    {
                        _chainBonus++;
                        _score += ((int)_chainBonus / 10 + 1) * item.GetBonus();
                    }
                    else
                    {
                        _chainBonus = 0;
                        _score += item.GetBonus();
                    }
                    toBeDestroy.Add(item);
                }

                
                // Remove if under the ground

                
            }

            foreach(Collectible i in toBeDestroy) items.Remove(i);
            
            /* Make squick bump */
            if ((squick.Pos.X < 36 && squick.Speed.X < leftBound)
                 || (squick.Pos.X + squick.Width > rightBound && squick.Speed.X > 0))
                        squick.Speed = Vector2.Negate(squick.Speed);
            
            // HUD update
            _HUD_score.Update(_score);
            _message.Update(gameTime);

            squick.Update(gameTime);
        }

        public override void Render(GameTime gameTime)
        {
            // Background
            RenderManager.Draw2DTexture(_levelBackground, _levelBackground.Bounds, Color.White);

            _backToMenu.Render(gameTime);

            // Components
            squick.Render(gameTime);
            foreach (Entity item in items)
            {
                item.Render(gameTime);
            }

            // HUD
            _HUD_score.Render(gameTime);
            _message.Render(gameTime);
            
        }


    }
}
