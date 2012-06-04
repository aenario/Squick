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
            new EntityFactory(3.0, EntityFactory.BONUS_NUT, new Vector2(100, 200)), // spawn nut after 7.5s at X= 300
            new EntityFactory(3.5, EntityFactory.MALUS_PINE, new Vector2(300, 1200)),
            new EntityFactory(4.0, EntityFactory.BONUS_GOLDEN_NUT, new Vector2(500, 3200)), 
            new EntityFactory(4.5, EntityFactory.MALUS_PINE, new Vector2(700, 5200)),
            
            new EntityFactory(10.0, EntityFactory.BONUS_NUT, new Vector2(200, 8200)), 
            new EntityFactory(10.5, EntityFactory.MALUS_PINE, new Vector2(300, 7200)),
            new EntityFactory(11.0, EntityFactory.BONUS_NUT, new Vector2(600, 6200)),

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