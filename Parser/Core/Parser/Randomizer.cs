using System;

namespace Parser.Core.Parser
{
    class Randomizer
    {
        private static Random rand = new();
        public static int RandomInt(int min, int max)
        {
            int randomInt = rand.Next(min, max);
            return randomInt;
        }
    }
}
