using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script
{
    internal class AssetMap<T> : IMapable<T>
    {
        private string mapString;
        T map;
        
        public AssetMap(T currentStringMap)
        {
            this.map = currentStringMap;
            MakeMap();
        }

        public void MakeMap()
        {
            throw new NotImplementedException();
        }

        public void MakeMap(string str)
        {
            throw new NotImplementedException();
        }

        public T GetMap()
        {
            throw new NotImplementedException();
        }
    }
}