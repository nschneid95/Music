using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("NotesTests")]

namespace Notes
{
    class SineGenerator : Wave
    {

        private static IEnumerable<short> Generator(double frequency, double offset) {
            int amplitude = short.MaxValue;
            double timePeriod = (Math.PI * 2 * frequency) /
               (SAMPLE_RATE);
            for (uint index = 0; ; index++) {
                yield return Convert.ToInt16(amplitude *
                   Math.Sin(timePeriod * index + offset * Math.PI * 2 * frequency));
            }
        }

        // Offset should be in seconds
        public SineGenerator(double frequency, double offset = 0) : base(() => Generator(frequency, offset)) { }
    }
}
