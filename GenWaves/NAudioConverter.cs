using NAudio.CoreAudioApi;

namespace GenWaves
{
    public abstract class NAudioConverter<T> : AudioGenerator where T : WasapiCapture, new()
    {
        private T Capture;
        public NAudioConverter() : base(0, 0)
        {
            Capture = new();
        }

        public override void StartGenerating(StereoWaveDelegate stereoWaveDelegate)
        {
            Capture.DataAvailable += (s, a) =>
            {
                List<float> leftChannel = new List<float>();
                List<float> rightChannal = new List<float>();


                for (int i = 4; i < a.BytesRecorded; i += 8)
                {
                    leftChannel.Add(BitConverter.ToSingle(a.Buffer, i));
                    rightChannal.Add(BitConverter.ToSingle(a.Buffer, i + 4));
                }
                stereoWaveDelegate(leftChannel.ToArray(), rightChannal.ToArray());
            };
            Capture.StartRecording();
            BitRate = Capture.WaveFormat.SampleRate;
        }

        public override void Dispose()
        {
            base.Dispose();
            Capture.StopRecording();
            Capture.Dispose();
        }
    }
}
