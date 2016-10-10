using Assets.Script.BoardTiles;

namespace Assets.Script.UtilsIO.Serializer
{
    // using the following tutorial / code ( - among others - and, thanks to):
    // http://wiki.unity3d.com/index.php?title=Saving_and_Loading_Data:_XmlSerializer

    /// <summary>
    /// Saving | Loading serialized object
    /// </summary>
    /// <typeparam name="T">The generic object to be used for save / load</typeparam>
    internal interface ISourceSerializable<T> where T : class
    {
        void Save(string path, T toSerialize);
        T Load(string path);
    }
}