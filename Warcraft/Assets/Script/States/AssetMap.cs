using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    internal class AssetMap : IMapable<string>
    {
        private string mapString;
        List<List<string>> map;
        
        public AssetMap(List<List<string>> currentStringMap)
        {
            this.map = currentStringMap;
            MakeMap();
        }
        
        internal void MakeMap()
        {
            MakeMap(mapString);
        }
        internal void MakeMap(string path)
        {

        }

        internal List<List<string>> GetMap()
        {
            return GetMap(mapString);
        }

        internal List<List<string>> GetMap(string path)
        {
            return map;
        }

        void IMapable<string>.MakeMap()
        {
            throw new NotImplementedException();
        }

        void IMapable<string>.MakeMap(string str)
        {
            throw new NotImplementedException();
        }

        string IMapable<string>.GetMap()
        {
            throw new NotImplementedException();
        }
    }
}