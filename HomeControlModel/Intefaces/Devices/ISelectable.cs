using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeControl.Models.Intefaces.Devices
{
    public interface ISelectable
    {
        IEnumerable<string> Channels { get; }
        string Channel { set; }
    }
}
