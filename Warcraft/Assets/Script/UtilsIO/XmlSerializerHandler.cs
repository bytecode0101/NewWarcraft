using Assets.Script.BoardTiles;
using System;

namespace Assets.Script.UtilsIO
{
    internal class XmlSerializerHandler<T> : SerializerHandler<T> where T : class
    {
        // could use a delegate as it stands because they look the same
        // however that might change for a different source implementation
        // still pondering about this

        public override T ManageLoad(SourceType sourceType, string path, T headClass)
        {
            if (sourceType != SourceType.XML)
            {
                if (NextSource != null)
                {
                    return NextSource.ManageLoad(sourceType, path, headClass);
                }
            }
            else
            {
                var srxml = new SerializerXML();
                return srxml.Load(path) as T;
            }

            return null;
        }

        public override bool ManageSave(SourceType sourceType, string path, T headClass)
        {
            if (sourceType != SourceType.XML)
            {
                if (NextSource != null)
                {
                    return ManageSave(sourceType, path, headClass);
                }
            }
            else
            {
                var srxml = new SerializerXML();
                return srxml.Save(path, this);
            }

            return false;
        }
    }
}