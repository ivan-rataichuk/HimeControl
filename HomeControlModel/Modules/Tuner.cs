using HomeControl.Models.Intefaces.Modules;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using static HomeControlModel.Events.HomeControlEvents;
using HomeControlModel.Events;
using HomeControlModel.Settings;
using System.Runtime.Serialization;

namespace HomeControl.Models.Modules
{
    [DataContract]
    public class Tuner : ISelector
    {
        public event UpdateSignal UpdateSignal;

        private IEnumerable<Channel> channels;

        [DataMember]
        private string type;

        public Tuner()
        {
            UpdateChannels();
        }

        public Tuner(string type)
        {
            this.type = type;
            UpdateChannels();
        }

        public IEnumerable<string> GetOptions()
        {
            return from c in channels
                   select c.Name;
        }

        public void Select(string channel)
        {
            if (!channel.Any())
            {
                return;
            }

            UpdateSignal?.Invoke(channels.FirstOrDefault(c => c.Name == channel).Signal);
        }

        private void UpdateChannels()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(FilesLocation.channelsXML);
            XmlElement xNodes = xDoc.DocumentElement;

            List<Channel> updatedChannels = new List<Channel>();

            foreach (XmlNode xNode in xNodes)
            {
                if (xNode.Attributes.Count > 0)
                {
                    XmlNode attr = xNode.Attributes.GetNamedItem("type");
                    if ((attr != null) && (attr.Value == type))
                    {
                        Channel channel = new Channel();
                        channel.Name = xNode.SelectSingleNode("name").InnerText;
                        channel.Signal = xNode.SelectSingleNode("signal").InnerText;
                        updatedChannels.Add(channel);
                    }
                }
            }

            channels = updatedChannels;
        }
    }
}