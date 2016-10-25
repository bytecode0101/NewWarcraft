using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Script
{
    /// <summary>
    /// Used to keep board data. TODO: think of a better name. the data at the end seems redundant
    /// </summary>
    public class BoardData
    {
        internal static string BoardPath = "/StreamingAssets/Maps/";

        internal int Width { get; private set; }
        internal int Height { get; private set; }
        internal float TileDistanceX { get; set; }
        internal float TileDistanceY { get; set; }
        internal bool IsBoardInitiated { get; set; }
        internal string CurrentBoardMapPath { get; set; }
        internal List<Point> BaseTiles { get; private set; }

        public BoardData(float tileDistanceX = 10f, float tileDistanceY = 10f)
        {
            BaseTiles = new List<Point>();

            TileDistanceX = tileDistanceX;
            TileDistanceY = tileDistanceY;
            IsBoardInitiated = false;
            CurrentBoardMapPath = "map001.xml";
        }

        internal void SetBoardDimensions(int width, int height)
        {
            Width = width;
            Height = height;
        }
        
    }
}
