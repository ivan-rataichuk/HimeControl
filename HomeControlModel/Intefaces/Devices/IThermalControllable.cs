using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeControl.Models.Intefaces.Devices
{
    public interface IThermalControllable
    {
        void SetTemperature(int temperature);
        int CurrentTemperature { get; }
    }
}
