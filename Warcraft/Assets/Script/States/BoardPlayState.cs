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
            ReadMap();
        }

        private void ReadMap()
        {
            if (main.currentBoardMap == null)
            {
                var fileMap = new FileMap<TileContainer>(main.currentBoardMapPath, new TileContainer());
                var currentStringMap = fileMap.GetMap();
                width = currentStringMap.width;
                for (int i = 0; i < currentStringMap.Tiles.Length; i++)
                {
                    InstantiateGameObject(currentStringMap.Tiles[i], i);
                }

                var assetMap = new AssetMap(currentStringMap);
                //var currentBoardMap = assetMap.GetMap();
            }
        }

        private void InstantiateGameObject(Tile tile, int i)
        {
            float tileDistanceX = GameSettings.DistanceX;
            float tileDistanceY = GameSettings.DistanceY;
            float offsetX = 5;
            float offsetY = 5;
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
                //UpdateTile(row, cell, tempObj);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="cell"></param>
        /// <param name="tempObj"></param>
        private void UpdateTile(int row, int cell, GameObject tempObj)
        {
            main.currentBoardMap[row][cell] = tempObj;
        }
    }
}
