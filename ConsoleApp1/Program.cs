using GenWaves;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using Utils;

class Program
{
    static void Main(string[] args)
    {
        var window = new RenderWindow(new VideoMode(1000, 1000), "Lissajous Figures");
        window.Closed += (s, a) => { window.Close(); };
        uint FPS = 30;

        float maxAmplitude = 1.0f;
        float scale = Math.Min(window.Size.X, window.Size.Y) / 4.0f / maxAmplitude;

        int sampleRate = 44100; // Задаем битрейт
        int step = sampleRate / (int)FPS;

        List<Wave> waves = new List<Wave>
        {
            new Wave(440f, 0.5f, -50),  // В левом канале
            new Wave(880f, 0.3f, 50),   // В правом канале
            //new Wave(660f, 0.2f, 0),    // В обоих каналах равномерно
        };

        //AudioGenerator waveGenerator = new WaveGenerator(44100, (int)FPS, waves);
        AudioGenerator waveGenerator = new NAudioMicrophoneConverter();
        window.SetActive(false);
        FPSCounter fpsCounter = new FPSCounter();

        float[] leftChannel = new float[step];
        float[] rightChannel = new float[step];
        bool dataReady = false;

        // Генерация стерео данных в отдельной задаче
        waveGenerator.StartGenerating((left, right) =>
        {
            lock (leftChannel)
            {
                Array.Copy(left, leftChannel, step);
                Array.Copy(right, rightChannel, step);
                dataReady = true;
            }
        });

        // Основной цикл визуализации
        while (window.IsOpen)
        {
            if (dataReady)
            {
                window.SetActive(true);
                fpsCounter.Update();

                window.Clear(Color.Black);

                List<Vertex> line = new List<Vertex>();

                lock (leftChannel)
                {
                    for (int i = 0; i < step; i++)
                    {
                        float x = scale * leftChannel[i];
                        float y = scale * rightChannel[i];

                        line.Add(new Vertex(new Vector2f(window.Size.X / 2 + x, window.Size.Y / 2 - y), Color.Green));
                    }

                    dataReady = false;
                }

                window.Draw(line.ToArray(), PrimitiveType.Points);
                window.Display();
            }

            window.DispatchEvents();
        }
    }
}