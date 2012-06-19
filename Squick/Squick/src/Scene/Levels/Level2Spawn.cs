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
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(100, 200)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(300, 800)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(500, 1400)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(650, 2000)),

            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(200, 2000)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(400, 2000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(500, 2800)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(300, 3400)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(100, 4000)), 
            
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(100, 5000)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 5000)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(500, 5000)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(700, 5000)),

            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(200, 5300)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(400, 5300)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(600, 5300)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(800, 5300)),

            new EntityFactory(0, EntityFactory.BONUS_GOLDEN_NUT, new Vector2(100, 5500)),
            
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(200, 6000)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(100, 6050)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 6050)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 6025)), 
            
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(500, 6500)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(600, 6550)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(700, 6550)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(100, 6525)), 

            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(500, 7000)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(600, 7050)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(700, 7050)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(100, 7025)), 

            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(500, 7500)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(600, 7550)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(700, 7550)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(100, 7525)), 

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(100, 8000)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 8300)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(300, 8600)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(400, 8900)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(500, 9200)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 9500)),

            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(100, 10000)), 
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(200, 10000)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(300, 10000)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(400, 10000)), 
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(500, 10000)),

            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(100, 11000)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 12000)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(600, 13000)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(100, 13000)),

            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(100, 14000)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 15000)),
            
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(600, 15800)),
            new EntityFactory(0, EntityFactory.BONUS_GOLDEN_NUT, new Vector2(600, 15900)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(550, 15900)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(600, 16000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(100, 16000)),
            
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(100, 17000)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 18000)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(600, 19000)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(100, 19000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(100, 20000)), 
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(300, 20000)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(500, 20000)),

            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(200, 22000)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(400, 22000)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(600, 22000)),

            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(400, 23000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(100, 24000)), 
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(300, 24000)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(500, 24000)),
            new EntityFactory(0, EntityFactory.BONUS_GOLDEN_NUT, new Vector2(650, 24000)),

            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(400, 25000)),

            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(200, 26000)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(400, 26000)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(600, 26000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(100, 26500)), 
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 26500)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(300, 26500)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(400, 26500)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(500, 26500)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 26500)),


            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(100, 27000)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 27050)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(100, 27100)),

            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(650, 27000)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(550, 27050)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(650, 27100)),

            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(400, 28000)), 
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(450, 28050)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(350, 28050)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(400, 28100)),
            new EntityFactory(0, EntityFactory.BONUS_GOLDEN_NUT, new Vector2(350, 28000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 28500)), 

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 29300)), 

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 30000)), 
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(400, 30000)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 30000)),

            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(200, 31000)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(200, 31500)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(200, 32000)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(200, 32500)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(200, 33000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(400, 31000)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(400, 32000)),
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(400, 33000)),

            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(600, 31000)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(600, 31500)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(600, 32000)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(600, 32500)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(600, 33000)),

            new EntityFactory(0, EntityFactory.BONUS_GOLDEN_NUT, new Vector2(100, 30500)),
            new EntityFactory(0, EntityFactory.BONUS_GOLDEN_NUT, new Vector2(650, 32500)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(100, 34000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(300, 35000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(500, 36000)),

            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(100, 37000)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(300, 37050)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(100, 37100)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(425, 37050)),

            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(650, 37000)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(550, 37050)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(650, 37100)),

            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(400, 38000)), 
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(450, 38050)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(350, 38050)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(400, 38100)),
            new EntityFactory(0, EntityFactory.BONUS_GOLDEN_NUT, new Vector2(350, 38000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 28500)), 
 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(200, 40000)), 
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(400, 40000)),
            new EntityFactory(0, EntityFactory.MALUS_PINE, new Vector2(600, 40000)),
            
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 41050)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 42050)),

            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(100, 43000)), 
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(200, 43000)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(300, 43000)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(400, 43000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 44000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 45000)),

            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(350, 46000)), 
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(450, 46000)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(550, 46000)),
            new EntityFactory(0, EntityFactory.MALUS_ANVIL, new Vector2(650, 46000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 47000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(600, 48000)),

            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(200, 49000)), 
            new EntityFactory(0, EntityFactory.BONUS_NUT, new Vector2(400, 49000)),
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