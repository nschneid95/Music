using System;
using System.Collections.Generic;
using System.Text;

namespace Music
{
    public static class Note
    {
        private static double Weight(uint index) {
            return index switch {
                1 => 74003754,
                2 => 67930932,
                3 => -30810271,
                4 => 17806237,
                5 => -7712146,
                _ => 0,
            };
            //return Math.Pow(0.25, index);
        }

        // TODO: Add a test for this method!!
        public static Wave BuildNote(double frequency, uint numOvertones) {
            List<WeightedWave> waves = new List<WeightedWave>();
            // Make sure the weights add up to 1
            double weightSum = 0;
            for (uint i = 1; i <= numOvertones + 1; i++)
                weightSum += Weight(i);
            for (uint i = 1; i <= numOvertones + 1; i++)
                waves.Add(new WeightedWave { wave = new SineGenerator(frequency * i), weight = Weight(i) / weightSum });
            return Wave.Add(waves);
        }
    }
}
