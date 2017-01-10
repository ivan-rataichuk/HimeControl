using HomeControl.Models.BaseClasses;
using HomeControl.Models.Intefaces.Devices;
using HomeControl.Models.Intefaces.Modules;
using HomeControl.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeControl.Models.Modules;
using System.Runtime.Serialization;

namespace HomeControl.Models.Devices
{
    [Device("Heater")]
    [DataContract]
    public class Heater : BaseDevice, IThermalControllable
    {
        [DataMember]
        public int CurrentTemperature { get; private set; }

        [DataMember]
        private IRegulator temperatureRegulator;

        public Heater() : base("Heater")
        {
            temperatureRegulator = new Regulator(this.Type);
            temperatureRegulator.UpdateValue += UpdateCurrentTemperature;
        }

        public void SetTemperature(int temperature)
        {
            temperatureRegulator.SetValue(temperature);
        }

        private void UpdateCurrentTemperature(int temperature)
        {
            CurrentTemperature = temperature;
        }
    }
}