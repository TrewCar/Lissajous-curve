using ConsoleApp1;
using NAudio.Wave;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

class Program
{
    static void Main(string[] args)
    {
        string mp3FilePath = @"Music\Jerobeam Fenderson - Shrooms.mp3";

        // Инициализация аудио
        var samplesLeft = new float[0];
        var samplesRight = new float[0];

        using (var reader = new AudioFileReader(mp3FilePath))
        {
            int totalSamples = (int)reader.Length / sizeof(float) / reader.WaveFormat.Channels;
            samplesLeft = new float[totalSamples];
            samplesRight = new float[totalSamples];

            float[] buffer = new float[reader.WaveFormat.SampleRate * reader.WaveFormat.Channels];
            int samplesRead, bufferIndex = 0;

            while ((samplesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
            {
                for (int i = 0; i < samplesRead; i += 2) // Чтение по 2 канала
                {
                    samplesLeft[bufferIndex] = buffer[i]; // Левый канал
                    samplesRight[bufferIndex] = buffer[i + 1]; // Правый канал
                    bufferIndex++;
                }
            }
        }
        var window = new RenderWindow(new VideoMode(1920, 1080), "Lissajous Figures");
        uint FPS = 144; 

        var audioReader = new AudioFileReader(mp3FilePath);
        var outputDevice = new WaveOutEvent();
        outputDevice.Init(audioReader);
        //outputDevice.Play();


        sampleRate = audioReader.WaveFormat.SampleRate;
        window.SetFramerateLimit(FPS);

        // Вычисляем максимальную амплитуду для масштабирования
        float maxAmplitude = 0;
        for (int i = 0; i < samplesLeft.Length; i++)
        {
            maxAmplitude = Math.Max(maxAmplitude, Math.Max(samplesLeft[i], samplesRight[i]));
        }

        float scale = Math.Min(window.Size.X, window.Size.Y) / 4.0f / maxAmplitude;
        int step = (int)(sampleRate / FPS);
        int countFrames = 0; //(int)(FPS * audioReader.TotalTime.TotalSeconds * 0.3f);

        InstanceFilder("Frames");

        FPSCounter fpsCounter = new FPSCounter();
        while (window.IsOpen && FPS * audioReader.TotalTime.TotalSeconds > countFrames)
        {
            fpsCounter.Update();
            window.DispatchEvents();
            window.Clear(Color.Black);

            int sampleOffset = countFrames * step;

            List<Vertex> line = new List<Vertex>();

            for (int t = 0; t < step; t++)
            {
                int currentSampleIndex = sampleOffset + t;
                if (currentSampleIndex >= samplesLeft.Length || currentSampleIndex >= samplesRight.Length)
                    break;

                float x = scale * samplesLeft[currentSampleIndex];
                float y = scale * samplesRight[currentSampleIndex];

                line.Add(new Vertex(new Vector2f(window.Size.X / 2 + x, window.Size.Y / 2 - y), Color.Green));
            }

            window.Draw(line.ToArray(), PrimitiveType.Points);
            window.Display();

            window.Capture().SaveToFile(@$"Frames\frame_{countFrames:D9}.png");
            Console.WriteLine($"{countFrames}/{FPS * audioReader.TotalTime.TotalSeconds}");

            countFrames++;
        }

        window.Close();

        outputDevice.Dispose();
        audioReader.Dispose();

        InstanceFilder("OutVideo");

        FFMpegUtils.ConcatFrames(
            FPS: FPS,
            paternFrames: "frame_%09d.png",
            pathToFrames: "Frames",
            pathOutVideo: "OutVideo",
            outNameFile: "Test");

        InstanceFilder("OutResult");

        FFMpegUtils.VideoConcatAudio(
            pathToVideo: @"OutVideo\Test.mp4",
            pathToAudio: mp3FilePath,
            pathOutVideo: "OutResult",
            outNameFile: "Result"
            );
    }

    private static int sampleRate;

    private static void InstanceFilder(string path)
    {
        if (Directory.Exists(path))
            Directory.Delete(path, true);
        Directory.CreateDirectory(path);
    }
}
