using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Script.BoardTiles
{
    [XmlRoot("XMLMap")]
    public class TileContainer
    {
        [XmlAttribute("width")]
        public int width;

        [XmlAttribute("height")]
        public int height;

        [XmlArray("Tiles"), XmlArrayItem("Tile")]
        public Tile[] Tiles;
    }
}