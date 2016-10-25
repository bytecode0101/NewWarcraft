using System;
using System.Collections.Generic;
using Assets.Script.States;
using UnityEngine;

public class PlayersManager
{
    public List<Player> Players { get; set; }

    Main main;

    //int boardWidth, boardHeight;
    float boardOffsetOnX, boardOffsetOnY;
    private GameInitState gameInitState;
    private int boardWidth;
    private int boardHeight;
    
    public PlayersManager(Main main, int boardWidth, int boardHeight)
    {

        Players = new List<Player>();

        this.main = main;
        this.gameInitState = gameInitState;
        this.boardWidth = boardWidth;
        this.boardHeight = boardHeight;

        boardOffsetOnX = this.main.gameSettings.BoardData.TileDistanceX * (boardWidth - 1) / 2f;
        boardOffsetOnY = this.main.gameSettings.BoardData.TileDistanceY * (boardHeight - 1) / 2f;

        InitStartingPawns();
        
    }

    internal void InitStartingPawns()
    {
        int index = 0;
        int prefabPawnsCount = main.prefabPawns.Count;
        
        if( main.gameSettings.NumberOfPlayers <= main.prefabPawns.Count &&
            main.gameSettings.NumberOfPlayers <= main.gameSettings.BoardData.BaseTiles.Count)
        { 
            foreach (var basePoint in main.gameSettings.BoardData.BaseTiles)
            {
                var currPawn = DoPawnInit(main.prefabPawns[index], basePoint.X, basePoint.Y);
                Players.Add(new Player(currPawn));
                
                if (index == 0)
                {
                    Camera.main.transform.position = currPawn.transform.localPosition;
                }

                index++;
            }
        }
    }
    private GameObject DoPawnInit(GameObject prefabItem, int placedOnX, int placedOnY)
    {
        var pawnObject =
                (GameObject)GameObject.Instantiate(prefabItem,
                new Vector3(placedOnY * main.gameSettings.BoardData.TileDistanceX - boardOffsetOnX,
                0,
                placedOnX * main.gameSettings.BoardData.TileDistanceY - boardOffsetOnY),
                Quaternion.identity);
                
        return pawnObject;
    }

    internal GameObject GetCurrentActivePlayer()
    {
        // not the best choice for a method name. nor the best implementation
        return Players[main.gameSettings.playerTurn].Pawns[0].PawnObject;
    }

    internal void PawnsUpdate()
    {
        var currPawnObj = GetCurrentActivePlayer();
        var onX = main.playerStartPoint.x - currPawnObj.transform.position.x;
        var onZ = main.playerStartPoint.z - currPawnObj.transform.position.z;
        if (!main.playerNotMoving &&
            Math.Abs(onX) < main.gameSettings.BoardData.TileDistanceX &&
            Math.Abs(onZ) < main.gameSettings.BoardData.TileDistanceY)
        {
            currPawnObj.transform.Translate(main.playerMoveTarget * Time.deltaTime * main.gameSettings.PawnBaseMovementSpeed);

        }
        else
        {
            if (!main.playerNotMoving)
            {
                //TODO:remove magic values; see the formula for player position on gameInit
                currPawnObj.transform.position = new Vector3(-boardOffsetOnX + currPawnObj.GetComponent<SpaceData>().X * main.gameSettings.BoardData.TileDistanceX, currPawnObj.transform.position.y, -boardOffsetOnY + currPawnObj.GetComponent<SpaceData>().Y * main.gameSettings.BoardData.TileDistanceY);

                main.playerNotMoving = true;

                main.SetCurrentActivePawn();

            }
        }
    }


}