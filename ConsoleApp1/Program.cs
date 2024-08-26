using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using ConsoleApp1;
using NAudio.Wave;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using static SFML.Window.Keyboard;

class Program
{
    static void Main(string[] args)
    {
        //string mp3FilePath = @"C:\Users\RMK\Music\Max for Live patch_ trop 0.4 (192kbps).mp3";
        string mp3FilePath = @"C:\Users\RMK\Music\Jerobeam Fenderson - Shrooms (320kbps).mp3";
        //string mp3FilePath = @"C:\Users\RMK\Music\Derivakat_-_REVIVED_73357566.mp3";
        //string mp3FilePath = @"https://rus.hitmotop.com/get/music/20190329/Billie_Eilish_-_bad_guy_63132977.mp3";
        // Инициализация аудио
        var samplesLeft = new List<float>();
        var samplesRight = new List<float>();
        using (var reader = new AudioFileReader(mp3FilePath))
        {
            float[] buffer = new float[reader.WaveFormat.SampleRate * reader.WaveFormat.Channels];
            int samplesRead;
            while ((samplesRead = reader.Read(buffer, 0, buffer.Length)) > 0)
            {
                for (int i = 0; i < samplesRead; i += 2) // Чтение по 2 канала
                {
                    samplesLeft.Add(buffer[i]); // Левый канал
                    samplesRight.Add(buffer[i + 1]); // Правый канал
                }
            }
        }
        int FPS = 144;  // Устанавливаем фиксированный FPS

        var audioReader = new AudioFileReader(mp3FilePath);

        var outputDevice = new WaveOutEvent();
        outputDevice.Init(audioReader);
        outputDevice.Play();

        // Инициализация окна SFML
        var window = new RenderWindow(new VideoMode(1000, 1000), "Lissajous Figures");

        // Параметры отображения
        sampleRate = audioReader.WaveFormat.SampleRate;
        int sampleOffset = 0;

        // Ограничиваем частоту кадров
        window.SetFramerateLimit((uint)FPS);

        int frameCount = 0;

        FPSCounter fpsCounter = new FPSCounter();

        fpsCounter.Update();

        while (window.IsOpen)
        {
            line.Clear();

            window.DispatchEvents();
            window.Clear(Color.Black);

            int step = (int)(sampleRate / fpsCounter.FPS);  // Количество семплов на один фрейм
            Limit = step;
            // Используем счетчик кадров для расчета смещения
            sampleOffset = (int)(audioReader.CurrentTime.TotalSeconds * sampleRate);

            // Предварительная обработка для масштабирования амплитуд
            float maxAmplitude = Math.Max(samplesLeft.Max(), samplesRight.Max());
            float scale = Math.Min(window.Size.X, window.Size.Y) / 4.0f / maxAmplitude;

            for (int t = 0; t < step; t++)
            {
                int currentSampleIndex = sampleOffset + t;
                if (currentSampleIndex >= samplesLeft.Count || currentSampleIndex >= samplesRight.Count)
                    break;

                float A = samplesLeft[currentSampleIndex];  // Амплитуда левого канала по X
                float B = samplesRight[currentSampleIndex]; // Амплитуда правого канала по Y

                float omega = 2 * (float)Math.PI * t / sampleRate;

                // Классическая форма Лиссажу
                float x = scale * A * (float)Math.Cos(omega);
                float y = scale * B * (float)Math.Cos(omega);

                Add(new Vertex(new Vector2f(window.Size.X / 2 + x, window.Size.Y / 2 - y), Color.Green));
            }
            frameCount++;
            window.Draw(line.ToArray(), PrimitiveType.LineStrip);
            window.Display();
            fpsCounter.Update();
        }

        window.Close();

        // Освобождение ресурсов
        outputDevice.Dispose();
        audioReader.Dispose();

        InstanceFilder("OutVideo");

        FFMpegUtils.ConcatFrames(
            FPS: 144,
            paternFrames: "frame_%09d.png",
            pathToFrames: "Test",
            pathOutVideo: "OutVideo",
            outNameFile: "Test");
    }
    private static int sampleRate;
    private static int Limit;
    private static List<Vertex> line = new();
    private static void Add(Vertex obj)
    {
        line.Add(obj);
        if(line.Count > Limit)
            line.RemoveAt(0);
    }

    private static void InstanceFilder(string path)
    {
        if (Directory.Exists(path))
            Directory.Delete(path, true);
        Directory.CreateDirectory(path);
    }

    private static void DrawLissajousFigure(RenderWindow window, float[] samplesLeft, float[] samplesRight, int offset, int sampleRate)
    {
        var width = window.Size.X;
        var height = window.Size.Y;

        VertexArray lissajous = new VertexArray(PrimitiveType.LineStrip);

        // Убедимся, что не выйдем за пределы массива
        if (offset + sampleRate / 30 > samplesLeft.Length || offset + sampleRate / 30 > samplesRight.Length)
            return;

        // Предварительная обработка для масштабирования амплитуд
        float maxAmplitude = Math.Max(samplesLeft.Max(), samplesRight.Max());
        float scale = Math.Min(1000, 1000) / 4.0f / maxAmplitude;

        for (int t = 0; t < sampleRate / 30; t++)
        {
            float A =  samplesLeft[offset + t];  // Амплитуда левого канала по X
            float B = samplesRight[offset + t]; // Амплитуда правого канала по Y

            // Используем omega для создания гармонического движения
            float omega = 2 * (float)Math.PI * t / sampleRate;
            float alpha = (float)Math.PI / 2 * (A + B) / 2;
            // Классическая форма Лиссажу
            float x = scale * A * (float)Math.Cos(omega);
            float y = scale * B * (float)Math.Cos(omega);

            lissajous.Append(new Vertex(new Vector2f(width / 2 + x, height / 2 - y), Color.Green));
        }

        window.Draw(lissajous);
    }
}