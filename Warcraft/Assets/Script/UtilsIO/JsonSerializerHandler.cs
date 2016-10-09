using Assets.Script.BoardTiles;
using System;

namespace Assets.Script.UtilsIO
{
    internal class JsonSerializerHandler<T> : SerializerHandler<T> where T : class
    {
        public override T ManageLoad(SourceType sourceType, string path, T headClass)
        {
            if (sourceType != SourceType.JSON)
            {
                if (NextSource != null)
                {
                    return NextSource.ManageLoad(sourceType, path, headClass);
                }
            }
            else
            {
                var srjson = new SerializerJson();
                return srjson.Load(path) as T;
            }

            return null;
        }

        public override bool ManageSave(SourceType sourceType, string path, T headClass)
        {
            if (sourceType != SourceType.JSON)
            {
                if (NextSource != null)
                {
                    return NextSource.ManageSave(sourceType, path, headClass);
                }
            }
            else
            {
                var srjson = new SerializerJson();
                return srjson.Save(path, this);
            }

            return false;
        }
    }
}