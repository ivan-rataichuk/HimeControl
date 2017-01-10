using HomeControlModel.Settings;
using System.Runtime.Serialization;
using System.Xml;
using static HomeControlModel.Events.HomeControlEvents;

namespace HomeControl.Models.BaseClasses
{
    [DataContract]
    public class BaseDevice
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Type { get; private set; }

        [DataMember]
        public bool IsOn { get; private set; }

        public event UpdatePowerConsumption ConsumptionChanged;

        [DataMember]
        private readonly int powerConsumption; // opened to test

        public BaseDevice()
        {

        }

        public BaseDevice(string Type)
        {
            this.Type = Type;
            SetPowerConsumption(this.Type, out powerConsumption);
            ConsumptionChanged += PowerMeter.GetInstance().UpdatePowerConsumption;
        }

        public void Switch()
        {
            IsOn = !IsOn;
            if (IsOn)
            {
                ConsumptionChanged?.Invoke(powerConsumption);
            }
            else
            {
                ConsumptionChanged?.Invoke(-powerConsumption);
            }
            
        }

        private void SetPowerConsumption(string type, out int powerConsumption)
        {
            powerConsumption = 0;

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
                        int consumption;

                        if (int.TryParse(xNode.SelectSingleNode("power-consumption").InnerText, out consumption))   
                        {
                            powerConsumption = consumption;
                        }
                        return;
                    }
                }
            }
        }
    }
}