using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Squick.Utility;

using Squick.Component.Collectible;

namespace Squick.Component
{
    class EntityFactory
    {
        public static int BONUS_NUT = 0;
        public static int BONUS_GOLDEN_NUT = 1;
        public static int MALUS_PINE = 2;
        public static int MALUS_ANVIL = 3;
        public static int MALUS_BOMB = 4;

        private static float defaultY = -50f;
        private const int defaultGravity = 50;

        public int type;
        public double time;
        public Vector2 pos;
        public int speed;

        public EntityFactory(double time, int type, Vector2 pos, int speed = defaultGravity)
        {
            this.time = time;
            this.type = type;
            this.pos = pos;
            this.speed = speed;
        }

        // CHECK IT OUT 
        public Entity asEntity()
        {
            Entity e;
                if (this.type == BONUS_NUT)
                {
                    e = new Nut();
                }
                else if (this.type == BONUS_GOLDEN_NUT)
                {
                    e = new GoldenNut();
                }
                else if (this.type == MALUS_PINE)
                {
                    e = new Pine();
                }
                else if (this.type == MALUS_ANVIL)
                {
                    e = new Anvil();
                }
                else if (this.type == MALUS_BOMB)
                {
                    e = new Bomb();
                }
                else // Certainly a syntax error
                {
                    e = new Nut();
                }

                e.Speed = new Vector2(0, speed);
                e.Pos = pos;
                Console.WriteLine("creating " + type + " at (" + e.Pos.X + ";" + e.Pos.Y + ") at speed " + speed);

     
            return e;
        }


        public EntityFactory(double time, int type, int X, int speed = defaultGravity) : this(time, type, new Vector2(X, defaultY), speed) { }
        public EntityFactory(double time, int type, int speed = defaultGravity) : this(time, type, new Random().Next(), speed) { }

    }
}
