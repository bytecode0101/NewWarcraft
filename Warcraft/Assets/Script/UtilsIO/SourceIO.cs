using Assets.Script.BoardTiles;
using System;

namespace Assets.Script.UtilsIO
{
    abstract class SourceIO<T>
    {
        public SourceIO<T> NextSource { get; set; }
        public abstract T ManageLoad(SourceType sourceType, string path, T headClass);
        public abstract bool ManageSave(SourceType sourceType, string path, T headClass);
    }
}