using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeControlModel.Events
{
    public class HomeControlEvents
    {
        public delegate void UpdateState(int value);
        public delegate void UpdateSignal(string signal);
        public delegate void UpdatePowerConsumption(int consumption);
    }
}
