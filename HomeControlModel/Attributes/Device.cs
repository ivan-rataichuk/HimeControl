using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeControl.Models.Attributes
{
    public class Device : Attribute
    {
        private string name;

        public Device(string name)
        {
            this.name = name;
        }

        public string GetName()
        {
            return name;
        }
    }
}