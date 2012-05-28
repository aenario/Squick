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

namespace Squick.Scene
{
    public class Level2 : Scene{

        private static Vector2 gravity = new Vector2(0, 3);
        

        private Texture2D _levelBackground;
        private JumpingSquick squick;
        private Branch activeBranch;
        private List<Branch> oldBranches;

        private float cameraOffset;

        private bool wasAbove;

        private Vector2 squickBottom;

        //private Vector2 cameraPos;

        //private Player _squick;

        public Level2(KinectInterface gameInput)
        {
            _levelBackground = ResourceManager.tex_background_level2;
            squick = new JumpingSquick(gameInput);
            squick.Pos = new Vector2(100, 100);
            squick.Speed = new Vector2(10,0);

            activeBranch = new Branch(gameInput);
            oldBranches = new List<Branch>();
            wasAbove = true;    
        }

        public override void Update(GameTime gameTime, KinectInterface gameInput)
        {

            activeBranch.Update(gameTime);
            squick.Update(gameTime);

                        
            squickBottom = Vector2.Add(squick.Pos, new Vector2(squick.Width/2, 0.9f*squick.Height));
            bool aboutToCross = wasAbove && activeBranch.isAbove(squickBottom);
            wasAbove = activeBranch.isBelow(squickBottom);
            
            if(aboutToCross){

                /* après 3h à se plonger dans les affres de la trigo,
                 * découvrir que c'est faisable en une seule goddamn ligne...
                 * Les joies du C#*/
                squick.Speed = Vector2.Reflect(squick.Speed, activeBranch.Normal);
                //var Ysp = squick.Speed.Y;
                //if(Ysp <0) Ysp = MathHelper.Clamp(Ysp, , -400);
                
                activeBranch.Fix();
                oldBranches.Add(activeBranch);
                activeBranch = new Branch(gameInput);
                activeBranch.Update(gameTime);

            }

            if (squick.Pos.Y < 0 && squick.Speed.Y < 0) // squick monte
            {
                
                var delta = squick.Pos.Y;
                cameraOffset -= delta;
                squick.Pos = new Vector2(squick.Pos.X, 0);
                foreach (Branch b in oldBranches)
                {
                    b.Pos = new Vector2(b.Pos.X, b.Pos.Y - delta);
                }
                oldBranches.RemoveAll(delegate(Branch b) { return b.Pos.Y > 1200; });
            }

            

            if (squick.Pos.X < 40 || squick.Pos.X > 600) squick.Speed = new Vector2(-squick.Speed.X, squick.Speed.Y);

            // DEBUG ONLY
            if(squick.Pos.Y > 600) squick.Pos = new Vector2(squick.Pos.X, 0);
            
        }

        public override void Render(GameTime gameTime)
        {

            Rectangle back1 = new Rectangle(0, (int) (cameraOffset % 600f) - 600, _levelBackground.Width, _levelBackground.Height);
            Rectangle back2 = new Rectangle(0, (int) (cameraOffset % 600f), _levelBackground.Width, _levelBackground.Height);

            RenderManager.Draw2DTexture( _levelBackground, back1, Color.White);
            RenderManager.Draw2DTexture(_levelBackground, back2, Color.White);
            squick.Render(gameTime);
            activeBranch.Render(gameTime);
            RenderManager.DrawBox(new Rectangle((int) squickBottom.X, (int) squickBottom.Y, 3, 3));

            foreach(Branch b in oldBranches) b.Render(gameTime);
            //RenderManager.DrawLine(leftHandPos, rightHandPos);

        }




    }
}
