using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace HomeControl.Models
{
    public class PowerMeter
    {
        public int TotalPowerConsumption { get; private set; }
        public int CurrentPowerConsumption { get; private set; }
        private Task process;

        public PowerMeter()
        {
            process = new Task(CalculatePowerConsumption);
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
            UpdateTotalPowerConsumption(CurrentPowerConsumption / 60);
            Thread.Sleep(1000);
        }
    }
}