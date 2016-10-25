using Assets.Script.BoardTiles;
using Assets.Script.UtilsIO.SerializerHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Script.ManagerMaps
{
    /// <summary>
    /// Create the asset representation of the file based map
    /// </summary>
    internal class AssetMap
    {
        /// <summary>
        /// the translator for string elements and ingame elements
        /// </summary>
        private Dictionary<string, GameObject> prefabs;

        private int width;
        private int height;

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

        /// <summary>
        /// Create the asset representation of the file based map
        /// </summary>
        public void MakeMap()
        {
            width = container.width;
            height = container.height;
            main.gameSettings.BoardData.SetBoardDimensions(container.width, container.height);

            for (int i = 0; i < container.Tiles.Length; i++)
            {
                MarkBaseOnBoard(container.Tiles[i].isABaseTile, i);

                InstantiateGameObject(container.Tiles[i], i);
            }
            
        }

        /// <summary>
        /// A shorthand element to save and quickly access the safe houses / bases found on the map
        /// </summary>
        /// <param name="isABaseTile">Check if the current tile was marked as a safe house / base</param>
        /// <param name="i">The index</param>
        private void MarkBaseOnBoard(bool isABaseTile, int i)
        {
            if (isABaseTile)
            { 
                main.gameSettings.BoardData.BaseTiles.Add(new Point(i / width, i % width));
            }
        }

        /// <summary>
        /// Get the current map object
        /// </summary>
        /// <returns>Returns an object representing the data from the file source; has the associated gObjects attached</returns>
        public TileContainer GetMap()
        {
            return container;
        }
        
        /// <summary>
        /// Instantiate the associated game object for each element found in the map file
        /// </summary>
        /// <param name="tile">The current element from the list of tiles</param>
        /// <param name="i">At this index from the map</param>
        public void InstantiateGameObject(Tile tile, int i)
        {
            float tileDistanceX = main.gameSettings.BoardData.TileDistanceX;
            float tileDistanceY = main.gameSettings.BoardData.TileDistanceY;

            float offsetPosX = main.gameSettings.BoardData.TileDistanceX / 2;
            float offsetPosY = .5f;
            float offsetPosZ = main.gameSettings.BoardData.TileDistanceY / 2;

            float posOnX = tileDistanceX;
            float PosOnZ = tileDistanceY;

            if (prefabs.Keys.Contains(tile.value))
            {
                var prefab = prefabs[tile.value];
                var tempObj = (GameObject)GameObject.Instantiate(prefab);

                tempObj.transform.SetParent(boardHolder.transform);

                var row = i / width;
                var cell = i % width;

                tempObj.transform.localPosition = new Vector3(cell * PosOnZ + offsetPosZ, tempObj.transform.localScale.y + offsetPosY, row * posOnX + offsetPosX);

                tempObj.GetComponent<SpaceData>().Y = row;
                tempObj.GetComponent<SpaceData>().X = cell;

                container.Tiles[row * cell].gameObject = tempObj;

            }
        }
        
        /// <summary>
        /// Tie file string elements to the representative prefabs elements
        /// </summary>
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