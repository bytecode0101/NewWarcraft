using System;
using Assets.Script.States;
using UnityEngine;

internal class MovePlayState : IState
{
    private Main main;

    public MovePlayState(Main main)
    {
        this.main = main;
    }

    public void DoState()
    {
        var pawnPos = main.boardPawns[GameSettings.playerTurn].GetComponent<SpaceData>();
        var currPos = main.hit.transform.GetComponent<SpaceData>();

        // filled collide - to place in a switch | state area
        if (main.hit.transform.CompareTag("onBoardElement") &&
            !main.hit.transform.name.Contains("Filled3D"))
        {

            if (!(pawnPos.X == currPos.X && pawnPos.Y == currPos.Y) &&
                (((pawnPos.X + 1 == currPos.X || pawnPos.X - 1 == currPos.X) && pawnPos.Y == currPos.Y) ||
                ((pawnPos.Y + 1 == currPos.Y || pawnPos.Y - 1 == currPos.Y) && pawnPos.X == currPos.X)))
            {
                Debug.Log("Got here");
                var vec = new Vector3((currPos.X - pawnPos.X) * main.tileDistanceX,
                                                                 0,
                                                                 (currPos.Y - pawnPos.Y) * main.tileDistanceY);

                main.playerNotMoving = false;
                main.playerMoveTarget = vec;
                main.playerStartPoint = main.boardPawns[GameSettings.playerTurn].transform.position;

                pawnPos.X = currPos.X;
                pawnPos.Y = currPos.Y;

                // to use broadcast or navigate in node from diceThrower to the main since they are siblings
                GameSettings.diceMoves--;

                //main.debugDiceMovementObj.text = GameSettings.diceMoves.ToString();

                var currHitState = main.hit.transform.GetComponent<MeshRenderer>().enabled;
                if (!main.hit.transform.name.Contains("Base3D") && currHitState)
                {
                    main.ManageHitSpace(main.hit);
                }
                if (currHitState)
                {
                    main.DoPlayerUpdate(main.hit);
                }


                main.CheckBattle();

                if (GameSettings.diceMoves == 0 && GameSettings.IsGameOn)
                {
                    GameSettings.playerTurn = (++GameSettings.playerTurn) % GameSettings.totalPlayers;

                    Camera.main.GetComponent<StandardAssetFollowTarget>().target = main.boardPawns[GameSettings.playerTurn].transform;

                }

                if (!GameSettings.IsGameOn)
                {
                    Debug.Log("Game Ended! Someone won");
                    main.DiceGui.SetActive(false);
                    main.wonGui.SetActive(true);
                    foreach (var item in main.boardPawns)
                    {
                        item.SetActive(false);
                    }
                }

            }
        }
    }
}