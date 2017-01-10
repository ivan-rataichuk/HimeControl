using HomeControl.Models.Intefaces.Modules;
using HomeControlModel.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using static HomeControlModel.Events.HomeControlEvents;

namespace HomeControl.Models.Modules
{
    [DataContract]
    public class Regulator : IRegulator
    {
        [DataMember]
        private readonly int maxValue;

        [DataMember]
        private readonly int minValue;

        public event UpdateState UpdateValue;

        public Regulator()
        {
            
        }

        public Regulator(string type)
        {
            ReadSettings(type, out maxValue, out minValue);
        }

        public void SetValue(int value)
        {
            if (value > maxValue)
            {
                UpdateValue?.Invoke(maxValue);
            }
            else if (value < minValue)
            {
                UpdateValue?.Invoke(minValue);
            }
            else
            {
                UpdateValue?.Invoke(value);
            }   
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