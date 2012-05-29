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
            new EntityFactory(3.0, EntityFactory.BONUS_NUT, 100), // spawn nut after 7.5s at X= 300
            new EntityFactory(3.5, EntityFactory.MALUS_PINE, 300),
            new EntityFactory(4.0, EntityFactory.BONUS_GOLDEN_NUT, 400), 
            new EntityFactory(4.5, EntityFactory.MALUS_PINE, 600),
            
            new EntityFactory(10.0, EntityFactory.BONUS_NUT, 200), 
            new EntityFactory(10.5, EntityFactory.MALUS_PINE, 400),
            new EntityFactory(11.0, EntityFactory.BONUS_NUT, 600),

            new EntityFactory(13.0, EntityFactory.BONUS_NUT, 100), 
            new EntityFactory(13.5, EntityFactory.MALUS_PINE, 250),
            new EntityFactory(13.0, EntityFactory.BONUS_NUT, 500),

            new EntityFactory(15.0, EntityFactory.BONUS_NUT, 400), 
            new EntityFactory(15.5, EntityFactory.MALUS_PINE, 550),
            new EntityFactory(15.0, EntityFactory.BONUS_NUT, 700),

            new EntityFactory(20.0, EntityFactory.BONUS_NUT, 300), 
            new EntityFactory(21.0, EntityFactory.MALUS_PINE, 400),
            new EntityFactory(20.0, EntityFactory.BONUS_NUT, 500),

            new EntityFactory(22.0, EntityFactory.BONUS_NUT, 100), 
            new EntityFactory(22.5, EntityFactory.MALUS_PINE, 250),
            new EntityFactory(22.0, EntityFactory.BONUS_NUT, 500),

            new EntityFactory(26.0, EntityFactory.BONUS_NUT, 400), 
            new EntityFactory(26.5, EntityFactory.MALUS_PINE, 550),
            new EntityFactory(26.0, EntityFactory.BONUS_NUT, 700),

            new EntityFactory(29.0, EntityFactory.BONUS_NUT, 300), 
            new EntityFactory(30.0, EntityFactory.MALUS_PINE, 400),
            new EntityFactory(29.0, EntityFactory.BONUS_NUT, 500),

            new EntityFactory(31.0, EntityFactory.BONUS_NUT, 100), 
            new EntityFactory(31.5, EntityFactory.MALUS_PINE, 250),
            new EntityFactory(31.0, EntityFactory.BONUS_NUT, 500),

            new EntityFactory(35.0, EntityFactory.BONUS_NUT, 400), 
            new EntityFactory(35.5, EntityFactory.MALUS_PINE, 550),
            new EntityFactory(35.0, EntityFactory.BONUS_NUT, 700),

            new EntityFactory(41.0, EntityFactory.BONUS_NUT, 300), 
            new EntityFactory(42.0, EntityFactory.MALUS_PINE, 400),
            new EntityFactory(41.0, EntityFactory.BONUS_NUT, 500),
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