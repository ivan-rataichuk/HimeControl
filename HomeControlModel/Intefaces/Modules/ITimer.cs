using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HomeControlModel.Events.HomeControlEvents;

namespace HomeControlModel.Intefaces.Modules
{
    interface ITimer
    {
        int Countdoun { set; }
        event UpdateState UpdateValue;
    }
}
