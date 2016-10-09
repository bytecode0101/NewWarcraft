using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Assets.Script.UtilsIO;
using Assets.Script.BoardTiles;
using System.IO;

public class SerializerJson<T> : ISourceSerializable<T>
{
    public bool Save(string path, object toSerialize)
    {
        // TODO: maybe add a catch here
        string content = JsonConvert.SerializeObject(toSerialize);
        
        using (StreamWriter sw = new StreamWriter(path))
        {
            sw.Write(content);
            return true;
        }

    }

    public T Load(string path)
    {
        using (StreamReader sr = new StreamReader(path))
        {
            var content = sr.ReadToEnd();
            
            // TODO: maybe add a catch here
            return JsonConvert.DeserializeObject<T>(content);
        }        
    }
}
