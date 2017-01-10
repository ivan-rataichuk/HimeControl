using HomeControl.Models.Intefaces.Modules;
using HomeControlModel.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using static HomeControlModel.Events.HomeControlEvents;
using HomeControlModel.Events;

namespace HomeControl.Models.Modules
{
    [DataContract]
    public class Regulator : IRegulator
    {
        [DataMember]
        protected readonly int maxValue;

        [DataMember]
        protected readonly int minValue;

        public event UpdateState UpdateValue;
        public event HomeControlEvents.ReadState ReadValue;

        public Regulator()
        {
            
        }

        public Regulator(string type)
        {
            ReadSettings(type, out maxValue, out minValue);
        }

        public virtual void SetValue(int value)
        {
            InvokeUpdateValue(CheckValue(value)); 
        }


        protected void InvokeUpdateValue(int value)
        {
            UpdateValue?.Invoke(value);
        }

        protected int InvokeReadValue()
        {
            return ReadValue.Invoke();
        }

        protected int CheckValue(int value)
        {
            if (value > maxValue)
            {
                return maxValue;
            }
            else if (value < minValue)
            {
                return minValue;
            }
            return value;
        }

        private void ReadSettings(string type, out int maxValue, out int minValue)
        {
            maxValue = 100;
            minValue = 0;

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(FilesLocation.deviceSettingsXML);
            XmlElement xNodes = xDoc.DocumentElement;

            foreach (XmlNode xNode in xNodes)
            {
                if (xNode.Attributes.Count > 0)
                {
                    XmlNode attr = xNode.Attributes.GetNamedItem("type");
                    if ((attr != null) && (attr.Value == type))
                    {
                        int max;
                        int min;

                        if (int.TryParse(xNode.SelectSingleNode("max-value").InnerText, out max) &&
                            int.TryParse(xNode.SelectSingleNode("min-value").InnerText, out min))
                        {
                            maxValue = max;
                            minValue = min;
                        }

                        return;
                    }
                }
            }
        }

        
    }
}