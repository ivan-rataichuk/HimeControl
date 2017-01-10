﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HomeControlModel.Events.HomeControlEvents;

namespace HomeControl.Models.Intefaces.Modules
{
    public interface IRegulator
    {
        void SetValue(int value);
        event ReadState ReadValue;
        event UpdateState UpdateValue;
    }
}
