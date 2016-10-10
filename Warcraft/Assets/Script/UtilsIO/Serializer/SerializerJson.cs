using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Assets.Script.UtilsIO;
using Assets.Script.BoardTiles;
using System.IO;

namespace Assets.Script.UtilsIO.Serializer
{
    /// <summary>
    /// Saving | Loading serialized object
    /// </summary>
    /// <typeparam name="T">The generic object to be used for save / load</typeparam>
    public class SerializerJson<T> : ISourceSerializable<T> where T : class
    {
        /// <summary>
        /// Saving serialized object
        /// </summary>
        /// <param name="path">The path where to save data</param>
        /// <param name="toSerialize">Data to serialize</param>
        public void Save(string path, T toSerialize)
        {
            // TODO: maybe add a catch here
            string content = JsonConvert.SerializeObject(toSerialize);
        
            using (StreamWriter sw = new StreamWriter(path))
            {
                sw.Write(content);
            }

        }

        /// <summary>
        /// Loading the serialized data into an object of type T
        /// </summary>
        /// <typeparam name="T">The generic object that is expected</typeparam>
        /// <param name="path">The path to the file that we want to load</param>
        /// <returns>The generic object that is expected</returns>
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
}
