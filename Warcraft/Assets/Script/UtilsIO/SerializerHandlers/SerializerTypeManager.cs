using Assets.Script.UtilsIO.Serializer;
using System;

namespace Assets.Script.UtilsIO.SerializerHandlers
{
    /// <summary>
    /// Decide which IO serializer to provide based on Source Type (xml, json, etc)
    /// </summary>
    internal class SerializerTypeManager
    {
        /// <summary>
        /// Get the Serializer (IO operations) based on the source type (xml, json, etc)
        /// </summary>
        /// <typeparam name="T">The object to place in or to save to the (de)serialize operation</typeparam>
        /// <param name="sourceType">The file type (xml, json etc)</param>
        /// <returns>returns the Serializer (IO operations)</returns>
        internal ISourceSerializable<T> GetByType<T>(SourceType sourceType) where T : class
        {
            switch (sourceType)
            {
                case SourceType.JSON:
                    return new SerializerJson<T>();
                case SourceType.XML:
                    return new SerializerXML<T>();                    
                default:
                    throw (new Exception("serialized type not defined"));
            }
        }
                
    }
}