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
    class SerializerXML<T> : ISourceSerializable<T> where T : class
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

        public T Load(string path)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(path, FileMode.Open))
            {
                return (T)serializer.Deserialize(stream);
            }
        }
    }
}
