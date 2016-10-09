using Assets.Script.BoardTiles;

namespace Assets.Script.UtilsIO
{
    internal interface ISourceSerializable<T>
    {
        bool Save(string path, object toSerialize);
        T Load(string path);
    }
}