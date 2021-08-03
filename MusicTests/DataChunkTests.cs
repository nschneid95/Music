using Microsoft.VisualStudio.TestTools.UnitTesting;
using Music;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Music.Tests
{
    [TestClass()]
    public class DataChunkTests
    {
        [TestMethod()]
        public void EncodingTest() {
            DataChunk chunk = new DataChunk();
            short[] left = new short[] { 1, 2, 3, 4, 5 };
            short[] right = new short[] { 2, 4, 6, 8, 10 };
            chunk.AddSampleData(left, right);
            DataChunk parsed = DataChunk.ExtractDataChunk(new MemoryStream(chunk.GetBytes()));
            Assert.AreEqual(chunk.ChunkId, parsed.ChunkId);
            Assert.AreEqual(chunk.ChunkSize, parsed.ChunkSize);
            Assert.AreEqual(chunk.WaveData.Length, parsed.WaveData.Length);
            for (int i = 0; i < chunk.WaveData.Length; i++)
                Assert.AreEqual(chunk.WaveData[i], parsed.WaveData[i]);
        }
    }
}