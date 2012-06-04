using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

using Squick.Component;

namespace Squick.Scene.Levels
{
    static class Level2CollectibleFactory
    {

        private static EntityFactory[] spawn = new EntityFactory[]{
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(100, 200)), // spawn nut after 7.5s at X= 300
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 1200)),
            new EntityFactory(0, EntityFactory.BONUS_GOLDEN_NUT, new Vector2(500, 3200)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(700, 5200)),
            
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 8200)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 7200)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 6200)),

            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(200, 10000)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 10000)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(600, 10000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 18200)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 17200)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 16200)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 20000)), 
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(300, 20000)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 20000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 28200)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 27200)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 26200)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 30000)), 
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(300, 30000)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 30000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 38200)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 37200)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 36200)),

            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(200, 40000)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 40000)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(600, 40000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 48200)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 47200)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 46200)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 47000)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 47500)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 48000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 49000)), 
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(300, 49000)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 49000)),

        };


        public static List<EntityFactory> getSpawnBetween(int minHeight, int maxHeight)
        {
             var query = 
                from n in spawn
                where n.pos.Y <= maxHeight && n.pos.Y > minHeight
                select n;

            List<EntityFactory> spawnNow = query.ToList<EntityFactory>();
            return spawnNow;
        }
        
    }
}