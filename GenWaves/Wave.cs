namespace GenWaves
{
    public class Wave
    {
        public float Frequency { get; set; }
        public float Volume { get; set; }
        public int LR { get; set; } // от -100 (левый) до 100 (правый)
        private double phase;

        public Wave(float frequency, float volume, int lr)
        {
            Frequency = frequency;
            Volume = volume;
            LR = lr;
            phase = 0;
        }

        // Метод для генерации амплитуды для текущего момента времени с учетом фазы
        public void GenerateSamples(float[] leftChannel, float[] rightChannel, int sampleRate, int n)
        {
            for (int i = 0; i < n; i++)
            {
                double amplitude = Volume * Math.Sin(phase);

                // Панорамирование для левого и правого каналов
                float leftAmplitude = (float)(amplitude * (1 - Math.Max(0, LR) / 100.0f));
                float rightAmplitude = (float)(amplitude * (1 - Math.Max(0, -LR) / 100.0f));

                leftChannel[i] += leftAmplitude;
                rightChannel[i] += rightAmplitude;

                // Обновляем фазу
                phase += 2 * Math.PI * Frequency / sampleRate;

                // Ограничиваем фазу, чтобы она не становилась слишком большой
                if (phase > 2 * Math.PI)
                    phase -= 2 * Math.PI;
            }
        }
    }
}
