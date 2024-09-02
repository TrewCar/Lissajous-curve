using NAudio.CoreAudioApi;
using NAudio.Wave;
using System.Text.RegularExpressions;

namespace ConsoleApp1.GenWaves
{
    public class NAudioConverter<T> : AudioGenerator where T : WasapiCapture, new()
    {
        private T Capture;
        public NAudioConverter(T Capture) : base(0, 0)
        {
            this.Capture = Capture;
        }

        public override void StartGenerating(StereoWaveDelegate stereoWaveDelegate)
        {
            Capture.DataAvailable += (s, a) =>
            {
                int samplesPerFrame = (int)(BitRate / FPS);

                List<float> leftChannel = new List<float>();
                List<float> rightChannal = new List<float>();


                for (int i = 4; i < a.BytesRecorded; i += 8)
                {
                    leftChannel.Add(BitConverter.ToSingle(a.Buffer, i));
                    rightChannal.Add(BitConverter.ToSingle(a.Buffer, i + 4));
                }
                stereoWaveDelegate(leftChannel.ToArray(), rightChannal.ToArray());
            };
        }

        public override void Dispose()
        {
            Capture.Dispose();
        }
    }
}
