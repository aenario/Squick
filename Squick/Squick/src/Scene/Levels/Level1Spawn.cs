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

        private const double WAVE_1 = 7.0f;
        private const double WAVE_2 = 35.0f;
        private const double WAVE_3 = 68.0f;

        private const int SPEED_1 = 50;
        private const int SPEED_2 = 100;
        private const int SPEED_3 = 200;

        private static EntityFactory[] spawn = new EntityFactory[]{

            // Tutorial waves
            // . Level1
            // ~ ReadySetGo (takes 5 seconds to appear)
            new EntityFactory(WAVE_1 + 0, EntityFactory.BONUS_NUT, 350,SPEED_1),  
            new EntityFactory(WAVE_1 + 8, EntityFactory.BONUS_NUT, 150,SPEED_1), 
            new EntityFactory(WAVE_1 + 16, EntityFactory.BONUS_NUT, 550,SPEED_1), 
            new EntityFactory(WAVE_1 + 24, EntityFactory.BONUS_GOLDEN_NUT, 350,SPEED_1), 
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
            // . Level 2
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

            new EntityFactory(WAVE_3 + 30, EntityFactory.BONUS_GOLDEN_NUT, 150,SPEED_1),
            new EntityFactory(WAVE_3 + 30, EntityFactory.BONUS_GOLDEN_NUT, 550,SPEED_1),
            new EntityFactory(WAVE_3 + 32, EntityFactory.BONUS_GOLDEN_NUT, 350,SPEED_1),

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

        public static bool done(GameTime gameTime)
        {
            return maxTime < gameTime.TotalGameTime.Subtract(baseTime).TotalSeconds;
            
        }

        public static void startNow(GameTime gameTime)
        {
            baseTime = gameTime.TotalGameTime;
            maxTime = (from n in spawn
                        select n.time).Max();
        }
        
    }
}