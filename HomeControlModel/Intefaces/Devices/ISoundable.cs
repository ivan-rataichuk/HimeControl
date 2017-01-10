using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeControl.Models.Intefaces.Devices
{
    public interface ISoundable
    {
        void SetVolume(int volume);
        string SoundSignal { get; }
        int Volume { get; }
    }
}
