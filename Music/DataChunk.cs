using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Music
{
    public class DataChunk
    {
        private const string CHUNK_ID = "data";

        public string ChunkId { get; private set; }
        public uint ChunkSize { get; set; }
        public short[] WaveData { get; private set; }

        public DataChunk() {
            ChunkId = CHUNK_ID;
            ChunkSize = 0;  // Until we add some data
        }

        public uint Length() {
            return (uint)GetBytes().Length;
        }

        public byte[] GetBytes() {
            List<byte> chunkBytes = new List<byte>();

            chunkBytes.AddRange(Encoding.ASCII.GetBytes(ChunkId));
            chunkBytes.AddRange(BitConverter.GetBytes(ChunkSize));
            byte[] bufferBytes = new byte[WaveData.Length * 2];
            Buffer.BlockCopy(WaveData, 0, bufferBytes, 0,
               bufferBytes.Length);
            chunkBytes.AddRange(bufferBytes.ToList());

            return chunkBytes.ToArray();
        }

        public static DataChunk ExtractDataChunk(Stream stream) {
            DataChunk chunk = new DataChunk();
            byte[] buffer = new byte[4];
            if (stream.Read(buffer) != 4) throw new ArgumentOutOfRangeException("Reached end of file while parsing data chunk");
            chunk.ChunkId = Encoding.ASCII.GetString(buffer);
            if (stream.Read(buffer) != 4) throw new ArgumentOutOfRangeException("Reached end of file while parsing data chunk");
            chunk.ChunkSize = BitConverter.ToUInt32(buffer, 0);
            buffer = new byte[chunk.ChunkSize];
            if (stream.Read(buffer) != chunk.ChunkSize) throw new ArgumentOutOfRangeException("Reached end of file while parsing data chunk");
            chunk.WaveData = new short[chunk.ChunkSize / 2];
            Buffer.BlockCopy(buffer, 0, chunk.WaveData, 0, buffer.Length);
            return chunk;
        }

        public void AddSampleData(short[] leftBuffer,
           short[] rightBuffer) {
            WaveData = new short[leftBuffer.Length +
               rightBuffer.Length];
            int bufferOffset = 0;
            for (int index = 0; index < WaveData.Length; index += 2) {
                WaveData[index] = leftBuffer[bufferOffset];
                WaveData[index + 1] = rightBuffer[bufferOffset];
                bufferOffset++;
            }
            ChunkSize = (uint)WaveData.Length * 2;
        }
    }
}
