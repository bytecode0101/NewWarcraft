using Assets.Script.BoardTiles;
using System;

namespace Assets.Script.UtilsIO.SerializerHandlers
{
    abstract class SerializerHandler<T>
    {
        protected SerializerTypeManager serializerTypeManager;

        public SerializerHandler<T> NextSource { get; set; }

        public SerializerHandler()
        {
            serializerTypeManager = new SerializerTypeManager();
        }

        /// <summary>
        /// Load the data from the said path
        /// </summary>
        /// <param name="sourceType">The desired file type</param>
        /// <param name="path">The path to the said file</param>
        /// <param name="headClass">if this object is not the one needed use the head to traverse to the next one</param>
        /// <returns>Returns the loaded deserialized object</returns>
        public abstract T Load(SourceType sourceType, string path, T headClass);

        /// <summary>
        /// Save the data to the said path
        /// </summary>
        /// <param name="sourceType">The desired file type</param>
        /// <param name="path">The path for the said file</param>
        /// <param name="headClass">if this object is not the one needed use the head to traverse to the next one</param>
        /// <returns>was the save succesfull?</returns>
        public abstract bool Save(SourceType sourceType, string path, T headClass);
    }
}