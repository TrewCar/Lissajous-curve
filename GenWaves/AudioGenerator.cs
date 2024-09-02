namespace GenWaves
{
    public abstract class AudioGenerator : IDisposable
    {
        public int BitRate { get; set; }
        public int FPS { get; set; }

        protected bool End = false;

        protected AudioGenerator(int bitRate, int fps)
        {
            BitRate = bitRate;
            FPS = fps;
        }

        public abstract void StartGenerating(StereoWaveDelegate stereoWaveDelegate);

        public virtual void Dispose()
        {
            End = true;
        }
    }
}
