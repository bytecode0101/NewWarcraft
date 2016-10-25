using Assets.Script.States;
using Assets.Script.Models;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Script.BoardTiles;
using System.Linq;

class Game
{
    // add a states machine // for game states

    // gui elements get updated (observer)
}

/// <summary>
/// MY SHORT TERM TARGETS (mainly remove the current code from main | see old bits to refactor region below
/// - Think more about the functionality of the players and pawns (eg. what happens when a player has more than one pawn?
/// - Do the player movement and other things in the player area but trigger it in one of the below states (?)
/// - Tie -as mentioned in the comment above- an observer system.
/// 
/// 
/// - I had the player and the TileResChild(AKA Resource) using the decorator pattern. Maybe not go too far from that idea or consider if still desired.
/// 
/// I do not see it as good practice to pass the main in every and each state and so on. It will probably creates a headache to manage this sort of approach. Use UML and revise
/// 
/// </summary>

public class Main : MonoBehaviour
{
    public GameObject prefabDiceGUI;
    public GameObject prefabPlayerPanel;
    public GameObject prefabBoardFloor;
    public List<GameObject> prefabTiles;
    public List<GameObject> prefabPawns;
    
    public GameObject boardObjectsHolder;
    public GameObject boardFloor;

    internal GameSettings gameSettings = new GameSettings();

    internal StateType prevStateType { get; set; }
    internal StateType currentStateType { get; set; }

    internal PlayersManager PlayersManager { get; set; }

    Dictionary<StateType, IState> states;

    public void Start()
    {
        states = new Dictionary<StateType, IState>();
        states.Add(StateType.BoardInitState, new BoardInitState(this));
        states.Add(StateType.IdlePlayState, new IdlePlayState(this));
        states.Add(StateType.MovePlayState, new MovePlayState(this));
        states.Add(StateType.GameInitState, new GameInitState(this));

        prevStateType = StateType.NoState;
        currentStateType = StateType.BoardInitState;
    }

    void Update()
    {

        // state change // first loading boardInitState [see start method]
        states[currentStateType].DoState();

        /////
        if (PlayersManager != null && PlayersManager.Players != null && PlayersManager.Players.Count > 1)
        {

            PlayersManager.PawnsUpdate();
            
            if (gameSettings.BoardCameraCanTransition)
            {
                var currPawnObj = PlayersManager.GetCurrentActivePlayer();
                
                Camera.main.transform.position = Vector3.MoveTowards(
                                    new Vector3(Camera.main.transform.position.x, 70, Camera.main.transform.position.z),
                                    new Vector3(currPawnObj.transform.position.x,
                                    100,
                                    currPawnObj.transform.position.z),
                                    /* Time.deltaTime */ 1);
            }
        }
    }

    /// <summary>
    /// Change between states
    /// </summary>
    /// <param name="forPrevState">Maybe useful for a go back option scenario?</param>
    /// <param name="forCurrentState">The state to switch to</param>
    internal void ChangeState(StateType forPrevState, StateType forCurrentState)
    {
        prevStateType = forPrevState;
        currentStateType = forCurrentState;
    }

    internal void SetCurrentActivePawn()
    {
        if (GameSettings.DiceMoves == 0 && gameSettings.IsGameOn)
        {
            gameSettings.playerTurn = (gameSettings.playerTurn + 1) % gameSettings.totalPlayers;

            //Camera.main.GetComponent<StandardAssetFollowTarget>().target = boardPawns[gameSettings.playerTurn].transform;

            // InvokeRepeating("PawnTransitionCamera", 0, .3f);
        }
    }
    #region old bits to refactor

    /// <summary>
    /// /////////////////////////////////
    /// </summary>
    //public Text debugDiceMovementObj;

    //public SharedMap sharedMap;

    public GameObject DiceGui, wonGui;
    
    public List<GameObject> resourcesOnPawnP1 = new List<GameObject>();
    public List<GameObject> resourcesOnPawnP2 = new List<GameObject>();

    public List<GameObject> resourcesInBaseP1 = new List<GameObject>();
    public List<GameObject> resourcesInBaseP2 = new List<GameObject>();
    
    internal Vector3 playerMoveTarget, playerStartPoint;

    public int debugDiceMovement = 0;

    public bool playerNotMoving = true;

    internal RaycastHit boardElemHit;

    public void CheckBattle()
    {
        // place the judge pattern from here
    }

