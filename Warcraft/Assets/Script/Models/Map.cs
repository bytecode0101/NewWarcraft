using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Assets.Script.Models
{
    public class Map
    {
        public string[] mapRows = new string[16];
        private string[,] map = new string[16, 22];

        public Map()
        {

        }

        public void UpdateCell(int x, int y, string value)
        {
            try
            {
                map[x, y] = value;
            }
            catch (Exception e)
            {
                var str = e.Message;
            }
        }

       
    }
}
