using HomeControl.Models;
using HomeControl.Models.BaseClasses;
using HomeControl.Models.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            HomeModel hm = HomeModel.GetInstance();
            foreach (var item in hm.GetDevicesTypes())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("---------------");

            hm.Devices.AddDevice("Heater");
            hm.Devices.AddDevice("Tv");

            foreach (var item in hm.Devices.GetAll())
            {
                Console.WriteLine($"{item.Type} ID: {item.Id}"); 
            }

            Heater heater = (Heater)hm.Devices.GetById(0);
            Console.WriteLine($"T = {heater.CurrentTemperature}");
            heater.SetTemperature(100);
            Console.WriteLine($"T = {heater.CurrentTemperature}");

            Tv tv = (Tv)hm.Devices.GetById(1);
            IEnumerable<string> channels = tv.Channels;
            foreach (var item in channels)
            {
                Console.WriteLine(item);
            }

            tv.Channel = "Channel 1";
            Console.WriteLine(tv.VideoSignal);

            tv.SetVolume(90);
            Console.WriteLine(tv.Volume);

            //hm.SaveInstance();
            Console.WriteLine(hm == null ? "null" : "notNull");

        }
    }
}
