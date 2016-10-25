using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Script.BoardTiles;
using Assets.Script.ManagerMaps;

namespace Assets.Script.States
{
    /// <summary>
    /// Used for loading game data after the boardState is done. 
    /// Things like show board, show players, show dices etc
    /// TODO: Maybe give it a better name
    /// </summary>
    public class GameInitState : IState
    {
        Main main;

        int boardWidth, boardHeight;
        float boardOffsetOnX, boardOffsetOnY;

        public GameInitState(Main main)
        {
            this.main = main;
        }

        public void DoState()
        {
            boardWidth = main.gameSettings.BoardData.Width;
            boardHeight = main.gameSettings.BoardData.Height;

            DoBoardFloorInit();
            
            // add a players load state or follow this practice instead (where the initstate has a board constructor, a game constructor, a playerconstructor, panels and so own. I place this question because it seems that a lot of non states end up as being states
            main.PlayersManager = new PlayersManager(main, boardWidth, boardHeight);
            
            main.ChangeState(main.currentStateType, StateType.IdlePlayState);
        }
        
        /// <summary>
        /// Create the floor that is needed for the shared board
        /// </summary>
        private void DoBoardFloorInit()
        {
            main.boardFloor = GameObject.Instantiate(main.prefabBoardFloor);
            
            main.boardFloor.GetComponent<MeshRenderer>().material.mainTextureScale =
                new Vector2(boardWidth, boardHeight);

            main.boardFloor.transform.localScale = 
                new Vector3(boardWidth, main.boardFloor.transform.localScale.y, boardHeight);

            main.boardFloor.GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
