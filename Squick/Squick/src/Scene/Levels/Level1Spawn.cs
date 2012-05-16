using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Squick.src.Scene.Levels
{
    static class Level1Spawn
    {

        private static TimeSpan LastSpawn;
        private static TimeSpan baseTime;

        private static SpawnEntry[] spawn = new SpawnEntry[]{
            new SpawnEntry(7.5, SpawnEntry.nut, 300), /* spawn nut after 7.5s at X= 300*/
            new SpawnEntry(8.3, SpawnEntry.pine, 300)
        };

        public static List<SpawnEntry> getSpawnAt(GameTime gameTime){
            if (baseTime == null) throw new Exception("shoud call startNow");
            var spawnTime = gameTime.TotalGameTime.Subtract(baseTime);

            var query = 
                from n in spawn 
                where n.time <= spawnTime.TotalSeconds && n.time > LastSpawn.TotalSeconds
                select n as SpawnEntry;

            List<SpawnEntry> spawnNow = query.ToList<SpawnEntry>();

            return spawnNow;


            LastSpawn = gameTime.TotalGameTime;

        }

        public static void startNow(GameTime gameTime)
        {
            baseTime = gameTime.TotalGameTime;
            LastSpawn = baseTime;
        }
        
    }
}
