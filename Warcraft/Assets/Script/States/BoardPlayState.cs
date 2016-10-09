using Assets.Script.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.BoardTiles;

namespace Assets.Script.States
{
    class BoardPlayState : MonoBehaviour, IState
    {
        private Dictionary<string, GameObject> prefabs;
        Main main;
        private int width;

        public BoardPlayState(Main main)
        {
            this.main = main;
            InitPrefabs();
        }

        private void InitPrefabs()
        {
            prefabs = new Dictionary<string, GameObject>();

            foreach (var prefabTile in main.prefabTiles)
            {
                prefabs.Add(prefabTile.name.ToLower(), prefabTile);
            }
        }

        public void DoState()
        {
            ReadMap(main.currentBoardMap,
                    main.currentBoardMapPath);
        }

        private void ReadMap(List<List<GameObject>> currentBoardMap,
                            string currentBoardMapPath)
        {
            if (currentBoardMap == null)
            {
                var fileMap = new FileMap<BoardTiles.TileContainer>(currentBoardMapPath, new BoardTiles.TileContainer());
                var currentStringMap = fileMap.GetMap();
                width = currentStringMap.width;
                for (int i = 0; i < currentStringMap.Tiles.Length; i++)
                {
                    currentBoardMap.Add(InstantiateGameObject(currentStringMap.Tiles[i], i));
                }

                var assetMap = new AssetMap(currentStringMap);
                //var currentBoardMap = assetMap.GetMap();
            }
        }

        private GameObject InstantiateGameObject(BoardTiles.Tile tile, int i)
        {
            float tileDistanceX = GameSettings.DistanceX;
            float tileDistanceY = GameSettings.DistanceY;
            int offsetX = 0;
            int offsetY = 0;
            float onx = tileDistanceX;
            float ony = tileDistanceY;
            
            if (prefabs.Keys.Contains(tile.value))
            {
                var prefab = prefabs[tile.value];
                var tempObj = (GameObject)Instantiate(prefab);
                tempObj.transform.SetParent(main.boardHolder.transform);
                var row = i / width;
                var cell = i % width;
                tempObj.transform.localPosition = new Vector3(row * onx + offsetX, tempObj.transform.localScale.y + .05f, cell * ony + offsetY);

                tempObj.GetComponent<SpaceData>().X = row;
                tempObj.GetComponent<SpaceData>().Y = cell;

                return tempObj;
            }
            return null;
        }
    }
}
