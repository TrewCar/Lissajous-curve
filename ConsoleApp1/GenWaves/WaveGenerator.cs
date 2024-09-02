using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GenWaves
{
    public delegate void StereoWaveDelegate(float[] leftChannel, float[] rightChannel);
    public class WaveGenerator : AudioGenerator
    {
        public List<Wave> Waves { get; set; }
        public WaveGenerator(int bitRate, int fps, List<Wave> waves) : base(bitRate, fps) 
        {
            this.Waves = waves;
        }

        public override void StartGenerating(StereoWaveDelegate stereoWaveDelegate)
        {
            new Task(() =>
            {
                FPSCounter counter = new FPSCounter();
                while (true)
                {
                    int samplesPerFrame = (int)(BitRate / FPS);

                    float[] leftChannel = new float[samplesPerFrame];
                    float[] rightChannel = new float[samplesPerFrame];

                    // Предполагается, что у нас есть список волн
                    foreach (var wave in Waves)
                    {
                        wave.GenerateSamples(leftChannel, rightChannel, BitRate, samplesPerFrame);
                    }

                    stereoWaveDelegate(leftChannel, rightChannel);
                    counter.Update();
                    Task.Delay((int)(1000f / (float)FPS)).Wait();
                }
            }).Start();
        }
        public override void Dispose()
        {
            
        }
    }
}
