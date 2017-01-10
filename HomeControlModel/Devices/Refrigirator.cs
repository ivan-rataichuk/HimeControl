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
    [Device("Refrigirator")]
    [DataContract]
    public class Refrigirator : BaseDevice, IThermalControllable
    {
        [DataMember]
        private IRegulator temperatureRegulator;

        [DataMember]
        public int CurrentTemperature { get; private set; }

        public Refrigirator() : base("Refrigirator")
        {
            temperatureRegulator = new TemperatureRegulator(this.Type);
            temperatureRegulator.UpdateValue += UpdateCurrentTemperature;
            temperatureRegulator.ReadValue += ReadCurrentTemperature;
        }

        public void SetTemperature(int temperature)
        {
            if (IsOn)
            {
                temperatureRegulator.SetValue(temperature);
            }   
        }

        private void UpdateCurrentTemperature(int temperature)
        {
            CurrentTemperature = temperature;
        }

        private int ReadCurrentTemperature()
        {
            return CurrentTemperature;
        }
    }
}