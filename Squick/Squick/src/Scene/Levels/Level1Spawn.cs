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
            new EntityFactory(2.0, EntityFactory.BONUS_NUT, 350),
            new EntityFactory(10.0, EntityFactory.BONUS_NUT, 150), 
            new EntityFactory(18.0, EntityFactory.BONUS_NUT, 550), 
            new EntityFactory(25.0, EntityFactory.BONUS_GOLDEN_NUT, 350), 
            // . Level 2
            /*
            new EntityFactory(3.5, EntityFactory.MALUS_PINE, 200),
            new EntityFactory(4.0, EntityFactory.BONUS_NUT, 300), 
           */
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