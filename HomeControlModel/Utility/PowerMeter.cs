using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HomeControl.Models
{
    [DataContract]
    public class PowerMeter
    {
        [DataMember]
        public int TotalPowerConsumption { get; private set; }
        [DataMember]
        public int CurrentPowerConsumption { get; private set; }

        private static PowerMeter instance;
        private Task process;

        private PowerMeter()
        {
            process = new Task(CalculatePowerConsumption);
            process.Start();
        }

        public static PowerMeter GetInstance()
        {
            if (instance == null)
            {
                instance = new PowerMeter();
            }
            return instance;
        }

        public void UpdatePowerConsumption(int consumption)
        {
            CurrentPowerConsumption += consumption;
        }

        private void UpdateTotalPowerConsumption(int consumption)
        {
            TotalPowerConsumption += consumption;
        }

        private void CalculatePowerConsumption()
        {
            while (true)
            {
                UpdateTotalPowerConsumption(CurrentPowerConsumption / 60); // Total power consumption in W/min - for demonstration purpose
                Thread.Sleep(1000);
            }
        }
    }
}