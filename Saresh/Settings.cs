using System;
using System.Xml.Serialization;

namespace Saresh
{
    [Serializable]
    [XmlRoot("Settings")]
    public class Settings
    {
        [XmlElement("BlockSetDelay")]
        public int BlockSetDelay { get; set; } = 50;

        public Settings() {}
    }
}
