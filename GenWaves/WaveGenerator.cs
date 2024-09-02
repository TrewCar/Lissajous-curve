using Utils;

namespace GenWaves
{
    public delegate void StereoWaveDelegate(float[] leftChannel, float[] rightChannel);
    public class WaveGenerator : AudioGenerator
    {
        public List<Wave> Waves { get; set; }
        public WaveGenerator(int bitRate, int fps, List<Wave> waves) : base(bitRate, fps)
        {
            Waves = waves;
        }

        public override void StartGenerating(StereoWaveDelegate stereoWaveDelegate)
        {
            new Task(() =>
            {
                FPSCounter counter = new FPSCounter();
                while (!this.End)
                {
                    int samplesPerFrame = BitRate;

                    float[] leftChannel = new float[samplesPerFrame];
                    float[] rightChannel = new float[samplesPerFrame];

                    foreach (var wave in Waves)
                    {
                        wave.GenerateSamples(leftChannel, rightChannel, BitRate, samplesPerFrame);
                    }

                    stereoWaveDelegate(leftChannel, rightChannel);
                    counter.Update();
                    Task.Delay((int)(1000f / FPS)).Wait();
                }
            }).Start();
        }
        public override void Dispose()
        {
            base.Dispose();
        }
    }
}
