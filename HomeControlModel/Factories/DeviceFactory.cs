using HomeControl.Models.BaseClasses;
using HomeControl.Models.Devices;
using HomeControl.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using HomeControlModel.Settings;

namespace HomeControl.Models.Factories
{
    public class DeviceFactory
    {
        private Dictionary<string, Type> typesDictionary;
        
        public DeviceFactory()
        {
            Init();
        }

        public BaseDevice CreateInstance(string type)
        {
            return (BaseDevice)Activator.CreateInstance(typesDictionary[type]);
        }

        public IEnumerable<string> GetTypes()
        {
            return typesDictionary.Keys.ToList();
        }

        private void Init()
        {
            typesDictionary = new Dictionary<string, Type>();

            Assembly assembly = Assembly.LoadFrom(FilesLocation.homeControlModelAssembly);
            Type[] types = assembly.GetTypes();

            foreach (Type type in types)
            {
                Attribute[] attributes = Attribute.GetCustomAttributes(type);
                foreach (Attribute attribute in attributes)
                {
                    if (attribute is Device)
                    {
                        Device attrDevice = (Device)attribute;
                        typesDictionary.Add(attrDevice.GetName(), type);
                    }
                }
            }
        }

    }
}