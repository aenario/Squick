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

namespace Squick.Scene.Levels
{
    public class Level1 : Scene{

        private static int GravitySpeed = 50;
        private static int leftBound = 35;
        private static int rightBound = 700;

        private Texture2D _levelBackground;
        private DizzySquick squick;
        private List<Entity> items = new List<Entity>();
        private bool justStarted = true;// CHANGE TO COMPLEXE ENTITY

        private int _score;
        private int _chainBonus;

        //private Player _squick;

        public Level1(KinectInterface gameInput)
        {
            _levelBackground = ResourceManager.tex_background_level1;
            squick = new DizzySquick(gameInput);
            squick.Pos = new Vector2(400, 400);

            // Level score
            _score = 0;
            _chainBonus = 0;
        }

        public override void Update(GameTime gameTime, KinectInterface gameInput)
        {
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
            foreach (Collectible item in items) // quick & dirty
            {
                item.Update(gameTime);

                if (item.Pos.Y > 600)
                {
                    //TODO items.Remove(item);
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
                        _score -= item.GetBonus();
                    }

                    //TODO items.Remove(item);
                }

                
                // Remove if under the ground

                
            }
            
            /* Make squick bump */
            if ((squick.Pos.X < 36 && squick.Speed.X < leftBound)
                 || (squick.Pos.X + squick.Width > rightBound && squick.Speed.X > 0))
                        squick.Speed = Vector2.Negate(squick.Speed);
             
            squick.Update(gameTime);
        }

        public override void Render(GameTime gameTime)
        {
            // Background
            RenderManager.Draw2DTexture(_levelBackground, _levelBackground.Bounds, Color.White);

            // Components
            squick.Render(gameTime);
            foreach (Entity item in items)
            {
                item.Render(gameTime);
            }

            // HUD
            RenderManager.DrawString(ResourceManager.font_UI, _score.ToString(), new Vector2(0, 580), Color.Blue); 
            
        }


    }
}
