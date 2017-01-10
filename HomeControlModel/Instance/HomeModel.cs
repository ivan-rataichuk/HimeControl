using HomeControl.Models.BaseClasses;
using HomeControl.Models.Devices;
using HomeControl.Models.Factories;
using HomeControl.Models.Intefaces.Devices;
using HomeControl.Models.Repository;
using HomeControlModel.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using HomeControlModel.Utility;

namespace HomeControl.Models
{
    [DataContract]
    public class HomeModel
    {
        [DataMember]
        public PowerMeter PowerMeter { get; private set; }

        [DataMember]
        public DevicesRepository Devices { get; private set; }

        private static HomeModel instance;
        private Scheduler scheduler;

        private HomeModel()
        {
            PowerMeter = PowerMeter.GetInstance();
            Devices = new DevicesRepository();
            scheduler = new Scheduler();
        }

        ~HomeModel()
        {
            SaveChanges();
        }

        public static HomeModel GetInstance()
        {
            if (instance == null)
            {
                instance = ReadInstance();
            }
            return instance;
        }

        public IEnumerable<string> GetDevicesTypes()
        {
            return new DeviceFactory().GetTypes();
        }

        public void SaveChanges()
        {
            PersistenceUnit persistenceUnit = new PersistenceUnit();
            persistenceUnit.SaveInstanse(instance);
        }

        private static HomeModel ReadInstance()
        {
            HomeModel instance = new PersistenceUnit().ReadInstance();
            if (instance == null)
            {
                return new HomeModel();
            }
            return instance;
        }

    }
}