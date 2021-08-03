
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Linq;

namespace Music
{
    class Program
    {
        static double CalculateAmplitude(double frequency, short[] sample) {
            double integral = 0;
            IEnumerator<short> iter = new SineGenerator(frequency).Values.GetEnumerator();
            foreach (short val in sample) {
                iter.MoveNext();
                integral += val * (double)iter.Current;
            }
            return integral;
        }

        static double CalculateMeanSquaredError(Dictionary<double, double> amplitudes, short[] sample) {
            List<WeightedWave> weightedWaves = new List<WeightedWave>();
            foreach (KeyValuePair<double, double> pair in amplitudes)
                weightedWaves.Add(new WeightedWave { wave = new SineGenerator(pair.Key), weight = pair.Value });
            Wave wave = Wave.Add(weightedWaves);
            double squaredError = 0;
            IEnumerator<short> iter = wave.Values.GetEnumerator();
            foreach (short val in sample) {
                iter.MoveNext();
                double error = val - iter.Current;
                squaredError += error * error;
            }
            return squaredError * Wave.SAMPLE_RATE / sample.Length;
        }

        static void Main() {
            {
                Stream stream = File.OpenRead("C:\\Users\\Tsuki\\Downloads\\piano_A.wav");
                //SoundPlayer player = new SoundPlayer(stream);
                //Console.WriteLine("Playing");
                //player.PlaySync();
                WaveHeader.ExtractHeader(stream);
                FormatChunk format = FormatChunk.ExtractFormatChunk(stream);
                DataChunk.ExtractDataChunk(stream);
                DataChunk data = DataChunk.ExtractDataChunk(stream);
                if (format.Frequency != Wave.SAMPLE_RATE) throw new Exception("Mismatched frequencies");
                short[] sample = new short[Wave.SAMPLE_RATE / 10];
                using (StreamWriter output = new StreamWriter(File.OpenWrite("C:\\Users\\Tsuki\\Documents\\frequencies.csv"))) {
                    output.Write("start_time,frequency,amplitude,error\n");
                    for (int offset = 0; offset < Wave.SAMPLE_RATE * 5; offset += sample.Length) {
                        Buffer.BlockCopy(data.WaveData, offset, sample, 0, sample.Length);
                        Dictionary<double, double> amps = new Dictionary<double, double>();
                        for (double frequency = 440; frequency < 10000; frequency *= 2)
                            amps[frequency] = CalculateAmplitude(frequency, sample);
                        double error = CalculateMeanSquaredError(amps, sample);
                        double sum = amps.Aggregate(0.0, (sum, pair) => sum + pair.Value);
                        foreach (KeyValuePair<double, double> pair in amps)
                            output.Write("{0},{1},{2},{3}\n", (double)offset / Wave.SAMPLE_RATE, pair.Key, pair.Value, error);
                        //    Console.WriteLine("Frequency: {0}\tApplitude: {1}", pair.Key, pair.Value / sum);
                    }
                    output.Flush();
                }
            }
            //Console.WriteLine("Press any key to play simulated note");
            //Console.Read();
            {

                //List<byte> tempBytes = new List<byte>();

                //WaveHeader header = new WaveHeader();
                //FormatChunk format = new FormatChunk();
                //DataChunk data = new DataChunk();
                //short[] sample = Note.BuildNote(440, 10).Sample(1);
                //data.AddSampleData(sample, sample);

                //header.FileLength += format.Length() + data.Length();

                //tempBytes.AddRange(header.GetBytes());
                //tempBytes.AddRange(format.GetBytes());
                //tempBytes.AddRange(data.GetBytes());

                //Stream myWaveData = new MemoryStream(tempBytes.ToArray());
                //SoundPlayer player = new SoundPlayer(myWaveData);
                //player.PlaySync();
            }

        }
    }
}
