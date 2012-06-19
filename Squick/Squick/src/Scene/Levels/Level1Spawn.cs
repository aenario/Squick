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

        
        public const double WAVE_1 = 7.0f; // 7.0f
        public const double WAVE_2 = 27.0f;
        public const double WAVE_3 = 60.0f;
        public const double WAVE_4 = 93.0f;
        public const double WAVE_5 = 108.0f;
        public const double WAVE_6 = 158.0f;
        public const double WAVE_7 = 193.0f; // 173
        
        /* for debug fin level
        public const double WAVE_1 = 0.0f; // 7.0f
        public const double WAVE_2 = 07.0f;
        public const double WAVE_3 = 0.0f;
        public const double WAVE_4 = 0.0f;
        public const double WAVE_5 = 0.0f;
        public const double WAVE_6 = 0.0f;
        public const double WAVE_7 = 0.0f; // 173
        //public const double WAVE_8 = 0.0f;*/

        private const int SPEED_1 = 70;
        private const int SPEED_2 = 120;
        private const int SPEED_3 = 200;
        private const int SPEED_4 = 400;

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
            // ~ /!\ Warning /!\
            new EntityFactory(WAVE_4 + 0, EntityFactory.MALUS_ANVIL, 350,SPEED_2),
            new EntityFactory(WAVE_4 + 2, EntityFactory.MALUS_ANVIL, 150,SPEED_2),
            new EntityFactory(WAVE_4 + 4, EntityFactory.MALUS_ANVIL, 450,SPEED_2),
            new EntityFactory(WAVE_4 + 6, EntityFactory.MALUS_ANVIL, 250,SPEED_2),
            new EntityFactory(WAVE_4 + 8, EntityFactory.MALUS_ANVIL, 550,SPEED_2),

            // . Level 5
            // ~ /!\ Stay focused /!\

            // double wall
            new EntityFactory(WAVE_5 + 0, EntityFactory.BONUS_NUT, 100,SPEED_2),  
            new EntityFactory(WAVE_5 + 0, EntityFactory.BONUS_NUT, 200,SPEED_2),  
            new EntityFactory(WAVE_5 + 0, EntityFactory.BONUS_NUT, 300,SPEED_2),  
            new EntityFactory(WAVE_5 + 3, EntityFactory.MALUS_PINE, 100,SPEED_2),  
            new EntityFactory(WAVE_5 + 3, EntityFactory.MALUS_PINE, 200,SPEED_2),  
            new EntityFactory(WAVE_5 + 3, EntityFactory.MALUS_PINE, 300,SPEED_2),

            // double wall
            new EntityFactory(WAVE_5 + 5, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_5 + 5, EntityFactory.BONUS_NUT, 500,SPEED_2),  
            new EntityFactory(WAVE_5 + 5, EntityFactory.BONUS_NUT, 600,SPEED_2),  
            new EntityFactory(WAVE_5 + 8, EntityFactory.MALUS_PINE, 400,SPEED_2),  
            new EntityFactory(WAVE_5 + 8, EntityFactory.MALUS_PINE, 500,SPEED_2),  
            new EntityFactory(WAVE_5 + 8, EntityFactory.MALUS_PINE, 600,SPEED_2),

            // cross
            new EntityFactory(WAVE_5 + 10, EntityFactory.BONUS_NUT, 100,SPEED_2),  
            new EntityFactory(WAVE_5 + 10, EntityFactory.BONUS_NUT, 300,SPEED_2),
            new EntityFactory(WAVE_5 + 11, EntityFactory.MALUS_PINE, 200,SPEED_2),
            new EntityFactory(WAVE_5 + 12, EntityFactory.BONUS_NUT, 100,SPEED_2),  
            new EntityFactory(WAVE_5 + 12, EntityFactory.BONUS_NUT, 300,SPEED_2),

            // cross
            new EntityFactory(WAVE_5 + 13, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_5 + 13, EntityFactory.BONUS_NUT, 600,SPEED_2),
            new EntityFactory(WAVE_5 + 14, EntityFactory.MALUS_PINE, 500,SPEED_2),
            new EntityFactory(WAVE_5 + 15, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_5 + 15, EntityFactory.BONUS_NUT, 600,SPEED_2),

            // pine wall
            new EntityFactory(WAVE_5 + 16, EntityFactory.MALUS_PINE, 250,SPEED_2),
            new EntityFactory(WAVE_5 + 16, EntityFactory.MALUS_PINE, 300,SPEED_2),
            new EntityFactory(WAVE_5 + 16, EntityFactory.MALUS_PINE, 400,SPEED_2),
            new EntityFactory(WAVE_5 + 16, EntityFactory.MALUS_PINE, 450,SPEED_2),
            new EntityFactory(WAVE_5 + 17, EntityFactory.BONUS_GOLDEN_NUT, 100,SPEED_2),
            new EntityFactory(WAVE_5 + 17, EntityFactory.BONUS_GOLDEN_NUT, 600,SPEED_2),

            // Progressive ranges >
            new EntityFactory(WAVE_5 + 19, EntityFactory.BONUS_NUT, 100,SPEED_2),  
            new EntityFactory(WAVE_5 + 19, EntityFactory.BONUS_NUT, 200,SPEED_2),  
            new EntityFactory(WAVE_5 + 19, EntityFactory.BONUS_NUT, 300,SPEED_2),  
            new EntityFactory(WAVE_5 + 19, EntityFactory.MALUS_PINE, 400,SPEED_2), 
            new EntityFactory(WAVE_5 + 19, EntityFactory.MALUS_PINE, 500,SPEED_2), 
            new EntityFactory(WAVE_5 + 19, EntityFactory.MALUS_PINE, 600,SPEED_2), 

            new EntityFactory(WAVE_5 + 22, EntityFactory.BONUS_NUT, 200,SPEED_2),  
            new EntityFactory(WAVE_5 + 22, EntityFactory.BONUS_NUT, 300,SPEED_2),  
            new EntityFactory(WAVE_5 + 22, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_5 + 22, EntityFactory.MALUS_PINE, 100,SPEED_2), 
            new EntityFactory(WAVE_5 + 22, EntityFactory.MALUS_PINE, 500,SPEED_2), 
            new EntityFactory(WAVE_5 + 22, EntityFactory.MALUS_PINE, 600,SPEED_2), 

            new EntityFactory(WAVE_5 + 25, EntityFactory.BONUS_NUT, 300,SPEED_2),  
            new EntityFactory(WAVE_5 + 25, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_5 + 25, EntityFactory.BONUS_NUT, 500,SPEED_2),  
            new EntityFactory(WAVE_5 + 25, EntityFactory.MALUS_PINE, 100,SPEED_2), 
            new EntityFactory(WAVE_5 + 25, EntityFactory.MALUS_PINE, 200,SPEED_2), 
            new EntityFactory(WAVE_5 + 25, EntityFactory.MALUS_PINE, 600,SPEED_2), 

            new EntityFactory(WAVE_5 + 28, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_5 + 28, EntityFactory.BONUS_NUT, 500,SPEED_2),  
            new EntityFactory(WAVE_5 + 28, EntityFactory.BONUS_NUT, 600,SPEED_2),  
            new EntityFactory(WAVE_5 + 28, EntityFactory.MALUS_PINE, 100,SPEED_2), 
            new EntityFactory(WAVE_5 + 28, EntityFactory.MALUS_PINE, 200,SPEED_2), 
            new EntityFactory(WAVE_5 + 28, EntityFactory.MALUS_PINE, 300,SPEED_2), 

            // Double cross
            new EntityFactory(WAVE_5 + 31, EntityFactory.BONUS_NUT, 100,SPEED_2),  
            new EntityFactory(WAVE_5 + 31, EntityFactory.BONUS_NUT, 300,SPEED_2),
            new EntityFactory(WAVE_5 + 31, EntityFactory.MALUS_PINE, 200,SPEED_2),
            new EntityFactory(WAVE_5 + 31, EntityFactory.BONUS_NUT, 100,SPEED_2),  
            new EntityFactory(WAVE_5 + 31, EntityFactory.BONUS_NUT, 300,SPEED_2),
            new EntityFactory(WAVE_5 + 31, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_5 + 31, EntityFactory.BONUS_NUT, 600,SPEED_2),
            new EntityFactory(WAVE_5 + 31, EntityFactory.MALUS_PINE, 500,SPEED_2),
            new EntityFactory(WAVE_5 + 31, EntityFactory.BONUS_NUT, 400,SPEED_2),  
            new EntityFactory(WAVE_5 + 31, EntityFactory.BONUS_NUT, 600,SPEED_2),

            // consecutive balus!
            new EntityFactory(WAVE_5 + 34, EntityFactory.BONUS_NUT, 100,SPEED_2),
            new EntityFactory(WAVE_5 + 35, EntityFactory.MALUS_PINE, 100,SPEED_2),

            new EntityFactory(WAVE_5 + 35, EntityFactory.BONUS_NUT, 300,SPEED_2),
            new EntityFactory(WAVE_5 + 36, EntityFactory.MALUS_PINE, 300,SPEED_2),

            new EntityFactory(WAVE_5 + 36, EntityFactory.BONUS_NUT, 500,SPEED_2),
            new EntityFactory(WAVE_5 + 37, EntityFactory.MALUS_PINE, 500,SPEED_2),

            // pine wall
            new EntityFactory(WAVE_5 + 40, EntityFactory.MALUS_PINE, 250,SPEED_2),
            new EntityFactory(WAVE_5 + 40, EntityFactory.MALUS_PINE, 300,SPEED_2),
            new EntityFactory(WAVE_5 + 40, EntityFactory.MALUS_PINE, 400,SPEED_2),
            new EntityFactory(WAVE_5 + 40, EntityFactory.MALUS_PINE, 450,SPEED_2),
            new EntityFactory(WAVE_5 + 43, EntityFactory.BONUS_GOLDEN_NUT, 350,SPEED_2),
            new EntityFactory(WAVE_5 + 44, EntityFactory.BONUS_GOLDEN_NUT, 350,SPEED_2),

            // Level 6
            // ~ /!\ Warning /!\
            // Bomb trio
            new EntityFactory(WAVE_6 + 0, EntityFactory.MALUS_BOMB, 300,SPEED_2),
            new EntityFactory(WAVE_6 + 0, EntityFactory.MALUS_BOMB, 400,SPEED_2),
            new EntityFactory(WAVE_6 + 0.5, EntityFactory.MALUS_BOMB, 350,SPEED_2),

            // Bomb trio
            new EntityFactory(WAVE_6 + 5, EntityFactory.MALUS_BOMB, 50,SPEED_2),
            new EntityFactory(WAVE_6 + 5, EntityFactory.MALUS_BOMB, 150,SPEED_2),
            new EntityFactory(WAVE_6 + 5.5, EntityFactory.MALUS_BOMB, 100,SPEED_2),

            // Bomb trio
            new EntityFactory(WAVE_6 + 5, EntityFactory.MALUS_BOMB, 550,SPEED_2),
            new EntityFactory(WAVE_6 + 5, EntityFactory.MALUS_BOMB, 650,SPEED_2),
            new EntityFactory(WAVE_6 + 5.5, EntityFactory.MALUS_BOMB, 600,SPEED_2),

            new EntityFactory(WAVE_6 + 8.5, EntityFactory.BONUS_GOLDEN_NUT, 350,SPEED_2),

            // It's raining anvil again
            new EntityFactory(WAVE_6 + 12, EntityFactory.MALUS_ANVIL, 450,SPEED_2),
            new EntityFactory(WAVE_6 + 14, EntityFactory.MALUS_ANVIL, 250,SPEED_2),
            new EntityFactory(WAVE_6 + 18, EntityFactory.MALUS_ANVIL, 550,SPEED_2),
            new EntityFactory(WAVE_6 + 20, EntityFactory.MALUS_ANVIL, 150,SPEED_2),
            new EntityFactory(WAVE_6 + 24, EntityFactory.MALUS_ANVIL, 650,SPEED_2),
            new EntityFactory(WAVE_6 + 26, EntityFactory.MALUS_ANVIL, 50,SPEED_2),

            // Nut walls
            new EntityFactory(WAVE_6 + 15, EntityFactory.BONUS_NUT, 100,SPEED_2),
            new EntityFactory(WAVE_6 + 15, EntityFactory.BONUS_NUT, 200,SPEED_2),
            new EntityFactory(WAVE_6 + 15, EntityFactory.BONUS_NUT, 300,SPEED_2),
            new EntityFactory(WAVE_6 + 15, EntityFactory.BONUS_NUT, 400,SPEED_2),
            new EntityFactory(WAVE_6 + 15, EntityFactory.BONUS_NUT, 500,SPEED_2),
            new EntityFactory(WAVE_6 + 15, EntityFactory.BONUS_NUT, 600,SPEED_2),

            new EntityFactory(WAVE_6 + 15.5, EntityFactory.BONUS_NUT, 50,SPEED_2),
            new EntityFactory(WAVE_6 + 15.5, EntityFactory.BONUS_NUT, 150,SPEED_2),
            new EntityFactory(WAVE_6 + 15.5, EntityFactory.BONUS_NUT, 250,SPEED_2),
            new EntityFactory(WAVE_6 + 15.5, EntityFactory.BONUS_NUT, 350,SPEED_2),
            new EntityFactory(WAVE_6 + 15.5, EntityFactory.BONUS_NUT, 450,SPEED_2),
            new EntityFactory(WAVE_6 + 15.5, EntityFactory.BONUS_NUT, 550,SPEED_2),
            new EntityFactory(WAVE_6 + 15.5, EntityFactory.BONUS_NUT, 650,SPEED_2),

             // Nut walls
            new EntityFactory(WAVE_6 + 21, EntityFactory.BONUS_NUT, 100,SPEED_2),
            new EntityFactory(WAVE_6 + 21, EntityFactory.BONUS_NUT, 200,SPEED_2),
            new EntityFactory(WAVE_6 + 21, EntityFactory.BONUS_NUT, 300,SPEED_2),
            new EntityFactory(WAVE_6 + 21, EntityFactory.BONUS_NUT, 400,SPEED_2),
            new EntityFactory(WAVE_6 + 21, EntityFactory.BONUS_NUT, 500,SPEED_2),
            new EntityFactory(WAVE_6 + 21, EntityFactory.BONUS_NUT, 600,SPEED_2),

            new EntityFactory(WAVE_6 + 21.5, EntityFactory.BONUS_NUT, 50,SPEED_2),
            new EntityFactory(WAVE_6 + 21.5, EntityFactory.BONUS_NUT, 150,SPEED_2),
            new EntityFactory(WAVE_6 + 21.5, EntityFactory.BONUS_NUT, 250,SPEED_2),
            new EntityFactory(WAVE_6 + 21.5, EntityFactory.BONUS_NUT, 350,SPEED_2),
            new EntityFactory(WAVE_6 + 21.5, EntityFactory.BONUS_NUT, 450,SPEED_2),
            new EntityFactory(WAVE_6 + 21.5, EntityFactory.BONUS_NUT, 550,SPEED_2),
            new EntityFactory(WAVE_6 + 21.5, EntityFactory.BONUS_NUT, 650,SPEED_2),

            // Level 7 
            // ~ Speed up 

            // DNA
            new EntityFactory(WAVE_7 + 0, EntityFactory.BONUS_NUT, 50,SPEED_3),
            new EntityFactory(WAVE_7 + 0.5, EntityFactory.BONUS_NUT, 100,SPEED_3),
            new EntityFactory(WAVE_7 + 1, EntityFactory.BONUS_NUT, 175,SPEED_3),
            new EntityFactory(WAVE_7 + 1.5, EntityFactory.BONUS_NUT, 275,SPEED_3),
            new EntityFactory(WAVE_7 + 2, EntityFactory.BONUS_NUT, 425,SPEED_3),
            new EntityFactory(WAVE_7 + 2.5, EntityFactory.BONUS_NUT, 525,SPEED_3),
            new EntityFactory(WAVE_7 + 3, EntityFactory.BONUS_NUT, 600,SPEED_3),
            new EntityFactory(WAVE_7 + 3.5, EntityFactory.BONUS_NUT, 650,SPEED_3),
            new EntityFactory(WAVE_7 + 3.5, EntityFactory.BONUS_NUT, 50,SPEED_3),
            new EntityFactory(WAVE_7 + 3, EntityFactory.BONUS_NUT, 100,SPEED_3),
            new EntityFactory(WAVE_7 + 2.5, EntityFactory.BONUS_NUT, 175,SPEED_3),
            new EntityFactory(WAVE_7 + 2, EntityFactory.BONUS_NUT, 275,SPEED_3),
            new EntityFactory(WAVE_7 + 1.5, EntityFactory.BONUS_NUT, 425,SPEED_3),
            new EntityFactory(WAVE_7 + 1, EntityFactory.BONUS_NUT, 525,SPEED_3),
            new EntityFactory(WAVE_7 + 0.5, EntityFactory.BONUS_NUT, 600,SPEED_3),
            new EntityFactory(WAVE_7 + 0, EntityFactory.BONUS_NUT, 650,SPEED_3),
            new EntityFactory(WAVE_7 + 1.75, EntityFactory.BONUS_NUT, 350,SPEED_3),

             // Bomb trio
            new EntityFactory(WAVE_7 + 3.5, EntityFactory.MALUS_BOMB, 300,SPEED_3),
            new EntityFactory(WAVE_7 + 3.5, EntityFactory.MALUS_BOMB, 400,SPEED_3),
            new EntityFactory(WAVE_7 + 3.75, EntityFactory.MALUS_BOMB, 350,SPEED_3),

            // Progressive ranges bis
            new EntityFactory(WAVE_7 + 5, EntityFactory.BONUS_NUT, 400,SPEED_3),  
            new EntityFactory(WAVE_7 + 5, EntityFactory.BONUS_NUT, 200,SPEED_3),  
            new EntityFactory(WAVE_7 + 5, EntityFactory.BONUS_NUT, 300,SPEED_3),  
            new EntityFactory(WAVE_7 + 5, EntityFactory.BONUS_NUT, 500,SPEED_3),  
            new EntityFactory(WAVE_7 + 5, EntityFactory.MALUS_BOMB, 100,SPEED_3), 
            new EntityFactory(WAVE_7 + 5, EntityFactory.MALUS_BOMB, 600,SPEED_3), 

            new EntityFactory(WAVE_7 + 6, EntityFactory.BONUS_NUT, 100,SPEED_3),  
            new EntityFactory(WAVE_7 + 6, EntityFactory.BONUS_NUT, 200,SPEED_3), 
            new EntityFactory(WAVE_7 + 6, EntityFactory.BONUS_NUT, 500,SPEED_3),  
            new EntityFactory(WAVE_7 + 6, EntityFactory.BONUS_NUT, 600,SPEED_3),
            new EntityFactory(WAVE_7 + 6, EntityFactory.MALUS_BOMB, 350,SPEED_3), 

            // Anvil on the sides
            new EntityFactory(WAVE_7 + 8, EntityFactory.MALUS_ANVIL, 150,SPEED_3),
            new EntityFactory(WAVE_7 + 8, EntityFactory.MALUS_ANVIL, 550,SPEED_3),
           
            // Follow the line
            new EntityFactory(WAVE_7 + 11, EntityFactory.BONUS_NUT, 350,SPEED_4),
            new EntityFactory(WAVE_7 + 12, EntityFactory.BONUS_NUT, 250,SPEED_4),
            new EntityFactory(WAVE_7 + 13, EntityFactory.BONUS_NUT, 150,SPEED_4),
            new EntityFactory(WAVE_7 + 14, EntityFactory.BONUS_NUT, 50,SPEED_4),
            new EntityFactory(WAVE_7 + 15, EntityFactory.BONUS_NUT, 200,SPEED_4),
            new EntityFactory(WAVE_7 + 16, EntityFactory.BONUS_NUT, 300,SPEED_4),
            new EntityFactory(WAVE_7 + 17, EntityFactory.BONUS_NUT, 400,SPEED_4),
            new EntityFactory(WAVE_7 + 18, EntityFactory.BONUS_NUT, 500,SPEED_4),
            new EntityFactory(WAVE_7 + 19, EntityFactory.BONUS_NUT, 600,SPEED_4),
            new EntityFactory(WAVE_7 + 20, EntityFactory.BONUS_NUT, 200,SPEED_4),
            new EntityFactory(WAVE_7 + 21, EntityFactory.BONUS_NUT, 100,SPEED_4),
            new EntityFactory(WAVE_7 + 22, EntityFactory.BONUS_NUT, 200,SPEED_4),

            new EntityFactory(WAVE_7 + 21, EntityFactory.MALUS_ANVIL, 600,SPEED_3), // introducing shit
            new EntityFactory(WAVE_7 + 21.5, EntityFactory.BONUS_NUT, 400,SPEED_4),
            new EntityFactory(WAVE_7 + 22.5, EntityFactory.BONUS_NUT, 300,SPEED_4),
            new EntityFactory(WAVE_7 + 23.5, EntityFactory.BONUS_NUT, 400,SPEED_4),
            
            new EntityFactory(WAVE_7 + 22, EntityFactory.MALUS_ANVIL, 200,SPEED_3), // introducing shit
            new EntityFactory(WAVE_7 + 22, EntityFactory.BONUS_NUT, 500,SPEED_4),
            new EntityFactory(WAVE_7 + 23, EntityFactory.BONUS_NUT, 600,SPEED_4),
            new EntityFactory(WAVE_7 + 24, EntityFactory.BONUS_NUT, 500,SPEED_4),

            new EntityFactory(WAVE_7 + 24, EntityFactory.MALUS_ANVIL, 300,SPEED_3), // introducing shit

              // Nut walls
            new EntityFactory(WAVE_7 + 25, EntityFactory.BONUS_NUT, 100,SPEED_3),
            new EntityFactory(WAVE_7 + 25, EntityFactory.BONUS_NUT, 200,SPEED_3),
            new EntityFactory(WAVE_7 + 25, EntityFactory.BONUS_NUT, 300,SPEED_3),
            new EntityFactory(WAVE_7 + 25, EntityFactory.BONUS_NUT, 400,SPEED_3),
            new EntityFactory(WAVE_7 + 25, EntityFactory.BONUS_NUT, 500,SPEED_3),
            new EntityFactory(WAVE_7 + 25, EntityFactory.BONUS_NUT, 600,SPEED_3),

            new EntityFactory(WAVE_7 + 25.25, EntityFactory.BONUS_NUT, 50,SPEED_3),
            new EntityFactory(WAVE_7 + 25.25, EntityFactory.BONUS_NUT, 150,SPEED_3),
            new EntityFactory(WAVE_7 + 25.25, EntityFactory.BONUS_NUT, 250,SPEED_3),
            new EntityFactory(WAVE_7 + 25.25, EntityFactory.BONUS_NUT, 350,SPEED_3),
            new EntityFactory(WAVE_7 + 25.25, EntityFactory.BONUS_NUT, 450,SPEED_3),
            new EntityFactory(WAVE_7 + 25.25, EntityFactory.BONUS_NUT, 550,SPEED_3),
            new EntityFactory(WAVE_7 + 25.25, EntityFactory.BONUS_NUT, 650,SPEED_3),

            // consecutive balus!
            new EntityFactory(WAVE_7 + 27, EntityFactory.BONUS_NUT, 400,SPEED_3),
            new EntityFactory(WAVE_7 + 27.5, EntityFactory.MALUS_BOMB, 400,SPEED_3),

            new EntityFactory(WAVE_7 + 28, EntityFactory.BONUS_NUT, 200,SPEED_3),
            new EntityFactory(WAVE_7 + 28.5, EntityFactory.MALUS_BOMB, 200,SPEED_3),

            new EntityFactory(WAVE_7 + 29, EntityFactory.BONUS_NUT, 300,SPEED_3),
            new EntityFactory(WAVE_7 + 29.5, EntityFactory.MALUS_BOMB, 300,SPEED_3),

            new EntityFactory(WAVE_7 + 30, EntityFactory.BONUS_NUT, 100,SPEED_3),
            new EntityFactory(WAVE_7 + 30.5, EntityFactory.MALUS_BOMB, 100,SPEED_3),

            new EntityFactory(WAVE_7 + 31, EntityFactory.BONUS_NUT, 600,SPEED_3),
            new EntityFactory(WAVE_7 + 31.5, EntityFactory.MALUS_BOMB, 600,SPEED_3),

            // Still raining out there
            new EntityFactory(WAVE_7 + 32, EntityFactory.MALUS_BOMB, 350,SPEED_3),
            new EntityFactory(WAVE_7 + 33, EntityFactory.MALUS_BOMB, 50,SPEED_3),
            new EntityFactory(WAVE_7 + 34, EntityFactory.MALUS_BOMB, 450,SPEED_3),
            new EntityFactory(WAVE_7 + 35, EntityFactory.MALUS_BOMB, 250,SPEED_3),
            new EntityFactory(WAVE_7 + 36, EntityFactory.MALUS_BOMB, 550,SPEED_3),
            new EntityFactory(WAVE_7 + 37, EntityFactory.MALUS_BOMB, 100,SPEED_3),
            new EntityFactory(WAVE_7 + 38, EntityFactory.MALUS_BOMB, 300,SPEED_3),
            new EntityFactory(WAVE_7 + 39, EntityFactory.MALUS_BOMB, 650,SPEED_3),

              // Bouquet final
            new EntityFactory(WAVE_7 + 41, EntityFactory.BONUS_GOLDEN_NUT, 100,SPEED_3),
            new EntityFactory(WAVE_7 + 41, EntityFactory.BONUS_GOLDEN_NUT, 200,SPEED_3),
            new EntityFactory(WAVE_7 + 41, EntityFactory.BONUS_GOLDEN_NUT, 300,SPEED_3),
            new EntityFactory(WAVE_7 + 41, EntityFactory.BONUS_GOLDEN_NUT, 400,SPEED_3),
            new EntityFactory(WAVE_7 + 41, EntityFactory.BONUS_GOLDEN_NUT, 500,SPEED_3),
            new EntityFactory(WAVE_7 + 41, EntityFactory.BONUS_GOLDEN_NUT, 600,SPEED_3),

            new EntityFactory(WAVE_7 + 41.25, EntityFactory.BONUS_GOLDEN_NUT, 50,SPEED_3),
            new EntityFactory(WAVE_7 + 41.25, EntityFactory.BONUS_GOLDEN_NUT, 150,SPEED_3),
            new EntityFactory(WAVE_7 + 41.25, EntityFactory.BONUS_GOLDEN_NUT, 250,SPEED_3),
            new EntityFactory(WAVE_7 + 41.25, EntityFactory.BONUS_GOLDEN_NUT, 350,SPEED_3),
            new EntityFactory(WAVE_7 + 41.25, EntityFactory.BONUS_GOLDEN_NUT, 450,SPEED_3),
            new EntityFactory(WAVE_7 + 41.25, EntityFactory.BONUS_GOLDEN_NUT, 550,SPEED_3),
            new EntityFactory(WAVE_7 + 41.25, EntityFactory.BONUS_GOLDEN_NUT, 650,SPEED_3),
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