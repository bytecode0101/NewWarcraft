using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.BoardTiles;
using Assets.Script.ManagerMaps;

namespace Assets.Script.States
{
    class BoardInitState : IState
    {
        Main main;
        
        public BoardInitState(Main main)
        {
            this.main = main;
        }

        public void DoState()
        {
            if (main.gameSettings.BoardData.IsBoardInitiated != true)
            {
                ReadMap();
            }
            main.ChangeState(main.currentStateType, StateType.GameInitState);
        }
        
        /// <summary>
        /// read from the desired file the map layout for the board
        /// </summary>
        private void ReadMap()
        {
            var fileMap = new FileMap<TileContainer>(main.gameSettings.BoardData.CurrentBoardMapPath, new TileContainer());
            var currentFileMap = fileMap.GetMap();

            InitFromFileMap(currentFileMap);

            main.gameSettings.BoardData.IsBoardInitiated = true;
        }

        /// <summary>
        /// associate the game objects to the file created container.
        /// </summary>
        /// <param name="currentFileMap"></param>
        private void InitFromFileMap(TileContainer currentFileMap)
        {
            //var assetMap = 
                new AssetMap(currentFileMap, main.boardObjectsHolder.transform, main);
        }
    }
}
