using Assets.Script.BoardTiles;
using Assets.Script.UtilsIO.Serializer;
using System;

namespace Assets.Script.UtilsIO.SerializerHandlers
{
    /// <summary>
    /// Handling the serialized data [I am keeping separate in case additional things are needed for xml, json etc]
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class XmlSerializerHandler<T> : SerializerHandler<T> where T : class
    {
        SourceType mySourceType = SourceType.XML;

        /// <summary>
        /// Load the data from the said path
        /// </summary>
        /// <param name="sourceType">The desired file type</param>
        /// <param name="path">The path to the said file</param>
        /// <param name="headClass">if this object is not the one needed use the head to traverse to the next one</param>
        /// <returns>Returns the loaded deserialized object</returns>
        public override T Load(SourceType sourceType, string path, T headClass)
        {
            if (sourceType != mySourceType)
            {
                if (NextSource != null)
                {
                    return NextSource.Load(sourceType, path, headClass);
                }
            }
            else
            {
                var srxml = serializerTypeManager.GetByType<T>(sourceType);
                return srxml.Load(path) as T;
            }

            return null;
        }

        /// <summary>
        /// Save the data to the said path
        /// </summary>
        /// <param name="sourceType">The desired file type</param>
        /// <param name="path">The path for the said file</param>
        /// <param name="headClass">if this object is not the one needed use the head to traverse to the next one</param>
        /// <returns>was the save succesfull?</returns>
        public override bool Save(SourceType sourceType, string path, T headClass)
        {
            if (sourceType != mySourceType)
            {
                if (NextSource != null)
                {
                    return Save(sourceType, path, headClass);
                }
            }
            else
            {
                var srxml = serializerTypeManager.GetByType<T>(sourceType);
                srxml.Save(path, headClass);
                return true;
            }

            return false;
        }
    }
}