using Assets.Script.BoardTiles;
using System;

namespace Assets.Script.UtilsIO
{
    internal class JsonSerializerHandler<T> : SerializerHandler<T> where T : class
    {
        public override T Load(SourceType sourceType, string path, T headClass)
        {
            if (sourceType != SourceType.JSON)
            {
                if (NextSource != null)
                {
                    return NextSource.Load(sourceType, path, headClass);
                }
            }
            else
            {
                var srjson = new SerializerJson<T>();
                return srjson.Load(path) as T;
            }

            return null;
        }

        public override bool Save(SourceType sourceType, string path, T headClass)
        {
            if (sourceType != SourceType.JSON)
            {
                if (NextSource != null)
                {
                    return NextSource.Save(sourceType, path, headClass);
                }
            }
            else
            {
                var srjson = new SerializerJson<T>();
                return srjson.Save(path, this);
            }

            return false;
        }
    }
}