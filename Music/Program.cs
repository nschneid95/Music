
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Windows.Forms;
using Notes;

namespace Music
{
    class Program
    {

        static void Main() {
            {
                Stream stream = File.OpenRead("C:\\Users\\theim\\Downloads\\piano_A.wav");
                SoundPlayer player = new SoundPlayer(stream);
                Console.WriteLine("Playing");
                player.PlaySync();
            }
            Console.WriteLine("Press any key to play simulated note");
            Console.Read();
            {

                List<byte> tempBytes = new List<byte>();

                WaveHeader header = new WaveHeader();
                FormatChunk format = new FormatChunk();
                DataChunk data = new DataChunk();
                short[] sample = Wave.Scale(Note.BuildNote(440, 10), 0.5).Sample(1);
                data.AddSampleData(sample, sample);

                header.FileLength += format.Length() + data.Length();

                tempBytes.AddRange(header.GetBytes());
                tempBytes.AddRange(format.GetBytes());
                tempBytes.AddRange(data.GetBytes());

                Stream myWaveData = new MemoryStream(tempBytes.ToArray());
                SoundPlayer player = new SoundPlayer(myWaveData);
                player.PlaySync();
            }

        }
    }
}
