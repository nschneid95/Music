using System;
using System.Collections.Generic;

namespace Music
{
    public class SineGenerator : Wave
    {

        private static IEnumerable<short> Generator(double frequency) {
            int amplitude = short.MaxValue;
            double timePeriod = (Math.PI * 2 * frequency) /
               (SAMPLE_RATE);
            for (uint index = 0; ; index++) {
                yield return Convert.ToInt16(amplitude *
                   Math.Sin(timePeriod * index));
            }
        }

        public SineGenerator(double frequency) : base(() => Generator(frequency)) { }
    }
}
