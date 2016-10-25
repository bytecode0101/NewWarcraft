using System;
using Assets.Script.States;
using UnityEngine;

internal class MovePlayState : IState
{
    private Main main;
    SpaceData pawnPos, currPos;
    public MovePlayState(Main main)
    {
        this.main = main;
    }

    public void DoState()
    {
        pawnPos = main.PlayersManager.Players[main.gameSettings.playerTurn].Pawns[0].PawnObject.GetComponent<SpaceData>();
        currPos = main.boardElemHit.transform.GetComponent<SpaceData>();
        
        // filled collide - to place in a switch | state area
        if (CanPawnMoveOn())
        {

            if (CanDoBoardMovement())
            {
                Debug.Log("Got here");
                
                SetMovementActivity(GetPawnMoveTarget());

                SetDiceActivity();

                SetInteractionsActivity();

                //main.CheckBattle(); // make as state?

               // SetCurrentActivePawn();

                SetWiningTempState();

            }
        }
        
        main.ChangeState(main.currentStateType, StateType.IdlePlayState);
    }

    /// <summary>
    /// Decide if the board game (the short demo one) is done.
    /// TODO: Move parts of this in another state
    /// </summary>
    private void SetWiningTempState()
    {
        if (!main.gameSettings.IsGameOn)
        {
            Debug.Log("Game Ended! Someone won");
            main.DiceGui.SetActive(false);
            main.wonGui.SetActive(true);
            foreach (var item in main.PlayersManager.Players)
            {
                item.Pawns[0].PawnObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Decides what happens after the movement takes place
    /// TODO: move this in another state
    /// </summary>
    private void SetInteractionsActivity()
    {
        var currboardElemHitState = main.boardElemHit.transform.GetComponent<MeshRenderer>().enabled;
        if (!main.boardElemHit.transform.name.Contains("Base3D") && currboardElemHitState)
        {
            main.ManageboardElemHitSpace(main.boardElemHit);
        }
        if (currboardElemHitState)
        {
            main.DoPlayerUpdate(main.boardElemHit);
        }
    }

    /// <summary>
    /// Decrease the dice value after the move took place
    /// </summary>
    private void SetDiceActivity()
    {
        // to use broadcast or navigate in node from diceThrower to the main since they are siblings
        GameSettings.DiceMoves--;
        //main.debugDiceMovementObj.text = GameSettings.diceMoves.ToString();
    }

    /// <summary>
    /// Set Movement Activity
    /// </summary>
    /// <param name="target">the desired target</param>
    private void SetMovementActivity(Vector3 target)
    {

        main.playerNotMoving = false;
        main.playerMoveTarget = target;
        main.playerStartPoint = main.PlayersManager.Players[main.gameSettings.playerTurn].Pawns[0].PawnObject.transform.position;

        pawnPos.X = currPos.X;
        pawnPos.Y = currPos.Y;
    }

    /// <summary>
    /// Get the current target for the pawn
    /// </summary>
    /// <returns>Get the current target for the pawn</returns>
    private Vector3 GetPawnMoveTarget()
    {
        return new Vector3((currPos.X - pawnPos.X) * main.gameSettings.BoardData.TileDistanceX,
                            0,
                            (currPos.Y - pawnPos.Y) * main.gameSettings.BoardData.TileDistanceY);
    }

    /// <summary>
    /// can the pawn move on the current position? uses pawn coords
    /// </summary>
    /// <returns></returns>
    private bool CanDoBoardMovement()
    {
        return !(pawnPos.X == currPos.X && pawnPos.Y == currPos.Y) &&
               (((pawnPos.X + 1 == currPos.X || pawnPos.X - 1 == currPos.X) && pawnPos.Y == currPos.Y) ||
                ((pawnPos.Y + 1 == currPos.Y || pawnPos.Y - 1 == currPos.Y) && pawnPos.X == currPos.X));
    }

    /// <summary>
    /// can pawn move on the given board spot? uses checks for compareTag and compare filled
    /// </summary>
    /// <returns>can pawn move on the given board spot?</returns>
    private bool CanPawnMoveOn()
    {
        return main.boardElemHit.transform.CompareTag("onBoardElement") &&
        !main.boardElemHit.transform.name.Contains("Filled3D");
    }
}