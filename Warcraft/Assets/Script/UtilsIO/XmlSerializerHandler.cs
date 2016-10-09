using Assets.Script.BoardTiles;
using System;

namespace Assets.Script.UtilsIO
{
    internal class XmlSerializerHandler<T> : SerializerHandler<T> where T : class
    {
        // could use a delegate as it stands because they look the same
        // however that might change for a different source implementation
        // still pondering about this

        public override T Load(SourceType sourceType, string path, T headClass)
        {
            if (sourceType != SourceType.XML)
            {
                if (NextSource != null)
                {
                    return NextSource.Load(sourceType, path, headClass);
                }
            }
            else
            {
                var srxml = new SerializerXML<T>();
                return srxml.Load(path) as T;
            }

            return null;
        }

        public override bool Save(SourceType sourceType, string path, T headClass)
        {
            if (sourceType != SourceType.XML)
            {
                if (NextSource != null)
                {
                    return Save(sourceType, path, headClass);
                }
            }
            else
            {
                var srxml = new SerializerXML<T>();
                return srxml.Save(path, this);
            }

            return false;
        }
    }
}