using Assets.Script.BoardTiles;

namespace Assets.Script.UtilsIO
{
    internal interface ISourceSerializable
    {
        bool Save(string path, object toSerialize);
        TileContainer Load(string path);
    }
}