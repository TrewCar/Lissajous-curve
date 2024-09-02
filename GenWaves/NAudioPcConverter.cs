using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenWaves
{
    public class NAudioPcConverter : NAudioConverter<WasapiLoopbackCapture>
    {
        public NAudioPcConverter() : base() { }
    }
}
