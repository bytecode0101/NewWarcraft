using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Assets.Script.Utils
{
    /// <summary>
    /// TODO: WIP or Deprecated?
    /// </summary>
    class IOManager
    {
        /// <summary>
        /// Saving serialized object
        /// </summary>
        /// <typeparam name="T">The generic object to be passed</typeparam>
        /// <param name="objectToSave">The is the object to be saved</param>
        public void Save<T>(T objectToSave)
        {
            var serializer = new XmlSerializer(typeof(T));

            using (var writer = XmlWriter.Create("out.xml", new XmlWriterSettings()
            {
                Indent = true,
                IndentChars = " "
            }))
            {
                serializer.Serialize(writer, this);
            }
        }

        /// <summary>
        /// Loading the serialized data into an object of type T
        /// </summary>
        /// <typeparam name="T">The generic object that is expected</typeparam>
        /// <param name="path">The path to the file that we want to load</param>
        /// <returns>The generic object that is expected</returns>
        public T Deserialize<T>(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StreamReader reader = new StreamReader(path);
            reader.ReadToEnd();
            var res = (T)serializer.Deserialize(reader);
            reader.Close();
            return res;
        }
    }
}
