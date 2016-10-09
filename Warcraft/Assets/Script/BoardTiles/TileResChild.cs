using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Script.BoardTiles
{
    public class TileResChild
    {
        [XmlAttribute("value")]
        public string value;

        [XmlAttribute("weight")]
        public int weight;

        [XmlAttribute("randomCoeficient")]
        public int randomCoeficient;
    }
}
