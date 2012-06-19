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
using Squick.UI;
using Squick.src.Utility;

namespace Squick.Scene.Levels
{
    public class Level2 : Scene{

        /* GAMEPLAY */
        private const float goal = 50000;
        private static float gravity = 85;
        private static double friction = 0.9d;
        private static float impactTrigger = 0.1f; // squick will bounce 0.05s before hitting branch
        private static float distanceToTop = 150;
        private int nbOfLives = 5;
        private float newSpeed(float oldSpeed, float bounceLength)
        {
            return MathHelper.Clamp(2 * oldSpeed + 7 * (250 - bounceLength), 300, 900);
        }
        private float score(float time)
        {
            return MathHelper.Clamp(9999f - 40f * (time - 30f), 0, 9999);
        }


        /* MISC */
        private float cameraOffset = 0;
        private TimeSpan beginLevel = new TimeSpan();

        
        /* DESTROYABLE FUNCTIONS */
        private static bool destroyable(Entity e)
        {
            return e.Pos.Y > 1200;
        }
        private static bool destroyable(Branch b)
        {
            return b.Destroyable || b.Pos.Y > 1200;
        }

        
        

        private Texture2D _levelBackground;
        private List<Entity> items = new List<Entity>();
        private JumpingSquick squick;
        private Branch activeBranch;
        private List<Branch> oldBranches;
        private List<Entity> toBeDestroy;
        private Gauge gauge;
        private HandCursors hc;

        

        public Level2(KinectInterface gameInput)
        {
            _levelBackground = ResourceManager.tex_background_level2;
            squick = new JumpingSquick(gameInput);
            squick.Pos = new Vector2(400, 800);
            squick.Speed = new Vector2(0,-600);
            toBeDestroy = new List<Entity>();

            activeBranch = new Branch(gameInput);
            oldBranches = new List<Branch>();
            cameraOffset = 0;

            gauge = new Gauge(new Rectangle(30, 50, 10, 500), goal, Color.BurlyWood, Color.Gold);
            hc = new HandCursors();
        }

        public override void Update(GameTime gameTime, KinectInterface gameInput)
        {
            
            if(beginLevel.TotalMilliseconds == 0) beginLevel = gameTime.TotalGameTime;
            
            if (gameTime.TotalGameTime.Subtract(beginLevel).TotalSeconds < 10d)
            {
                hc.Update(gameTime, gameInput);
                activeBranch.Update(gameTime);
                return;
            }
            else hc = null;
            /* Update everything */
            squick.Update(gameTime);
            gauge.Update(cameraOffset);
            activeBranch.Update(gameTime);
            foreach (Collectible i in items) i.Update(gameTime);
            oldBranches.RemoveAll(destroyable);
            foreach (Branch b in oldBranches) b.Update(gameTime);

            /* gravity & friction & bounce */
            squick.SpeedY += gravity * (float) gameTime.ElapsedGameTime.TotalSeconds;
            squick.SpeedX *= (float) Math.Pow(friction, gameTime.ElapsedGameTime.TotalSeconds);
            bounceAgainstWalls(squick);
            
            /* make squick jump on branches */
            processJump(gameInput, gameTime, activeBranch);
            foreach (Branch b in oldBranches) processJump(gameInput, gameTime, b);

            /* squick is going near top screen */
            if (squick.Pos.Y < distanceToTop && squick.Speed.Y < 0) 
            {
                
                var delta = squick.Pos.Y - distanceToTop;
                
                foreach (EntityFactory item in Level2CollectibleFactory.getSpawnBetween((int) cameraOffset, (int)(cameraOffset-delta)))
                {
                    Collectible i = (Collectible) item.asEntity();
                    i.MovementPattern = Collectible.MOVEMENT_NONE;
                    i.PosY -= cameraOffset;
                    items.Add(i);
                }

                // squick at top screen
                squick.PosY = distanceToTop;

                // move other entities
                foreach (Entity e in oldBranches) e.PosY -= delta;
                foreach (Entity e in items) e.PosY -= delta;

                // destroy everything under screen
                items.RemoveAll(destroyable);

                cameraOffset -= delta;

                if (cameraOffset > goal)
                {
                    _sceneFinished = true;
                    ScoreHolder.Level2 = (int) score((float) gameTime.TotalGameTime.Subtract(beginLevel).TotalSeconds);
                    _nextScene = new VictoryMenu();
                }

            }

            
            foreach (Entity i in items)
                if(squick.Collide(i)){
                    int impact = ((Collectible) i).GetBonus();
                    if (impact > 300) // golden nuts = life
                    {
                        nbOfLives++;
                    }
                    else
                    {
                        squick.SpeedY = Math.Min(squick.SpeedY - 6 * impact, 0);
                    }
                    squick.makeDizzy();
                    toBeDestroy.Add(i);
                }

            if (toBeDestroy.Count > 0)
            {
                items.RemoveAll(toBeDestroy.Contains);
                toBeDestroy.Clear();
            }

            if (squick.Speed.Y > 0 && squick.Pos.Y > 600)
            {
                if (nbOfLives == 1) // was the last life
                {
                    _sceneFinished = true;
                    _nextScene = new GameOverMenu(2);
                    return;
                }
                nbOfLives--;
                squick.PosY = -200;
                squick.Speed = Vector2.Zero;
            }
            
        }

