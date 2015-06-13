using System;
using System.Collections.Generic;
using System.Text;

namespace spaceattack.Helpers
{
    class RandomHelper
    {
        private static Random r;

        static RandomHelper()
        {
            r = new Random();
        }

        public static int Next(int max)
        {
            return r.Next(max);
        }
        public static int Next(int min, int max)
        {
            return r.Next(min, max);
        }
        public static float NextFloat()
        {
            return (float)r.NextDouble();
        }
    }
}
