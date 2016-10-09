using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.Interfaces;
using System.Xml.Serialization;
using System.IO;
using Assets.Script.UtilsIO;
using Assets.Script.BoardTiles;

namespace Assets.Script
{
    class FileMap<T> : IMapable<T> where T : class
    {
        string str;
        SerializerHandler<T> serializer;
        T map;
        T headClass;
                
        public FileMap(string path, object headClass)
        {
            str = path;
            SerializerChainInit();
            MakeMap();
        }
        
        private void SerializerChainInit()
        {
            serializer = new JsonSerializerHandler<T>();
            SerializerHandler<T> sxml = new XmlSerializerHandler<T>();
            serializer.NextSource = sxml;
        }

        public void MakeMap()
        {
            MakeMap(str);
        }

        public void MakeMap(string str)
        {
            var path = string.Format("{0}{1}{2}", Application.dataPath, GameSettings.BoardPath, str);

            map = serializer.ManageLoad(SourceType.XML, path, headClass);
        }

        public T GetMap()
        {
            return map;
        }
    }
}
