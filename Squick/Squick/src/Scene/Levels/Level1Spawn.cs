using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Squick.Component;

namespace Squick.Scene.Levels
{
    static class Level1CollectibleFactory
    {

        private static double LastSpawn = 0;
        private static TimeSpan baseTime;
        private static double maxTime;

        public const double WAVE_1 = 7.0f;
        public const double WAVE_2 = 27.0f; 
        public const double WAVE_3 = 60.0f;
        public const double WAVE_4 = 93.0f;
        public const double WAVE_5 = 138.0f;

        private const int SPEED_1 = 70;
        private const int SPEED_2 = 120;
        private const int SPEED_3 = 200;

        private static EntityFactory[] spawn = new EntityFactory[]{

            // Tutorial waves
            // . Level1
            // ~ ReadySetGo (takes 5 seconds to appear)
            new EntityFactory(WAVE_1 + 0, EntityFactory.BONUS_NUT, 350,SPEED_1),  
            new EntityFactory(WAVE_1 + 5, EntityFactory.BONUS_NUT, 150,SPEED_1),
            new EntityFactory(WAVE_1 + 10, EntityFactory.BONUS_NUT, 550,SPEED_1), 
            new EntityFactory(WAVE_1 + 15, EntityFactory.BONUS_GOLDEN_NUT, 350,SPEED_1), 
            // . Level 2
            // ~ /!\ Warning /!\
            new EntityFactory(WAVE_2 + 0, EntityFactory.MALUS_PINE, 350,SPEED_1),  

            new EntityFactory(WAVE_2 + 7, EntityFactory.MALUS_PINE, 150,SPEED_1), 
            new EntityFactory(WAVE_2 + 7, EntityFactory.MALUS_PINE, 550,SPEED_1), 

            new EntityFactory(WAVE_2 + 13, EntityFactory.MALUS_PINE, 325,SPEED_1),
            new EntityFactory(WAVE_2 + 13, EntityFactory.MALUS_PINE, 375,SPEED_1), 

            new EntityFactory(WAVE_2 + 20, EntityFactory.MALUS_PINE, 150,SPEED_1), 
            new EntityFactory(WAVE_2 + 20, EntityFactory.BONUS_NUT, 350,SPEED_1), 
            new EntityFactory(WAVE_2 + 20, EntityFactory.MALUS_PINE, 550,SPEED_1), 
            // . Level 3
            // ~ /!\ Speed-up /!\
            new EntityFactory(WAVE_3 + 0, EntityFactory.BONUS_NUT, 100,SPEED_2),  
            new EntityFactory(WAVE_3 + 2, EntityFactory.BONUS_NUT, 150,SPEED_2),  
            new EntityFactory(WAVE_3 + 4, EntityFactory.BONUS_NUT, 200,SPEED_2),  
            new EntityFactory(WAVE_3 + 6, EntityFactory.BONUS_NUT, 250,SPEED_2),  
            new EntityFactory(WAVE_3 + 8, EntityFactory.BONUS_NUT, 300,SPEED_2),  
            new EntityFactory(WAVE_3 + 10, EntityFactory.BONUS_NUT, 350,SPEED_2),

            new EntityFactory(WAVE_3 + 14, EntityFactory.BONUS_NUT, 600,SPEED_2),
            new EntityFactory(WAVE_3 + 16, EntityFactory.BONUS_NUT, 550,SPEED_2),
            new EntityFactory(WAVE_3 + 18, EntityFactory.BONUS_NUT, 500,SPEED_2),
            new EntityFactory(WAVE_3 + 20, EntityFactory.BONUS_NUT, 450,SPEED_2),
            new EntityFactory(WAVE_3 + 22, EntityFactory.BONUS_NUT, 400,SPEED_2),
            new EntityFactory(WAVE_3 + 24, EntityFactory.BONUS_NUT, 350,SPEED_2),

            new EntityFactory(WAVE_3 + 25, EntityFactory.BONUS_GOLDEN_NUT, 150,SPEED_1),
            new EntityFactory(WAVE_3 + 25, EntityFactory.BONUS_GOLDEN_NUT, 550,SPEED_1),
            new EntityFactory(WAVE_3 + 25, EntityFactory.BONUS_GOLDEN_NUT, 350,SPEED_1),

            // . Level 4
            // ~ /!\ Stay focused /!\

            // double wall
            new EntityFactory(WAVE_4 + 0, EntityFactory.BONUS_NUT, 100,SPEED_2),  
            new EntityFactory(WAVE_4 + 0, EntityFactory.BONUS_NUT, 200,SPEED_2),  
            new EntityFactory(WAVE_4 + 0, EntityFactory.BONUS_NUT, 300,SPEED_2),  
            new EntityFactory(WAVE_4 + 3, EntityFactory.MALUS_PINE, 100,SPEED_2),  
            new EntityFactory(WAVE_4 + 3, EntityFactory.MALUS_PINE, 200,SPEED_2),  
            new EntityFactory(WAVE_4 + 3, EntityFactory.MALUS_PINE, 300,SPEED_2),

            // double wall
            new EntityFactory(WAVE_4 + 5, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_4 + 5, EntityFactory.BONUS_NUT, 500,SPEED_2),  
            new EntityFactory(WAVE_4 + 5, EntityFactory.BONUS_NUT, 600,SPEED_2),  
            new EntityFactory(WAVE_4 + 8, EntityFactory.MALUS_PINE, 400,SPEED_2),  
            new EntityFactory(WAVE_4 + 8, EntityFactory.MALUS_PINE, 500,SPEED_2),  
            new EntityFactory(WAVE_4 + 8, EntityFactory.MALUS_PINE, 600,SPEED_2),

            // cross
            new EntityFactory(WAVE_4 + 10, EntityFactory.BONUS_NUT, 100,SPEED_2),  
            new EntityFactory(WAVE_4 + 10, EntityFactory.BONUS_NUT, 300,SPEED_2),
            new EntityFactory(WAVE_4 + 11, EntityFactory.MALUS_PINE, 200,SPEED_2),
            new EntityFactory(WAVE_4 + 12, EntityFactory.BONUS_NUT, 100,SPEED_2),  
            new EntityFactory(WAVE_4 + 12, EntityFactory.BONUS_NUT, 300,SPEED_2),

            // cross
            new EntityFactory(WAVE_4 + 13, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_4 + 13, EntityFactory.BONUS_NUT, 600,SPEED_2),
            new EntityFactory(WAVE_4 + 14, EntityFactory.MALUS_PINE, 500,SPEED_2),
            new EntityFactory(WAVE_4 + 15, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_4 + 15, EntityFactory.BONUS_NUT, 600,SPEED_2),

            // pine wall
            new EntityFactory(WAVE_4 + 16, EntityFactory.MALUS_PINE, 250,SPEED_2),
            new EntityFactory(WAVE_4 + 16, EntityFactory.MALUS_PINE, 300,SPEED_2),
            new EntityFactory(WAVE_4 + 16, EntityFactory.MALUS_PINE, 400,SPEED_2),
            new EntityFactory(WAVE_4 + 16, EntityFactory.MALUS_PINE, 450,SPEED_2),
            new EntityFactory(WAVE_4 + 17, EntityFactory.BONUS_GOLDEN_NUT, 100,SPEED_2),
            new EntityFactory(WAVE_4 + 17, EntityFactory.BONUS_GOLDEN_NUT, 600,SPEED_2),

            // Progressive ranges >
            new EntityFactory(WAVE_4 + 19, EntityFactory.BONUS_NUT, 100,SPEED_2),  
            new EntityFactory(WAVE_4 + 19, EntityFactory.BONUS_NUT, 200,SPEED_2),  
            new EntityFactory(WAVE_4 + 19, EntityFactory.BONUS_NUT, 300,SPEED_2),  
            new EntityFactory(WAVE_4 + 19, EntityFactory.MALUS_PINE, 400,SPEED_2), 
            new EntityFactory(WAVE_4 + 19, EntityFactory.MALUS_PINE, 500,SPEED_2), 
            new EntityFactory(WAVE_4 + 19, EntityFactory.MALUS_PINE, 600,SPEED_2), 

            new EntityFactory(WAVE_4 + 21, EntityFactory.BONUS_NUT, 200,SPEED_2),  
            new EntityFactory(WAVE_4 + 21, EntityFactory.BONUS_NUT, 300,SPEED_2),  
            new EntityFactory(WAVE_4 + 21, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_4 + 21, EntityFactory.MALUS_PINE, 100,SPEED_2), 
            new EntityFactory(WAVE_4 + 21, EntityFactory.MALUS_PINE, 500,SPEED_2), 
            new EntityFactory(WAVE_4 + 21, EntityFactory.MALUS_PINE, 600,SPEED_2), 

            new EntityFactory(WAVE_4 + 23, EntityFactory.BONUS_NUT, 300,SPEED_2),  
            new EntityFactory(WAVE_4 + 23, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_4 + 23, EntityFactory.BONUS_NUT, 500,SPEED_2),  
            new EntityFactory(WAVE_4 + 23, EntityFactory.MALUS_PINE, 100,SPEED_2), 
            new EntityFactory(WAVE_4 + 23, EntityFactory.MALUS_PINE, 200,SPEED_2), 
            new EntityFactory(WAVE_4 + 23, EntityFactory.MALUS_PINE, 600,SPEED_2), 

            new EntityFactory(WAVE_4 + 25, EntityFactory.BONUS_NUT, 300,SPEED_2),  
            new EntityFactory(WAVE_4 + 25, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_4 + 25, EntityFactory.BONUS_NUT, 500,SPEED_2),  
            new EntityFactory(WAVE_4 + 25, EntityFactory.MALUS_PINE, 100,SPEED_2), 
            new EntityFactory(WAVE_4 + 25, EntityFactory.MALUS_PINE, 200,SPEED_2), 
            new EntityFactory(WAVE_4 + 25, EntityFactory.MALUS_PINE, 600,SPEED_2), 

            new EntityFactory(WAVE_4 + 25, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_4 + 25, EntityFactory.BONUS_NUT, 500,SPEED_2),  
            new EntityFactory(WAVE_4 + 25, EntityFactory.BONUS_NUT, 600,SPEED_2),  
            new EntityFactory(WAVE_4 + 25, EntityFactory.MALUS_PINE, 100,SPEED_2), 
            new EntityFactory(WAVE_4 + 25, EntityFactory.MALUS_PINE, 200,SPEED_2), 
            new EntityFactory(WAVE_4 + 25, EntityFactory.MALUS_PINE, 300,SPEED_2), 

            // Double cross
            new EntityFactory(WAVE_4 + 29, EntityFactory.BONUS_NUT, 100,SPEED_2),  
            new EntityFactory(WAVE_4 + 29, EntityFactory.BONUS_NUT, 300,SPEED_2),
            new EntityFactory(WAVE_4 + 29, EntityFactory.MALUS_PINE, 200,SPEED_2),
            new EntityFactory(WAVE_4 + 29, EntityFactory.BONUS_NUT, 100,SPEED_2),  
            new EntityFactory(WAVE_4 + 29, EntityFactory.BONUS_NUT, 300,SPEED_2),
            new EntityFactory(WAVE_4 + 29, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_4 + 29, EntityFactory.BONUS_NUT, 600,SPEED_2),
            new EntityFactory(WAVE_4 + 29, EntityFactory.MALUS_PINE, 500,SPEED_2),
            new EntityFactory(WAVE_4 + 29, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_4 + 29, EntityFactory.BONUS_NUT, 600,SPEED_2),

            // consecutive balus!
            new EntityFactory(WAVE_4 + 33, EntityFactory.BONUS_NUT, 100,SPEED_2),
            new EntityFactory(WAVE_4 + 34, EntityFactory.MALUS_PINE, 100,SPEED_2),

            new EntityFactory(WAVE_4 + 34, EntityFactory.BONUS_NUT, 300,SPEED_2),
            new EntityFactory(WAVE_4 + 35, EntityFactory.MALUS_PINE, 300,SPEED_2),

            new EntityFactory(WAVE_4 + 35, EntityFactory.BONUS_NUT, 500,SPEED_2),
            new EntityFactory(WAVE_4 + 36, EntityFactory.MALUS_PINE, 500,SPEED_2),

            // pine wall
            new EntityFactory(WAVE_4 + 38, EntityFactory.MALUS_PINE, 250,SPEED_2),
            new EntityFactory(WAVE_4 + 38, EntityFactory.MALUS_PINE, 300,SPEED_2),
            new EntityFactory(WAVE_4 + 38, EntityFactory.MALUS_PINE, 400,SPEED_2),
            new EntityFactory(WAVE_4 + 38, EntityFactory.MALUS_PINE, 450,SPEED_2),
            new EntityFactory(WAVE_4 + 41, EntityFactory.BONUS_GOLDEN_NUT, 350,SPEED_2),
            new EntityFactory(WAVE_4 + 42, EntityFactory.BONUS_GOLDEN_NUT, 350,SPEED_2),

            // Wave 5
            // ~ /!\ Warning /!\

        };

        public static List<EntityFactory> getSpawnAt(GameTime gameTime){
            if (baseTime == null) throw new Exception("shoud call startNow");
            var spawnTime = gameTime.TotalGameTime.Subtract(baseTime);

            var query = 
                from n in spawn 
                where n.time <= spawnTime.TotalSeconds && n.time > LastSpawn
                select n;

            List<EntityFactory> spawnNow = query.ToList<EntityFactory>();
            LastSpawn = spawnTime.TotalSeconds;

            return spawnNow;
        }

        public static double doneSince(GameTime gameTime)
        {
            return gameTime.TotalGameTime.Subtract(baseTime).TotalSeconds - maxTime;
            
        }

        public static void startNow(GameTime gameTime)
        {
            baseTime = gameTime.TotalGameTime;
            maxTime = (from n in spawn
                        select n.time).Max();
        }
        
    }
}