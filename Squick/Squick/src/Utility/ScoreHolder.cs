using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Squick.src.Utility
{
    class ScoreHolder
    {
        private static int level1 = -1;
        private static int level2 = -1;

        public static bool hasLevel1
        {
            get { return level1 != -1; }
        }
        public static void clearLevel1()
        {
            level1 = -1;
        }
        public static int Level1
        {
            get { return level1; }
            set { level1 = value; }
        }

        public static bool hasLevel2
        {
            get { return level2 != -1; }
        }
        public static void clearLevel2()
        {
            level2 = -1;
        }
        public static int Level2
        {
            get { return level2; }
            set { level2 = value; }
        }

        public static int Total
        {
            get { return level1 + level2; }
        }
    }
}
