using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Script.BoardTiles
{
    /// <summary>
    /// A space container / tile container for the board. Used to populate board ( conversion from serialized / deserialized to gameObject is done -JUST- partially with the help of this)
    /// </summary>
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