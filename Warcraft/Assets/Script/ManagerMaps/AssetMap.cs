using Assets.Script.BoardTiles;
using Assets.Script.UtilsIO.SerializerHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Script.ManagerMaps
{
    internal class AssetMap
    {
        private Dictionary<string, GameObject> prefabs;

        private int width;

        TileContainer container;
        Transform boardHolder;

        Main main;
        
        public AssetMap(TileContainer currentFileMap, Transform boardHolder, Main main)
        {
            container = currentFileMap;
            this.boardHolder = boardHolder;
            this.main = main;

            InitPrefabs();
            MakeMap();
        }

        public void MakeMap()
        {
            width = container.width;

            for (int i = 0; i < container.Tiles.Length; i++)
            {
                InstantiateGameObject(container.Tiles[i], i);
            }
            
        }
        
        public TileContainer GetMap()
        {
            return container;
        }
        
        public void InstantiateGameObject(Tile tile, int i)
        {
            float tileDistanceX = GameSettings.DistanceX;
            float tileDistanceY = GameSettings.DistanceY;

            float offsetPosX = GameSettings.DistanceX / 2;
            float offsetPosY = GameSettings.DistanceX / 20;
            float offsetPosZ = GameSettings.DistanceY / 2;

            float posOnX = tileDistanceX;
            float PosOnZ = tileDistanceY;

            if (prefabs.Keys.Contains(tile.value))
            {
                var prefab = prefabs[tile.value];
                var tempObj = (GameObject)GameObject.Instantiate(prefab);

                tempObj.transform.SetParent(boardHolder.transform);

                var row = i / width;
                var cell = i % width;

                tempObj.transform.localPosition = new Vector3(row * posOnX + offsetPosX, tempObj.transform.localScale.y + offsetPosY, cell * PosOnZ + offsetPosZ);

                tempObj.GetComponent<SpaceData>().X = row;
                tempObj.GetComponent<SpaceData>().Y = cell;

                container.Tiles[row * cell].gameObject = tempObj;

            }
        }
        
        private void InitPrefabs()
        {
            prefabs = new Dictionary<string, GameObject>();

            foreach (var prefabTile in main.prefabTiles)
            {
                prefabs.Add(prefabTile.name.ToLower(), prefabTile);
            }
        }

    }
}