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
        SourceIO<T> headSource;
        T map;
        T headClass;
                
        public FileMap(string path, object headClass)
        {
            str = path;
            DoSourceInit();
            MakeMap();
        }
        
        private void DoSourceInit()
        {
            headSource = new SourceIOJson<T>();
            SourceIO<T> sxml = new SourceIOXML<T>();
            headSource.NextSource = sxml;
        }

        public void MakeMap()
        {
            MakeMap(str);
        }

        public void MakeMap(string str)
        {
            var path = string.Format("{0}{1}{2}", Application.dataPath, GameSettings.BoardPath, str);

            map = headSource.ManageLoad(SourceType.XML, path, headClass);
        }

        public T GetMap()
        {
            return map;
        }
    }
}
