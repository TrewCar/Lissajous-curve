using GenWaves;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
namespace WinForm
{
    public partial class Main : Form
    {
        private RenderWindow window;
        private AudioGenerator generator;

        private uint FPS = 30;

        private int sampleRate = 44100; // Задаем битрейт
        private int step;

        private float[] leftChannel;
        private float[] rightChannel;

        private bool On = false;
        private bool dataReady = false;

        private float maxAmplitude = 1f;
        private float scale = 1f;

        private TypeRender render = TypeRender.Lisajue;

        public Main()
        {
            step = sampleRate / (int)FPS;
            leftChannel = new float[step];
            rightChannel = new float[step];

            InitializeComponent();
        }

        private void SetType_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton == null)
                return;
            generator?.Dispose();
            if (radioButton.Checked)
            {
                if (radioButton.Name == "RadioMicro")
                {
                    generator = new NAudioMicrophoneConverter();
                    InitDraw();
                }
                else if (radioButton.Name == "RadioPC")
                {
                    generator = new NAudioPcConverter();
                    InitDraw();
                }
                else if (radioButton.Name == "RadioWave")
                {
                    List<Wave> waves = new List<Wave>
                    {
                        new Wave(440f, 0.5f, -50),  
                        new Wave(880f, 0.3f, 50),   
                        //new Wave(660f, 0.2f, 0),    
                    };
                    generator = new WaveGenerator(sampleRate, (int)FPS, waves);
                    InitDraw();
                }
                else if (radioButton.Name == "RadioOff")
                {
                    generator.Dispose();
                    generator = null;
                }
                if (generator != null)
                {
                    step = generator.BitRate / (int)FPS;
                    lock (leftChannel)
                    {
                        leftChannel = new float[step];
                        rightChannel = new float[step];
                    }
                }
            }
        }
        private void InitWindowRender()
        {
            DisposeWindowRender();
            window = new RenderWindow(new VideoMode(1920, 1080), "Lissajous Figures");
            window.Closed += (s, a) => { window.Close(); On = false; };
            scale = Math.Min(window.Size.X, window.Size.Y) / 4.0f / maxAmplitude;

            while (On)
            {
                if (dataReady)
                {
                    window.SetActive(true);
                    //fpsCounter.Update();

                    window.Clear(SFML.Graphics.Color.Black);
                    if (render == TypeRender.Lisajue)
                        DrawLisajue();
                    else if (render == TypeRender.Waves)
                        DrawWaves();

                    window.Display();
                }
                window.DispatchEvents();
            }
        }
        private void DisposeWindowRender()
        {
            if (window == null) return;
            window.Close();
            window.Dispose();
            window = null;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            On = !On;
            if (!On)
                DisposeWindowRender();
            else
                InitWindowRender();
        }

        private void InitDraw()
        {
            generator.StartGenerating((left, right) =>
            {
                lock (leftChannel)
                {
                    Array.Copy(left, leftChannel, step);
                    Array.Copy(right, rightChannel, step);
                    dataReady = true;
                }
            });
        }

        private void Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            window?.Close();
        }

        private void DrawLisajue()
        {
            List<Vertex> line = new List<Vertex>();

            lock (leftChannel)
            {
                for (int i = 0; i < step; i++)
                {
                    float x = scale * leftChannel[i];
                    float y = scale * rightChannel[i];

                    line.Add(new Vertex(new Vector2f(window.Size.X / 2 + x, window.Size.Y / 2 - y), SFML.Graphics.Color.Green));
                }

                dataReady = false;
            }

            window.Draw(line.ToArray(), PrimitiveType.LineStrip);
        }
        private void DrawWaves()
        {
            lock (leftChannel)
            {
                window.Draw(DrawWave(leftChannel, SFML.Graphics.Color.Green));
                window.Draw(DrawWave(rightChannel, SFML.Graphics.Color.Blue));
                dataReady = false;
            }
        }
        private VertexArray DrawWave(float[] waveData, SFML.Graphics.Color color)
        {
            var vertexArray = new VertexArray(PrimitiveType.LineStrip, (uint)waveData.Length);

            for (int i = 0; i < waveData.Length; i++)
            {
                float x = (float)i / waveData.Length * window.Size.X;
                float y = (1.0f - waveData[i]) * (window.Size.Y / 2);
                vertexArray[(uint)i] = new Vertex(new Vector2f(x, y), color);
            }
            return vertexArray;
        }

        private void RadioWaves_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton == null)
                return;

            if (radioButton.Checked)
            {
                if (radioButton.Name == "RadioLisajue")
                {
                    render = TypeRender.Lisajue;
                }
                else if (radioButton.Name == "RadioWaves")
                {
                    render = TypeRender.Waves;
                }
            }
        }
    }
    enum TypeRender
    {
        Lisajue,
        Waves
    }
}
