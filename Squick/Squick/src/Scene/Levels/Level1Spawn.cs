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

        private static EntityFactory[] spawn = new EntityFactory[]{

            // Tutorial waves
            // . Level1
            // ~ ReadySetGo (takes 5 seconds to appear)
            new EntityFactory(7.0, EntityFactory.BONUS_NUT, 350),  
            new EntityFactory(15.0, EntityFactory.BONUS_NUT, 150), 
            new EntityFactory(23.0, EntityFactory.BONUS_NUT, 550), 
            new EntityFactory(30.0, EntityFactory.BONUS_GOLDEN_NUT, 350), 
            // . Level 2
            // ~ /!\ Warning /!\
            new EntityFactory(35.0, EntityFactory.MALUS_PINE, 350),  

            new EntityFactory(42.0, EntityFactory.MALUS_PINE, 150), 
            new EntityFactory(42.0, EntityFactory.MALUS_PINE, 550), 

            new EntityFactory(48.0, EntityFactory.MALUS_PINE, 250),
            new EntityFactory(48.0, EntityFactory.MALUS_PINE, 450), 

            new EntityFactory(55.0, EntityFactory.MALUS_PINE, 150), 
            new EntityFactory(55.0, EntityFactory.BONUS_NUT, 350), 
            new EntityFactory(55.0, EntityFactory.MALUS_PINE, 550), 
            // . Level 2
            // ~ /!\ Speed-up /!\
            new EntityFactory(66.0, EntityFactory.BONUS_NUT, 100),  
            new EntityFactory(68.0, EntityFactory.BONUS_NUT, 150),  
            new EntityFactory(70.0, EntityFactory.BONUS_NUT, 200),  
            new EntityFactory(72.0, EntityFactory.BONUS_NUT, 250),  
            new EntityFactory(74.0, EntityFactory.BONUS_NUT, 300),  
            new EntityFactory(76.0, EntityFactory.BONUS_NUT, 350),

            new EntityFactory(80.0, EntityFactory.BONUS_NUT, 600),
            new EntityFactory(82.0, EntityFactory.BONUS_NUT, 550),
            new EntityFactory(84.0, EntityFactory.BONUS_NUT, 500),
            new EntityFactory(86.0, EntityFactory.BONUS_NUT, 450),
            new EntityFactory(88.0, EntityFactory.BONUS_NUT, 400),
            new EntityFactory(90.0, EntityFactory.BONUS_NUT, 350),

            new EntityFactory(95.0, EntityFactory.BONUS_GOLDEN_NUT, 150),
            new EntityFactory(95.0, EntityFactory.BONUS_GOLDEN_NUT, 550),
            new EntityFactory(97.0, EntityFactory.BONUS_GOLDEN_NUT, 350),

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

            //if(spawnNow.Count > 0) Console.Write("spawn "+spawnNow.Count+" items | base="+baseTime.TotalSeconds+" now ="+spawnTime.TotalSeconds);

            return spawnNow;
        }

        public static void startNow(GameTime gameTime)
        {
            baseTime = gameTime.TotalGameTime;
        }
        
    }
}