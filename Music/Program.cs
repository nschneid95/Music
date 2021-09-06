
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;
using System.Linq;

namespace Music
{
    class Program
    {
        //static double CalculateAmplitude(double frequency, short[] sample, double offset = 0) {
        //    double integral = 0;
        //    IEnumerator<short> iter = new SineGenerator(frequency, offset).Values.GetEnumerator();
        //    int count = 0;
        //    foreach (short val in sample) {
        //        count++;
        //        iter.MoveNext();
        //        integral += val * (double)iter.Current;
        //    }
        //    return 2 * integral / count / short.MaxValue;
        //}

        //static double CalculateMeanSquaredError(Dictionary<double, double> amplitudes, short[] sample, double offset = 0) {
        //    List<WeightedWave> weightedWaves = new List<WeightedWave>();
        //    foreach (KeyValuePair<double, double> pair in amplitudes)
        //        weightedWaves.Add(new WeightedWave { wave = new SineGenerator(pair.Key, offset), weight = pair.Value / short.MaxValue });
        //    Wave wave = Wave.Add(weightedWaves);
        //    double squaredError = 0;
        //    IEnumerator<short> iter = wave.Values.GetEnumerator();
        //    foreach (short val in sample) {
        //        iter.MoveNext();
        //        double error = val - iter.Current;
        //        squaredError += error * error;
        //    }
        //    return squaredError * Wave.SAMPLE_RATE / sample.Length;
        //}

        static void Main() {
            {
                Stream stream = File.OpenRead("C:\\Users\\theim\\Downloads\\piano_A.wav");
                SoundPlayer player = new SoundPlayer(stream);
                Console.WriteLine("Playing");
                player.PlaySync();

                // Fourier analysis code
                /*
                WaveHeader.ExtractHeader(stream);
                FormatChunk format = FormatChunk.ExtractFormatChunk(stream);
                DataChunk.ExtractDataChunk(stream);
                DataChunk data = DataChunk.ExtractDataChunk(stream);
                if (format.Frequency != Wave.SAMPLE_RATE) throw new Exception("Mismatched frequencies");
                short[] waveData = new short[data.WaveData.Length - Wave.SAMPLE_RATE];
                Buffer.BlockCopy(data.WaveData, (int)Wave.SAMPLE_RATE, waveData, 0, waveData.Length * 2);
                //short[] waveData = new short[101];
                //Buffer.BlockCopy(new SineGenerator(440, 1.0/3333).Values.Take(waveData.Length).ToArray(), 0, waveData, 0, waveData.Length * 2);
                // First calculate the offset needed to match a full wave
                double min_error = -1;
                double phase = -1;
                for (double test_offset = 0; test_offset < 1.0 / 440; test_offset += 1.0 / Wave.SAMPLE_RATE) {
                    double error = CalculateMeanSquaredError(new Dictionary<double, double> { { 440, CalculateAmplitude(440, waveData, test_offset)} }, waveData, test_offset);
                    if (min_error < 0 || error < min_error) {
                        phase = test_offset;
                        min_error = error;
                    }
                }
                // Sample rate needs to be a full wave for all calculated frequencies. Basically a multiple of (Wave.SAMPLE_RATE / 440)
                short[] sample = new short[Wave.SAMPLE_RATE / 10];
                using (StreamWriter output = new StreamWriter(File.OpenWrite("C:\\Users\\theim\\Documents\\frequencies.csv"))) {
                    output.Write("start_time,frequency,amplitude,error\n");
                    for (int sample_offset = 0; sample_offset < Wave.SAMPLE_RATE * 5; sample_offset += sample.Length) {
                        Buffer.BlockCopy(data.WaveData, sample_offset, sample, 0, sample.Length * 2);
                        Dictionary<double, double> amps = new Dictionary<double, double>();
                        for (double frequency = 440; frequency < 10000; frequency *= 2)
                            amps[frequency] = CalculateAmplitude(frequency, sample, phase);
                        double error = CalculateMeanSquaredError(amps, sample, phase);
                        //double sum = amps.Aggregate(0.0, (sum, pair) => sum + pair.Value);
                        foreach (KeyValuePair<double, double> pair in amps)
                            output.Write("{0},{1},{2},{3}\n", (double)sample_offset / Wave.SAMPLE_RATE, pair.Key, pair.Value, error);
                        //    Console.WriteLine("Frequency: {0}\tApplitude: {1}", pair.Key, pair.Value / sum);
                    }
                    output.Flush();
                }
                */
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
