using Assets.Script.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.BoardTiles;

namespace Assets.Script.States
{
    class BoardPlayState : IState
    {
        private object game;
        Main main;
        private int width;

        public BoardPlayState(Main main)
        {
            this.main = main;
        }

        public void DoState()
        {
            ReadMap();
        }

        private void ReadMap()
        {
            if (main.currentBoardMap == null)
            {
                var fileMap = new FileMap<TileContainer>(main.currentBoardMapPath, new TileContainer());
                var currentStringMap = fileMap.GetMap();
                main.Width = currentStringMap.width;
                for (int i = 0; i < currentStringMap.Tiles.Length; i++)
                {
                    main.InstantiateGameObject(currentStringMap.Tiles[i], i);
                }

                var assetMap = new AssetMap(currentStringMap);
                //var currentBoardMap = assetMap.GetMap();
            }
        }
    }
}
