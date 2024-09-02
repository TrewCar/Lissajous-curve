using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GenWaves
{
    public delegate void StereoWaveDelegate(float[] leftChannel, float[] rightChannel);
    public class WaveGenerator
    {
        public int BitRate { get; set; }
        public int FPS { get; set; }


        public WaveGenerator(int bitRate, int fps)
        {
            BitRate = bitRate;
            FPS = fps;
        }

        public void GenerateStereoWave(List<Wave> waves, StereoWaveDelegate stereoWaveDelegate)
        {
            new Task(() =>
            {
                FPSCounter counter = new FPSCounter();
                while (true)
                {
                    int samplesPerFrame = (int)(BitRate / FPS);

                    float[] leftChannel = new float[samplesPerFrame];
                    float[] rightChannel = new float[samplesPerFrame];

                    foreach (var wave in waves)
                    {
                        wave.GenerateSamples(leftChannel, rightChannel, BitRate, samplesPerFrame);
                    }

                    stereoWaveDelegate(leftChannel, rightChannel);
                    counter.Update();
                    Task.Delay((int)(1000f / (float)FPS));
                }
            }).Start();
        }
    }
}
