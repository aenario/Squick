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
using Squick.Utility;
using Squick.src.Entity;
using Squick.src.Scene.Levels;
using Squick.src.Scene;

namespace Squick.Scene
{
    public class Level1 : Scene{

        private Texture2D _levelBackground;
        private dizzySquick squick;
        private List<Entity> items = new List<Entity>();
        private bool justStarted = true;// CHANGE TO COMPLEXE ENTITY

        //private Player _squick;

        public Level1(KinectInterface gameInput)
        {
            _levelBackground = ResourceManager.tex_background_level1;
            squick = new dizzySquick(gameInput);
            squick.Pos = new Vector2(400, 400);
            
        }

        public override void Update(GameTime gameTime, KinectInterface gameInput)
        {
            if (justStarted)
            {
                Level1Spawn.startNow(gameTime);
                justStarted = false;
            }

            /* spawn items */
            foreach (SpawnEntry se in Level1Spawn.getSpawnAt(gameTime))
            {
                Entity item = se.asEntity();
                item.Speed = new Vector2(0, 5);
                items.Add(item);
            }

            /* destroy items below screen */
            foreach (Entity item in items)
            {
                if (item.Pos.Y > 600) items.Remove(item);
            }

            /* Make squick bump */
            if ((squick.Pos.X < 0 && squick.Speed.X < 0)
                 || (squick.Pos.X + squick.Width > 800 && squick.Speed.X > 0))
                        squick.Speed = Vector2.Negate(squick.Speed);
             

            squick.Update(gameTime);
        }

        public override void Render(GameTime gameTime)
        {
            RenderManager.Draw2DTexture(_levelBackground, _levelBackground.Bounds, Color.White);
            RenderManager.DrawEntity(squick);
            foreach (Entity item in items)
            {
               RenderManager.DrawEntity(item);
            }
        }




    }
}
