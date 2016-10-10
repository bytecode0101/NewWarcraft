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
        main.debugDiceMovement = GameSettings.diceMoves;

        if (main.playerNotMoving)
        if (Input.GetMouseButtonDown(0) && main.playerNotMoving)
        {
            if (GameSettings.diceMoves > 0)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out main.hit, 100))
                {
                    var prevState = main.currentStateType;                        
                    main.ChangeStates(prevState, StateType.MovePlayState);
                }
            }
            else
            {
                // add logic here
                Debug.developerConsoleVisible = true;
                Debug.Log("Please roll dice to proceed");
            }
        }


    }
}