using Assets.Script.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.States
{
    class BoardPlayState : IState
    {

        Main main;
        public BoardPlayState(Main main)
        {
            this.main = main;
            
        }

        public void DoState()
        {
            ReadMap(main.currentBoardMap, 
                    main.prefabBaseTile,
                    main.prefabDangerTile,
                    main.prefabEmptyTile,
                    main.prefabFilledTile,
                    main.prefabMercenaryTile,
                    main.prefabResourceTile,
                    main.currentBoardMapPath);
        }

        private void ReadMap(List<List<GameObject>> currentBoardMap, 
                            GameObject prefabBaseTile,
                            GameObject prefabDangerTile,
                            GameObject prefabEmptyTile,
                            GameObject prefabFilledTile,
                            GameObject prefabMercenaryTile,
                            GameObject prefabResourceTile,
                            string currentBoardMapPath)
        {
            if (currentBoardMap == null)
            {
                var fileMap = new FileMap<BoardTiles.TileContainer>(currentBoardMapPath, new BoardTiles.TileContainer());
                var currentStringMap = fileMap.GetMap();
                var assetMap = new AssetMap(currentStringMap);
                //var currentBoardMap = assetMap.GetMap();
            }
        }
        
    }
}
