using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Squick.Utility;
/*
namespace Squick.Component
{
    class EntityFactory
    {
        public static int nut = 1;
        public static int pine = 2;

        private static float defaultY = -50f;

        public int type;
        public double time;
        public Vector2 pos;

        public EntityFactory(double time, int type, Vector2 pos)
        {
            this.time = time;
            this.type = type;
            this.pos = pos;
        }

        // CHECK IT OUT 
        public Entity asEntity()
        {
            Entity e;
                if (this.type == nut)
                {
                    e = new SimpleEntity(ResourceManager.tex_nut);
                }
                else if (this.type == pine)
                {
                    e = new SimpleEntity(ResourceManager.tex_pine);
                }
                else
                {
                    e = new SimpleEntity(ResourceManager.tex_squick_rightArm);
                }

            
                e.Pos = pos;
                Console.WriteLine("creating " + type + " at (" + e.Pos.X + ";" + e.Pos.Y + ")");

     
            return e;
        }
        

        public EntityFactory(double time, int type, int X) : this(time, type, new Vector2(X, defaultY)) { }
        public EntityFactory(double time, int type) : this(time, type, new Random().Next()) { }

    }
}
*/