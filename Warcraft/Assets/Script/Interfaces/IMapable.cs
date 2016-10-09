using System.Collections.Generic;

interface IMapable<T>
{
    void MakeMap();
    void MakeMap(string str);
    T GetMap();
}