using Notes;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Notes.Tests
{
    [TestClass()]
    public class WaveTests
    {
        [TestMethod()]
        public void ScaleTestWithinBounds() {
            double frequency = 400;
            double numSecs = 1.0 / frequency;
            short[] bigWave = new SineGenerator(frequency).Sample(numSecs);
            short[] smallWave = Wave.Scale(new SineGenerator(frequency), 0.5f).Sample(numSecs);
            Assert.AreEqual(bigWave.Length, smallWave.Length);
            for (int i = 0; i < bigWave.Length; i++)
                Assert.AreEqual(bigWave[i] / 2, smallWave[i]);
        }

        [TestMethod()]
        public void ScaleTestBounds() {
            double frequency = 400;
            double numSecs = 1.0 / frequency;
            short[] smallWave = new SineGenerator(frequency).Sample(numSecs);
            short[] bigWave = Wave.Scale(new SineGenerator(frequency), 2f).Sample(numSecs);
            Assert.AreEqual(smallWave.Length, bigWave.Length);
            for (int i = 0; i < bigWave.Length; i++)
                Assert.IsTrue(Math.Abs((int)bigWave[i]) >= Math.Abs((int)smallWave[i]));
        }

        [TestMethod()]
        public void AddTestWithinBounds() {
            double frequency = 400;
            double numSecs = 1.0 / frequency;
            short[] wave = new SineGenerator(frequency).Sample(numSecs);
            short[] zero = Wave.Add(
                new List<WeightedWave> {
                    new WeightedWave{ wave = new SineGenerator(frequency), weight = 1},
                    new WeightedWave{ wave = new SineGenerator(frequency), weight = -1}
                }).Sample(numSecs);
            Assert.AreEqual(wave.Length, zero.Length);
            foreach (short val in zero)
                Assert.AreEqual(0, val);
        }

        [TestMethod()]
        public void AddTestBounds() {
            double frequency = 400;
            double numSecs = 1.0 / frequency;
            short[] wave = new SineGenerator(frequency).Sample(numSecs);
            short[] dbl = Wave.Add(
                new List<WeightedWave> {
                    new WeightedWave{ wave = new SineGenerator(frequency), weight = 1},
                    new WeightedWave{ wave = new SineGenerator(frequency), weight = 1}
                }).Sample(numSecs);
            for (int i = 0; i < wave.Length; i++)
                Assert.IsTrue(Math.Abs((int)wave[i]) <= Math.Abs((int)dbl[i]));
        }
        private class TestWave : Wave
        {
            public TestWave() : base(() => new List<short> { 1, 2, 3 }) { }
            public static uint SampleRate => SAMPLE_RATE;
        }

        [TestMethod()]
        public void SampleTest() {
            short[] arr = new TestWave().Sample(5.0 / TestWave.SampleRate);
            Assert.IsTrue(arr.Length > 3);
            Assert.AreEqual(1, arr[0]);
            Assert.AreEqual(2, arr[1]);
            Assert.AreEqual(3, arr[2]);
            for (int i = 3; i < arr.Length; i++)
                Assert.AreEqual(0, arr[i]);
        }
    }
}