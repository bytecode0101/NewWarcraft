using System;
using Assets.Script.States;
using UnityEngine;

internal class IdlePlayState : IState
{
    private Main main;

    public IdlePlayState(Main main)
    {
        this.main = main;
    }

    public void DoState()
    {
        main.debugDiceMovement = GameSettings.DiceMoves;

        if (main.playerNotMoving)
        { 
            if (Input.GetMouseButtonDown(0) && main.playerNotMoving)
            {
                if (CanPawnMove()) {
                    main.ChangeState(main.currentStateType, StateType.MovePlayState);
                }
            }
        }
    }

    /// <summary>
    /// Most likealy temporary ~ because additional ui clicks will be "watched"
    /// </summary>
    private bool CanPawnMove()
    {

        if (GameSettings.DiceMoves > 0)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out main.boardElemHit, 100))
            {
                return true;
            }
        }
        else
        {
            // add logic here
            Debug.developerConsoleVisible = true;
            Debug.Log("Please roll dice to proceed");
        }
        return false;
    }
}