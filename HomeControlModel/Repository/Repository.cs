using HomeControl.Models.BaseClasses;
using HomeControl.Models.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeControl.Models.Repository
{
    public class DevicesRepository
    {
        public List<BaseDevice> Devices { get; set; }

        public DevicesRepository()
        {
            Devices = new List<BaseDevice>();
        }

        public void AddDevice(string deviceType)
        {
            if (!deviceType.Any())
            {
                return;
            }

            DeviceFactory factory = new DeviceFactory();
            BaseDevice device = factory.CreateInstance(deviceType);

            if (Devices.Capacity == 0)
            {
                device.Id = 0;
            }
            else
            {
                device.Id = Devices.Max(d => d.Id) + 1;
            }
            Devices.Add(device);
        }

        public BaseDevice GetById(int id)
        {
            return Devices.FirstOrDefault(d => d.Id == id);
        }

        public IEnumerable<BaseDevice> GetAll()
        {
            return Devices;
        }

        public void UpdateDevice(BaseDevice device)
        {
            BaseDevice storedDevice = Devices.FirstOrDefault(d => d.Id == device.Id);
            storedDevice = device;
        }

        public void RemoveDevice(BaseDevice device)
        {
            Devices.Remove(device);
        }
    }
}