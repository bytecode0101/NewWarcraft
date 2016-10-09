using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Script.BoardTiles
{
    public class Tile
    {
        [XmlAttribute("value")]
        public string value;

        [XmlArray("TileResChildren"), XmlArrayItem("TileResChild")]
        public TileResChild[] TileResChildren;
    }
}
