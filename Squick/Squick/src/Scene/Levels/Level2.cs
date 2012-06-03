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
using Squick.Component.Misc;
using Squick.Utility;
using Squick.Scene.Menus;
using Squick.Component.Collectible;

namespace Squick.Scene.Levels
{
    public class Level2 : Scene{

        private static float gravity = 3;
        private static float impactTrigger = 0.05f; // squick will bounce 0.05s before hitting branch
        private static float distanceToTop = 150;
        private static bool destroyable(Entity e){
            return e.Pos.Y > 1200;
        }


        private float newSpeed(float oldSpeed, float bounceLength){
            return MathHelper.Clamp(2 * oldSpeed + 5*(200 - bounceLength), 300, 1200); }

        private Texture2D _levelBackground;
        private List<Entity> items = new List<Entity>();
        private JumpingSquick squick;
        private Branch activeBranch;
        private List<Branch> oldBranches;

        private float cameraOffset = 0;

        private Vector2 squickBottom;

        public Level2(KinectInterface gameInput)
        {
            _levelBackground = ResourceManager.tex_background_level2;
            squick = new JumpingSquick(gameInput);
            squick.Pos = new Vector2(400, 100);
            squick.Speed = new Vector2(0,0);

            activeBranch = new Branch(gameInput);
            oldBranches = new List<Branch>();
        }

        public override void Update(GameTime gameTime, KinectInterface gameInput)
        {

            activeBranch.Update(gameTime);
            squick.Update(gameTime);
            foreach (Entity i in items) ((Collectible)i).Update(gameTime);
            squick.SpeedY += gravity;
                        
            squickBottom = Vector2.Add(squick.Pos, new Vector2(squick.Width/2, 0.9f*squick.Height));
            var later = Vector2.Add(squickBottom, Vector2.Multiply(squick.Speed, impactTrigger));
            bool aboutToCross = activeBranch.isBelow(squickBottom) && activeBranch.isAbove(later);

            if (aboutToCross) processJump(gameInput, gameTime);

            if (squick.Pos.Y < distanceToTop && squick.Speed.Y < 0) // squick monte
            {
                
                var delta = squick.Pos.Y - distanceToTop;
                var oldOff = cameraOffset;
                
                foreach (EntityFactory item in Level2CollectibleFactory.getSpawnBetween((int) cameraOffset, (int)(cameraOffset-delta)))
                {
                    Collectible i = (Collectible) item.asEntity();
                    i.MovementPattern = Collectible.MOVEMENT_NONE;
                    i.PosY -= cameraOffset;
                    items.Add(i);
                }

                squick.PosY = distanceToTop;
                foreach (Entity e in oldBranches) e.PosY -= delta;
                foreach (Entity e in items) e.PosY -= delta;

                cameraOffset -= delta;

                oldBranches.RemoveAll(destroyable);
                items.RemoveAll(destroyable);
            }

            

            if (squick.Pos.X < 40 || squick.Pos.X > 600) squick.Speed = new Vector2(-squick.Speed.X, squick.Speed.Y);

            // DEBUG ONLY
            if(squick.Pos.Y > 600) squick.Pos = new Vector2(squick.Pos.X, 0);
            
            /*
            if(squick.Pos.Y > 600){
                _sceneFinished = true;
                _nextScene = new DebugMenu();
            }
             */
            
        }

        private void processJump(KinectInterface gameInput, GameTime gameTime )
        {
            float oldSpeed = squick.Speed.Length();
            Vector2 direction = Vector2.Reflect(squick.Speed, activeBranch.Normal);

            squick.Speed = Vector2.Multiply(Vector2.Normalize(direction),
                newSpeed(oldSpeed, activeBranch.BounceLength));

            activeBranch.Fix();
            oldBranches.Add(activeBranch);
            activeBranch = new Branch(gameInput);
            activeBranch.Update(gameTime);
        }

        public override void Render(GameTime gameTime)
        {

            Rectangle back1 = new Rectangle(0, (int) (cameraOffset % 600f) - 600, _levelBackground.Width, _levelBackground.Height);
            Rectangle back2 = new Rectangle(0, (int) (cameraOffset % 600f), _levelBackground.Width, _levelBackground.Height);

            RenderManager.Draw2DTexture( _levelBackground, back1, Color.White);
            RenderManager.Draw2DTexture(_levelBackground, back2, Color.White);
            RenderManager.DrawString(ResourceManager.font_UI, "Height : " + cameraOffset, new Vector2(10, 30), Color.Gold);

            squick.Render(gameTime);
            activeBranch.Render(gameTime);
            RenderManager.DrawBox(new Rectangle((int) squickBottom.X, (int) squickBottom.Y, 3, 3));

            foreach(Branch b in oldBranches) b.Render(gameTime);
            foreach (Entity i in items)
            {
                Console.WriteLine(i.Pos.X + ":" + i.Pos.Y + " 3");
                i.Render(gameTime);
            }

        }




    }
}
