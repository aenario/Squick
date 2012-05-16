using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Squick.src.Scene.Levels
{
    static class Level1Spawn
    {

        private static double LastSpawn = 0;
        private static TimeSpan baseTime;

        private static SpawnEntry[] spawn = new SpawnEntry[]{
            new SpawnEntry(3.0, SpawnEntry.nut, 100), /* spawn nut after 7.5s at X= 300*/
            new SpawnEntry(3.5, SpawnEntry.pine, 200),
            new SpawnEntry(4.0, SpawnEntry.nut, 300), 
            new SpawnEntry(4.5, SpawnEntry.pine, 400),
            new SpawnEntry(5.0, SpawnEntry.nut, 500),
        };

        public static List<SpawnEntry> getSpawnAt(GameTime gameTime){
            if (baseTime == null) throw new Exception("shoud call startNow");
            var spawnTime = gameTime.TotalGameTime.Subtract(baseTime);

            var query = 
                from n in spawn 
                where n.time <= spawnTime.TotalSeconds && n.time > LastSpawn
                select n;

            List<SpawnEntry> spawnNow = query.ToList<SpawnEntry>();
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
