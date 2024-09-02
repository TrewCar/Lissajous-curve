using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GenWaves
{
    public abstract class AudioGenerator : IDisposable
    {
        public int BitRate { get; set; }
        public int FPS { get; set; }

        protected AudioGenerator(int bitRate, int fps)
        {
            BitRate = bitRate;
            FPS = fps;
        }

        public abstract void StartGenerating(StereoWaveDelegate stereoWaveDelegate);

        public abstract void Dispose();
    }
}
