using System;
using System.Collections.Generic;
using System.Text;

namespace Music
{
    public static class Note
    {
        private static double Weight(uint index) {
            return index switch {
                1 => 7000,
                2 => 3000,
                3 => 450,
                4 => 4,
                5 => 1.5,
                _ => 0,
            };
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
