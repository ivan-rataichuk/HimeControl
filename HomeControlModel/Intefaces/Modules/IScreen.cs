using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeControl.Models.Intefaces.Modules
{
    public interface IScreen
    {
        void UpdateSignal(string signal);
        string GetPicture();
    }
}
