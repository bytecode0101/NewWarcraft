using Assets.Script.BoardTiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Assets.Script.UtilsIO.Serializer
{
    /// <summary>
    /// Saving | Loading serialized object
    /// </summary>
    /// <typeparam name="T">The generic object to be used for save / load</typeparam>
    class SerializerXML<T> : ISourceSerializable<T> where T : class
    {

        /// <summary>
        /// Saving serialized object
        /// </summary>
        /// <param name="path">The path where to save data</param>
        /// <param name="toSerialize">Data to serialize</param>
        public void Save(string path, T toSerialize)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(path, FileMode.Create))
            {
                serializer.Serialize(stream, toSerialize);
            }
        }

        /// <summary>
        /// Loading the serialized data into an object of type T
        /// </summary>
        /// <typeparam name="T">The generic object that is expected</typeparam>
        /// <param name="path">The path to the file that we want to load</param>
        /// <returns>The generic object that is expected</returns>
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
