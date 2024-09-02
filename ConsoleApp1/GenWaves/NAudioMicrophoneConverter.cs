using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GenWaves
{
    class NAudioMicrophoneConverter : NAudioConverter<WasapiCapture>
    {
        public NAudioMicrophoneConverter() : base(new WasapiCapture()) { }
    }
}
