using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Notes.Tests
{
    [TestClass()]
    public class FormatChunkTests
    {
        [TestMethod()]
        public void EncodingTest() {
            FormatChunk chunk = new FormatChunk();
            FormatChunk parsed = FormatChunk.ExtractFormatChunk(new MemoryStream(chunk.GetBytes()));
            Assert.AreEqual(chunk.AverageBytesPerSec, parsed.AverageBytesPerSec);
            Assert.AreEqual(chunk.BitsPerSample, parsed.BitsPerSample);
            Assert.AreEqual(chunk.BlockAlign, parsed.BlockAlign);
            Assert.AreEqual(chunk.Channels, parsed.Channels);
            Assert.AreEqual(chunk.ChunkId, parsed.ChunkId);
            Assert.AreEqual(chunk.ChunkSize, parsed.ChunkSize);
            Assert.AreEqual(chunk.FormatTag, parsed.FormatTag);
            Assert.AreEqual(chunk.Frequency, parsed.Frequency);
        }
    }
}