        private void processJump(KinectInterface gameInput, GameTime gameTime, Branch b)
        {
            var later = Vector2.Add(squick.Bottom, Vector2.Multiply(squick.Speed, impactTrigger));
            bool aboutToCross = b.isBelow(squick.Bottom) && b.isAbove(later);

            if (!aboutToCross) return; 

            float oldSpeed = squick.Speed.Length();
            Vector2 direction = Vector2.Reflect(squick.Speed, b.Normal);

            squick.Speed = Vector2.Multiply(Vector2.Normalize(direction),
                newSpeed(oldSpeed, b.BounceLength));

            b.HitAndBreak(gameTime);
            if (b.Equals(activeBranch))
            {
                oldBranches.Add(activeBranch);
                activeBranch = new Branch(gameInput);
                activeBranch.Update(gameTime);
            }

        }

        private void bounceAgainstWalls(Entity e)
        {
            Rectangle b = e.GetBoundingBox();
            if (b.X < 40 && e.SpeedX < 0 || b.X + b.Width > 700 && e.SpeedX > 0) e.SpeedX = -e.SpeedX;
        }

        public override void Render(GameTime gameTime)
        {

            Rectangle back1 = new Rectangle(0, (int) (cameraOffset % 600f) - 600, _levelBackground.Width, _levelBackground.Height);
            Rectangle back2 = new Rectangle(0, (int) (cameraOffset % 600f), _levelBackground.Width, _levelBackground.Height);

            RenderManager.Draw2DTexture( _levelBackground, back1, Color.White);
            RenderManager.Draw2DTexture(_levelBackground, back2, Color.White);
            
            RenderManager.DrawString(ResourceManager.font_score, cameraOffset.ToString("000 000"), new Vector2(70, 0), Color.Gold);

            squick.Render(gameTime);
            activeBranch.Render(gameTime);
            gauge.Render(gameTime);

            Rectangle bb = new Rectangle((int) gauge.Top.X - 15, (int) gauge.Top.Y - 15, 30, 30);
            RenderManager.Draw2DTexture(ResourceManager.tex_squick_headCut, bb, Color.White);

            RenderManager.DrawBox(new Rectangle((int) squick.Bottom.X, (int) squick.Bottom.Y, 3, 3));

            foreach(Branch b in oldBranches) b.Render(gameTime);
            foreach (Entity i in items) i.Render(gameTime);

            for (int i = 0; i < nbOfLives; i++)
            {
                Rectangle b = new Rectangle(660 - 40*i, 10, 30, 30);
                RenderManager.Draw2DTexture(ResourceManager.tex_squick_headCut, b, Color.White);
            }
            
            if(hc != null) hc.Render(gameTime);

        }




    }
}
