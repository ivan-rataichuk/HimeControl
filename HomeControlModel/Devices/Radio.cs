using HomeControl.Models.BaseClasses;
using HomeControl.Models.Intefaces.Devices;
using HomeControl.Models.Intefaces.Modules;
using HomeControl.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeControl.Models.Modules;
using System.Runtime.Serialization;

namespace HomeControl.Models.Devices
{
    [Device("Radio")]
    [DataContract]
    public class Radio : BaseDevice, ISelectable, ISoundable
    {
        [DataMember]
        public string SoundSignal { get; private set; }

        [DataMember]
        public int Volume { get; private set; }

        public IEnumerable<string> Channels
        {
            get { return tuner.GetOptions(); }
        }

        public string Channel
        {
            set { tuner.Select(value); }
        }

        [DataMember]
        private ISelector tuner;

        [DataMember]
        private IRegulator speakers;

        public Radio() : base("Radio")
        {
            tuner = new Tuner(this.Type);
            tuner.UpdateSignal += UpdateSignal;
            speakers = new Regulator(this.Type);
            speakers.UpdateValue += UpdateCurrentVolume;
        }

        public void SetVolume(int volume)
        {
            speakers.SetValue(volume);
        }

        private void UpdateCurrentVolume(int volume)
        {
            Volume = volume;
        }

        private void UpdateSignal(string signal)
        {
            SoundSignal = signal;
        }
    }
}