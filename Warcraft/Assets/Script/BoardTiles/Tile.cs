using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using UnityEngine;

namespace Assets.Script.BoardTiles
{
    /// <summary>
    /// A given space / tile on the board; The tile can have empty, resource [with water, fire etc], base etc
    /// </summary>
    public class Tile
    {
        // A quick AHA. Place things as public; internal or something else will not work as expected

        [XmlAttribute("value")]
        public string value;

        [XmlAttribute("isABaseTile")]
        public bool isABaseTile;

        [XmlArray("TileResChildren"), XmlArrayItem("TileResChild")]
        public TileResChild[] TileResChildren;
        
        [JsonIgnore, XmlIgnore]
        public GameObject gameObject;
    }
}
