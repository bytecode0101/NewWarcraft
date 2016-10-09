using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Assets.Script.Utils
{
    class IOManager
    {
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
