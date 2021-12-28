using System;
using System.Collections.Generic;

namespace Notes
{
    public class Wave
    {
        public const uint SAMPLE_RATE = 44100;

        protected delegate IEnumerable<short> GenerateValues();
        private readonly GenerateValues generator;

        public IEnumerable<short> Values => generator();

        protected Wave(GenerateValues generator) {
            this.generator = generator;
        }

        private static IEnumerable<short> ScaleGenerator(GenerateValues base_generator, double scalar) {
            foreach (short val in base_generator())
                yield return ToShort(val * scalar);
        }


        /// <summary>
        /// Scales the wave by the given scalar. Note that the outputs are cast to shorts!
        /// </summary>
        /// <param name="wave">The wave to be scaled.</param>
        /// <param name="scalar">The value to scale by.</param>
        /// <returns>The input wave scaled by the scalar.</returns>
        public static Wave Scale(Wave wave, double scalar) {
            return new Wave(() => ScaleGenerator(wave.generator, scalar));
        }

        // Casts the double to a short but handles overflow properly
        private static short ToShort(double val) {
            if (val > short.MaxValue)
                return short.MaxValue;
            if (val < short.MinValue)
                return short.MinValue;
            return (short)val;
        }

        private static IEnumerable<short> AddGenerators(IEnumerable<WeightedWave> weightedWaves) {
            var weightedGenerators = new List<Tuple<IEnumerator<short>, double>>();
            foreach (WeightedWave weightedWave in weightedWaves) {
                if (weightedWave.weight != 0)
                    weightedGenerators.Add(Tuple.Create(weightedWave.wave.generator().GetEnumerator(), weightedWave.weight));
            }

            while (weightedGenerators.Count > 0) {
                double val = 0;
                for (int i = weightedGenerators.Count - 1; i >= 0; i--) {
                    IEnumerator<short> gen = weightedGenerators[i].Item1;
                    double weight = weightedGenerators[i].Item2;
                    // Remove component waves when they are exhausted
                    if (!gen.MoveNext())
                        weightedGenerators.RemoveAt(i);
                    val += gen.Current * weight;
                }
                yield return ToShort(val);
            }
        }

        /// <summary>
        /// Adds a collection of weighted waves together.
        /// </summary>
        /// <param name="weightedWaves">A list of weighted waves</param>
        /// <returns>The summed wave</returns>
        public static Wave Add(IEnumerable<WeightedWave> weightedWaves) {
            return new Wave(() => AddGenerators(weightedWaves));
        }

        private static IEnumerable<short> MultiplyGenerators(GenerateValues gen1, GenerateValues gen2) {
            IEnumerator<short> iter1 = gen1().GetEnumerator();
            IEnumerator<short> iter2 = gen2().GetEnumerator();
            while (iter1.MoveNext() || iter2.MoveNext())
                yield return ToShort(iter1.Current * iter2.Current);
        }

        /// <summary>
        /// Multiplies two waves together pointwise.
        /// </summary>
        /// <param name="a">The first wave.</param>
        /// <param name="b">The second wave.</param>
        /// <returns>The product wave.</returns>
        public static Wave Multiply(Wave a, Wave b) {
            return new Wave(() => MultiplyGenerators(a.generator, b.generator));
        }

        /// <summary>
        /// Samples the wave for the given amount of time. This should only be called once per wave.
        /// </summary>
        /// <param name="seconds">How long to sample the wave for.</param>
        /// <returns>A buffer containing the sample.</returns>
        public short[] Sample(double seconds) {
            uint bufferSize = (uint)(SAMPLE_RATE * seconds);
            short[] ret = new short[bufferSize];
            IEnumerator<short> iter = generator().GetEnumerator();
            for (int i = 0; i < bufferSize; i++) {
                if (!iter.MoveNext())
                    break;
                ret[i] = iter.Current;
            }
            return ret;
        }
    }

    public struct WeightedWave
    {
        public Wave wave;
        public double weight;
    }
}
