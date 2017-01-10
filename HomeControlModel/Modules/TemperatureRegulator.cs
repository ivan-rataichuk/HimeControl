using HomeControl.Models.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HomeControl.Models.Modules
{
    [DataContract]
    class TemperatureRegulator : Regulator
    {
        private Task temperatureVariation;

        [DataMember]
        private int setTemperature;

        public TemperatureRegulator()
        {
            Init();
        }

        public TemperatureRegulator(string type) : base(type)
        {
            Init();
        }

        public override void SetValue(int value)
        {
            setTemperature = CheckValue(value);
        }

        private void Simulate()
        {
            int currentTemperature;

            while (true)
            {
                currentTemperature = InvokeReadValue();
                if (currentTemperature != setTemperature)
                {
                    InvokeUpdateValue(currentTemperature + Math.Sign(setTemperature - currentTemperature));
                }
                Thread.Sleep(1000);
            }
        }

        private void Init()
        {
            temperatureVariation = new Task(Simulate);
            temperatureVariation.Start();
        }
    }
}
