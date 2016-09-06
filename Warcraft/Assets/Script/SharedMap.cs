using UnityEngine;
using System.Collections;

public class SharedMap {

    GameSettings gameSettings = new GameSettings();
    public Element[,] Spaces;
    internal int Width;
    internal int Height;

    public SharedMap(int width, int height)
    {
        Width = width;
        Height = height;
        Spaces = new Element[Width, Height];
    }

    public SharedMap()
    {
        Width = gameSettings.BoardWidth;
        Height = gameSettings.BoardHeight;
        Spaces = new Element[Width, Height];
    }
}
