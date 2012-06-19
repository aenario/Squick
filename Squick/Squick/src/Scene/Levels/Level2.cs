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
using Squick.Scene.Effects;

namespace Squick.Scene.Levels
{
    public class Level2 : Scene{

        /* GAMEPLAY */
        private const float goal = 50000;
        //private const float goal = 10000; // debug only
        private static float gravity = 85;
        private static double friction = 0.9d;
        private static float impactTrigger = 0.1f; // squick will bounce 0.05s before hitting branch
        private static float distanceToTop = 150;
        private int nbOfLives = 5;
        private float newSpeed(float oldSpeed, float bounceLength)
        {
            return MathHelper.Clamp(2 * oldSpeed + 7 * (250 - bounceLength), 300, 900);
        }
        private Vector2 newSpeedItem(Vector2 oldSpeed, int impact)
        {
            float newSpeed = oldSpeed.Y - 6 * (impact-50);
            float clamped = impact > 0 ^ oldSpeed.Y > 0 ?
                MathHelper.Clamp(newSpeed, float.MinValue, oldSpeed.Y) : 
                MathHelper.Clamp(newSpeed, oldSpeed.Y, 0);
            
            return new Vector2(oldSpeed.X, clamped);

        }
        private float score(float time)
        {
            return MathHelper.Clamp(9999f - 40f * (time - 30f), 0, 9999);
        }


        /* MISC */
        private float cameraOffset = 0;
        private TimeSpan beginLevel = new TimeSpan();
        private TimeSpan endLevel = new TimeSpan();
        private bool finished
        {
            get { return endLevel.TotalMilliseconds != 0;  }
        }

        
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
        private Fade fadeEffect;
        private Gauge gauge;
        private Message message;
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

            fadeEffect = new Fade();

            gauge = new Gauge(new Rectangle(30, 50, 10, 500), goal, Color.BurlyWood, Color.Gold);
            message = new Message("Training time !", new Vector2(20, 220), 2.0f, Message.DISPLAY_LBL, 50);
            hc = new HandCursors();
        }

        public override void Update(GameTime gameTime, KinectInterface gameInput)
        {

            if (endLevel.TotalMilliseconds != 0)
            {
                double doneSince = Math.Round(gameTime.TotalGameTime.Subtract(endLevel).TotalSeconds, 1);

                if (doneSince == 3d) fadeEffect.Start(Fade.EFFECT.FADE_IN, Color.Black, 2000);
                if (doneSince == 5d)
                {
                    _sceneFinished = true;
                    _nextScene = new VictoryMenu();
                }

                moveCamera(gameTime);
                items.Clear(); // moveCamera keep spawning
                squick.Update(gameTime);
                bounceAgainstWalls(squick);
                fadeEffect.Update(gameTime);
                message.Update(gameTime);
                return;
            }
            else if (cameraOffset > goal)
            {
                endLevel = gameTime.TotalGameTime;
                ScoreHolder.Level2 = (int)score((float)gameTime.TotalGameTime.Subtract(beginLevel).TotalSeconds);
                message.SetText("Level Clear");
            }


            if(beginLevel.TotalMilliseconds == 0) beginLevel = gameTime.TotalGameTime;
            double startedSince = gameTime.TotalGameTime.Subtract(beginLevel).TotalSeconds;

            if (startedSince < 21d)
            {
                startedSince = Math.Round(startedSince, 1);
                if      (startedSince == 5d)  message.SetText("Move the branch");
                else if (startedSince == 10d) message.SetText("Small branch = ");
                else if (startedSince == 12d) message.SetText("BIG JUMP");
                else if (startedSince == 15d) message.SetText("Get Ready !");
                else if (startedSince == 18d) message.SetText("go");
                else if (startedSince == 20d) message.SetText("");
                
                message.Update(gameTime);
                hc.Update(gameTime, gameInput);
                activeBranch.Update(gameTime);
                return;
            }
            else hc = null;


            /* Update everything */
            squick.Update(gameTime);
            fadeEffect.Update(gameTime);
            gauge.Update(cameraOffset);
            message.Update(gameTime);
            activeBranch.Update(gameTime);
            oldBranches.RemoveAll(destroyable);
            foreach (Collectible i in items) i.Update(gameTime);
            foreach (Branch b in oldBranches) b.Update(gameTime);

            /* gravity & friction & bounce */
            squick.SpeedY += gravity * (float) gameTime.ElapsedGameTime.TotalSeconds;
            squick.SpeedX *= (float) Math.Pow(friction, gameTime.ElapsedGameTime.TotalSeconds);
            bounceAgainstWalls(squick);
            
            /* make squick jump on branches */
            processJump(gameInput, gameTime, activeBranch);
            foreach (Branch b in oldBranches) processJump(gameInput, gameTime, b);

            moveCamera(gameTime);

            if (squick.Speed.Y > 0 && squick.Pos.Y > 600) hitBottom();
            
            foreach (Collectible i in items)
                if(squick.Collide(i)){
                    int impact = i.GetBonus();
                    if (i is GoldenNut) nbOfLives++;
                    else if (i is Bomb) fadeEffect.Start(Fade.EFFECT.FADE_OUT, Color.White, 500.0f);
                    else squick.Speed = newSpeedItem(squick.Speed, impact);

                    squick.makeDizzy();
                    i.CollideWithPlayer(true);
                    i.Destroy();
                    toBeDestroy.Add(i);
                }

            if (toBeDestroy.Count > 0)
            {
                items.RemoveAll(toBeDestroy.Contains);
                toBeDestroy.Clear();
            }
            
        }

