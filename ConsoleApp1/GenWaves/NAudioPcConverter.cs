using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.GenWaves
{
    public class NAudioPcConverter : NAudioConverter<WasapiLoopbackCapture>
    {
        public NAudioPcConverter() : base(new WasapiLoopbackCapture()) { }
    }
}
