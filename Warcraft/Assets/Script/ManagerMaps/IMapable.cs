namespace Assets.Script.States
{
    interface IMapable<T>
    {
        void MakeMap();
        void MakeMap(string str);
        T GetMap();
    }
}