using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Music
{
    public class WaveHeader
    {
        private const string FILE_TYPE_ID = "RIFF";
        private const string MEDIA_TYPE_ID = "WAVE";

        public string FileTypeId { get; private set; }
        public uint FileLength { get; set; }
        public string MediaTypeId { get; private set; }

        public WaveHeader() {
            FileTypeId = FILE_TYPE_ID;
            MediaTypeId = MEDIA_TYPE_ID;
            // Mininmum size is always 4 bytes
            FileLength = 4;
        }

        public byte[] GetBytes() {
            List<byte> chunkData = new List<byte>();
            chunkData.AddRange(Encoding.ASCII.GetBytes(FileTypeId));
            chunkData.AddRange(BitConverter.GetBytes(FileLength));
            chunkData.AddRange(Encoding.ASCII.GetBytes(MediaTypeId));
            return chunkData.ToArray();
        }

        public static WaveHeader ExtractHeader(Stream stream) {
            WaveHeader header = new WaveHeader();
            byte[] buffer = new byte[4];
            if (stream.Read(buffer) != 4) throw new ArgumentOutOfRangeException("Reached end of file while parsing wave header");
            header.FileTypeId = Encoding.ASCII.GetString(buffer);
            if (stream.Read(buffer) != 4) throw new ArgumentOutOfRangeException("Reached end of file while parsing wave header");
            header.FileLength = BitConverter.ToUInt32(buffer, 0);
            if (stream.Read(buffer) != 4) throw new ArgumentOutOfRangeException("Reached end of file while parsing wave header");
            header.MediaTypeId = Encoding.ASCII.GetString(buffer);
            return header;
        }
    }
}