        private void hitBottom()
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

        private void moveCamera(GameTime gameTime)
        {

            /* squick is going near top screen */
            if (squick.Pos.Y < distanceToTop && squick.Speed.Y < 0)
            {

                var delta = squick.Pos.Y - distanceToTop;

                foreach (EntityFactory item in Level2CollectibleFactory.getSpawnBetween((int)cameraOffset, (int)(cameraOffset - delta)))
                {
                    Collectible i = (Collectible)item.asEntity();
                    i.MovementPattern = Collectible.MOVEMENT_NONE;
                    i.PosY -= cameraOffset;
                    i.Update(gameTime);
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

            }

        }

        private void processJump(KinectInterface gameInput, GameTime gameTime, Branch b)
        {
            var later = Vector2.Add(squick.Bottom, Vector2.Multiply(squick.Speed, impactTrigger));
            bool aboutToCross = b.isBelow(squick.Bottom) && b.isAbove(later);

            if (!aboutToCross) return;

            AudioManager.PlaySound(AudioManager.sound_jump);

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
            if (b.X < 40 && e.SpeedX < 0 || b.X + b.Width > 700 && e.SpeedX > 0)
            {
                AudioManager.PlaySound(AudioManager.sound_bounce);
                e.SpeedX = -e.SpeedX;

            }
        }

        public override void Render(GameTime gameTime)
        {

            Rectangle back1 = new Rectangle(0, (int) (cameraOffset % 2400f) - 2400, _levelBackground.Width, _levelBackground.Height);
            Rectangle back2 = new Rectangle(0, (int) (cameraOffset % 2400f), _levelBackground.Width, _levelBackground.Height);

            RenderManager.DrawBox(new Rectangle(0, 0, 800, 600), Color.SkyBlue);
            RenderManager.Draw2DTexture( _levelBackground, back1, Color.White);
            RenderManager.Draw2DTexture(_levelBackground, back2, Color.White);

            squick.Render(gameTime);
            if (!finished)
            {
                activeBranch.Render(gameTime);
                gauge.Render(gameTime);
                foreach (Branch b in oldBranches) b.Render(gameTime);
                foreach (Entity i in items) i.Render(gameTime);
                Rectangle bb = new Rectangle((int)gauge.Top.X - 15, (int)gauge.Top.Y - 15, 30, 30);
                RenderManager.Draw2DTexture(ResourceManager.tex_squick_headCut, bb, Color.White);
                for (int i = 0; i < nbOfLives; i++)
                {
                    Rectangle b = new Rectangle(660 - 40 * i, 10, 30, 30);
                    RenderManager.Draw2DTexture(ResourceManager.tex_squick_headCut, b, Color.White);
                }
            }

            RenderManager.DrawString(ResourceManager.font_score, cameraOffset.ToString("000 000"), new Vector2(70, 0), Color.Gold);
            message.Render(gameTime);
            if(hc != null) hc.Render(gameTime);
            fadeEffect.Render(gameTime);

        }




    }
}
