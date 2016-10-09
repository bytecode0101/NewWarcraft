using Assets.Script.BoardTiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Script.UtilsIO
{    
    // using the following tutorial / code (and, thanks to):
    // http://wiki.unity3d.com/index.php?title=Saving_and_Loading_Data:_XmlSerializer
    class SerializerXML : ISourceSerializable
    {
        public bool Save(string path, object toSerialize)
        {
            var serializer = new XmlSerializer(typeof(TileContainer));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, toSerialize);
                return true;
            }
        }

        public TileContainer Load(string path)
        {
            var serializer = new XmlSerializer(typeof(TileContainer));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return serializer.Deserialize(stream) as TileContainer;
            }
        }

        //Loads the xml directly from the given string. Useful in combination with www.text.
        public TileContainer LoadFromText(string text)
        {
            var serializer = new XmlSerializer(typeof(TileContainer));
            return serializer.Deserialize(new StringReader(text)) as TileContainer;
        }
    }
}
