using Microsoft.VisualStudio.TestTools.UnitTesting;
using Music;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Music.Tests
{
    [TestClass()]
    public class WaveHeaderTests
    {
        [TestMethod()]
        public void EncodingTest() {
            WaveHeader header = new WaveHeader();
            byte[] data = header.GetBytes();
            MemoryStream stream = new MemoryStream(data);
            WaveHeader parsed = WaveHeader.ExtractHeader(stream);
            Assert.AreEqual(header.FileTypeId, parsed.FileTypeId);
            Assert.AreEqual(header.FileLength, parsed.FileLength);
            Assert.AreEqual(header.MediaTypeId, parsed.MediaTypeId);
        }
    }
}