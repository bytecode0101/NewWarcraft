using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using Assets.Script.UtilsIO;
using Assets.Script.BoardTiles;
using Assets.Script.UtilsIO.SerializerHandlers;
using Assets.Script.States;

namespace Assets.Script.ManagerMaps
{
    /// <summary>
    /// This class manages the IO for data (in xml, json)
    /// TODO: Add an additional method for SaveMap() to also implement that
    /// </summary>
    /// <typeparam name="T">The object type in which the content is saved or in which it is placed</typeparam>
    class FileMap<T> : IMapable<T> where T : class
    {
        string str;
        SerializerHandler<T> serializer;
        T map;
        T headClass;  

        /// <summary>
        /// The file map constructor
        /// </summary>
        /// <param name="path">the file path</param>
        /// <param name="headClass">the object in which to place or get from data</param>
        public FileMap(string path, object headClass)
        {
            str = path;
            SerializerChainInit();
            MakeMap();
        }
        
        /// <summary>
        /// Set the "chain of responsability" regarding the order in which to -attempt- the file types read / save
        /// </summary>
        private void SerializerChainInit()
        {
            serializer = new JsonSerializerHandler<T>();
            SerializerHandler<T> sxml = new XmlSerializerHandler<T>();
            serializer.NextSource = sxml;
        }

        /// <summary>
        /// Make the map from given file source
        /// </summary>
        public void MakeMap()
        {
            MakeMap(str);
        }

        /// <summary>
        /// Make the map from given file source
        /// </summary>
        /// <param name="str">A file path for a file different than the one given in ctor</param>
        public void MakeMap(string str)
        {
            var path = string.Format("{0}{1}{2}", Application.dataPath, GameSettings.BoardPath, str);

            map = serializer.Load(SourceType.XML, path, headClass);
        }

        /// <summary>
        /// Return the generated map from the said file source
        /// </summary>
        /// <returns>Return the generated map from the said file source</returns>
        public T GetMap()
        {
            return map;
        }
    }
}
