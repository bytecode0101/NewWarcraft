using Assets.Script.BoardTiles;
using System;

namespace Assets.Script.UtilsIO
{
    abstract class SerializerHandler<T>
    {
        public SerializerHandler<T> NextSource { get; set; }
        public abstract T Load(SourceType sourceType, string path, T headClass);
        public abstract bool Save(SourceType sourceType, string path, T headClass);
    }
}