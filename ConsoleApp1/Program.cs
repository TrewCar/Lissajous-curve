using ConsoleApp1;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Program
{
    static void Main(string[] args)
    {

        using (var capture = new WasapiLoopbackCapture())
        {
            var window = new RenderWindow(new VideoMode(800, 800), "Lissajous Figures");
            window.Closed += (s, a) => { window.Close(); capture.StopRecording(); };
            uint FPS = 30;
            //window.SetFramerateLimit(FPS);

            float maxAmplitude = 1.0f;
            float scale = Math.Min(window.Size.X, window.Size.Y) / 4.0f / maxAmplitude;

            int sampleRate = capture.WaveFormat.SampleRate;
            int step = sampleRate / (int)FPS;
            int countFrames = 0;
            window.SetActive(false);
            FPSCounter fpsCounter = new FPSCounter();

            // Обработка аудио данных при их получении
            capture.DataAvailable += (s, a) =>
            {
                window.SetActive(true);
                fpsCounter.Update();

                window.Clear(Color.Black);

                List<Vertex> line = new List<Vertex>();

                for (int i = 4; i < a.BytesRecorded; i +=8)
                {
                    float x = scale * BitConverter.ToSingle(a.Buffer, i);
                    float y = scale * BitConverter.ToSingle(a.Buffer, i + 4);

                    line.Add(new Vertex(new Vector2f(window.Size.X / 2 + x, window.Size.Y / 2 - y), Color.Green));
                }

                window.Draw(line.ToArray(), PrimitiveType.Points);


                countFrames++;

                window.Display();
            };

            capture.StartRecording();

            while (window.IsOpen) { window.DispatchEvents(); }
        }
    }
}