    public void DoPlayerUpdate(RaycastHit boardElemHit)
    {
        //emptySpace, filledSpace, dangerSpace, mercenarySpace, resourceSpace;
        // maybe a chain of command here?
        if (boardElemHit.transform.name.Contains("Resource3D")) // maybe use a tag here?
        {

            //show the dialog
            // show the five elements needed
            GetRandomResource();

        }
        else if (boardElemHit.transform.name.Contains("Danger3D")) // maybe use a tag here?
        {
            DangerResource();

            //show the dialog
            // a random card. pay some resources / fight / flee (with some chances of escape
        }
        else if (boardElemHit.transform.name.Contains("Mercenary3D")) // maybe use a tag here?
        {
            MercenaryResource();

            //show the dialog
            // hire some mercenary to protected your fort |
            // a risk that they will be swayed by your opponent when they siege your place
            // change some resources for others
        }
        else if (boardElemHit.transform.name.Contains("Base3D"))
        {
            int total = DisplaceResources();

            /////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////
            var res = CheckIfGameWonTemporary(total);
            if (res) gameSettings.IsGameOn = false;
            /////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////
        }
    }

    public void DangerResource()
    {
        ChanceResource(2);
    }

    public void MercenaryResource()
    {
        ChanceResource(8);
    }

    public void ChanceResource(int winChance)
    {
        int res = UnityEngine.Random.Range(0, 10);
        int count = resourcesInBaseP1.Count;

        var resourceOnPawn = (gameSettings.playerTurn == 0) ? resourcesOnPawnP1 : resourcesOnPawnP2;

        while (res - winChance > 0)
        {
            for (int i = 0; i < count; i++)
            {
                var tempComponent = resourceOnPawn[i].GetComponentInChildren<Text>();
                var strVal = tempComponent.text.ToString();
                int intVal = 0;

                if (int.TryParse(strVal, out intVal))
                {
                    if (intVal > 0)
                    {
                        tempComponent.text = (winChance >= 5) ? (++intVal).ToString() : (--intVal).ToString();
                    }

                }
            }
            res--;
        }

    }

    public int DisplaceResources()
    {
        int total = 0;

        int count = resourcesOnPawnP1.Count;
        for (int i = 0; i < count; i++)
        {

            var resourceOnPawn = (gameSettings.playerTurn == 0) ? resourcesOnPawnP1 : resourcesOnPawnP2;
            var resourcesInBase = (gameSettings.playerTurn == 0) ? resourcesInBaseP1 : resourcesInBaseP2;

            var tempComponent = resourceOnPawn[i].GetComponentInChildren<Text>();
            var tempComponentBase = resourcesInBase[i].GetComponentInChildren<Text>();

            if (tempComponent && tempComponentBase)
            {
                var strVal = tempComponent.text.ToString();
                var strValBase = tempComponentBase.text.ToString();

                int intVal = 0;
                int intValBase = 0;

                if (int.TryParse(strVal, out intVal) && int.TryParse(strValBase, out intValBase))
                {
                    tempComponent.text = "0";
                    tempComponentBase.text = (intVal + intValBase).ToString();

                    //////////////////////////////////////////
                    //////////////////////////////////////////
                    //////////////////////////////////////////
                    total += intVal + intValBase;
                }
            }
        }

        return total;
    }

    public bool CheckIfGameWonTemporary(int total)
    {
        return (total > gameSettings.winTarget);
    }

    public void GetRandomResource()
    {
        if (resourcesOnPawnP1.Count > 0)
        {
            var resourceOnPawn = (gameSettings.playerTurn == 0) ? resourcesOnPawnP1 : resourcesOnPawnP2;

            var tempResource = resourceOnPawn[(int)UnityEngine.Random.Range(0,
                                                resourceOnPawn.Count)];

            // simpler to do it this way ~however is inneficient than to keep track
            var tempComponent = tempResource.GetComponentInChildren<Text>();
            if (tempComponent)
            {
                var strVal = tempComponent.text.ToString();
                int intVal = 0;
                if (int.TryParse(strVal, out intVal))
                {
                    tempComponent.text = (++intVal).ToString();

                }
            }
        }
    }

    public void ManageboardElemHitSpace(RaycastHit boardElemHit)
    {
        var boardElemHitMesh = boardElemHit.transform.GetComponent<MeshRenderer>();
        boardElemHitMesh.enabled = false;

        var boardElemHitChild = boardElemHit.transform.gameObject.GetComponentsInChildren<MeshRenderer>();
        if (boardElemHitChild != null)
        {
            foreach (var item in boardElemHitChild)
            {
                if (item.gameObject != boardElemHitMesh.gameObject)
                {
                    item.enabled = true;
                    break;
                }
            }
        }
    }

    ///// <summary>
    ///// TODO: WIP or Deprecated?
    ///// </summary>
    //public List<List<GameObject>> TempSharedMap()
    //{
    //    var rowsSharedMap = new List<List<GameObject>>();

    //    Map m = new Map();

    //    int y = 0;
    //    foreach (var row in rowsSharedMap)
    //    {
    //        int x = 0;
    //        foreach (var cell in row)
    //        {
    //            m.UpdateCell(y, x, rowsSharedMap[y][x].name);
    //            x++;
    //        }
    //        x = 0;
    //        y++;
    //    }
    //    return rowsSharedMap;
    //}

    #endregion

}