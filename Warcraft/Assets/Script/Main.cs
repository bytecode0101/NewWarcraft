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

class Main : MonoBehaviour
{
    public GameObject prefabDiceGUI;
    public List<GameObject> boardPawns;
    public GameObject prefabPlayerPanel;
    public List<GameObject> prefabTiles;

    public GameObject boardObjectsHolder;
    public GameObject boardFloor;

    public string currentBoardMapPath;

    GameSettings gameSettings = new GameSettings();

    public float tileDistanceX = 10f;
    public float tileDistanceY = 10f;
    internal bool isBoardInitiated = false;
    
    internal StateType prevStateType { get; set; }
    internal StateType currentStateType { get; set; }

    Dictionary<StateType, IState> states;
    
    public void Start()
    {
        currentBoardMapPath = "map001.xml";
        states = new Dictionary<StateType, IState>();
        states.Add(StateType.BoardInitState, new BoardInitState(this));
        states.Add(StateType.IdlePlayState, new IdlePlayState(this));
        states.Add(StateType.MovePlayState, new MovePlayState(this));

        prevStateType = StateType.NoState;
        currentStateType = StateType.BoardInitState;
    }

    void Update()
    {
        if (prevStateType != currentStateType)
        {
            // Set a state - temporary whilst the main menu is not present
            switch (currentStateType)
            {
                case StateType.BoardInitState:
                    states[currentStateType].DoState();
                    ChangeStates(currentStateType, StateType.IdlePlayState);
                    break;

                case StateType.IdlePlayState:
                    states[currentStateType].DoState();
                    break;

                case StateType.MovePlayState:
                    states[currentStateType].DoState();
                    ChangeStates(currentStateType, StateType.IdlePlayState);
                    break;

                default:
                    break;
            }

            ///
            Main main = this;
            var onX = main.playerStartPoint.x - main.boardPawns[GameSettings.playerTurn].transform.position.x;
            var onZ = main.playerStartPoint.z - main.boardPawns[GameSettings.playerTurn].transform.position.z;
            if (!main.playerNotMoving &&
                Math.Abs(onX) < main.tileDistanceX &&
                Math.Abs(onZ) < main.tileDistanceY)
            {
                main.boardPawns[GameSettings.playerTurn].transform.Translate(main.playerMoveTarget * Time.deltaTime * 1.5f);

                //Camera.main.transform.position = new Vector3(PawnObject[GameSettings.playerTurn].transform.position.x,
                //                                              Camera.main.transform.position.y,
                //                                            PawnObject[GameSettings.playerTurn].transform.position.z);
            }
            else
            {
                if (!main.playerNotMoving)
                {
                    main.playerNotMoving = true;
                    main.boardPawns[GameSettings.playerTurn].transform.position = new Vector3(-105 + main.boardPawns[GameSettings.playerTurn].GetComponent<SpaceData>().X * main.tileDistanceX,
                                                                    main.boardPawns[GameSettings.playerTurn].transform.position.y,
                                                                    -75 + main.boardPawns[GameSettings.playerTurn].GetComponent<SpaceData>().Y * main.tileDistanceY);
                }
            }
        }
    }

    internal void ChangeStates(StateType forPrevState, StateType forCurrentState)
    {
        prevStateType = forPrevState;
        currentStateType = forCurrentState;
    }

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
    
    internal RaycastHit hit;

    public void CheckBattle()
    {
        // place the judge pattern from here
    }

    public void DoPlayerUpdate(RaycastHit hit)
    {
        //emptySpace, filledSpace, dangerSpace, mercenarySpace, resourceSpace;
        // maybe a chain of command here?
        if (hit.transform.name.Contains("Resource3D")) // maybe use a tag here?
        {

            //show the dialog
            // show the five elements needed
            GetRandomResource();

        }
        else if (hit.transform.name.Contains("Danger3D")) // maybe use a tag here?
        {
            DangerResource();

            //show the dialog
            // a random card. pay some resources / fight / flee (with some chances of escape
        }
        else if (hit.transform.name.Contains("Mercenary3D")) // maybe use a tag here?
        {
            MercenaryResource();

            //show the dialog
            // hire some mercenary to protected your fort |
            // a risk that they will be swayed by your opponent when they siege your place
            // change some resources for others
        }
        else if (hit.transform.name.Contains("Base3D"))
        {
            int total = DisplaceResources();

            /////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////
            var res = CheckIfGameWonTemporary(total);
            if (res) GameSettings.IsGameOn = false;
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

        var resourceOnPawn = (GameSettings.playerTurn == 0) ? resourcesOnPawnP1 : resourcesOnPawnP2;

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

            var resourceOnPawn = (GameSettings.playerTurn == 0) ? resourcesOnPawnP1 : resourcesOnPawnP2;
            var resourcesInBase = (GameSettings.playerTurn == 0) ? resourcesInBaseP1 : resourcesInBaseP2;

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
        return (total > GameSettings.winTarget);
    }

    public void GetRandomResource()
    {
        if (resourcesOnPawnP1.Count > 0)
        {
            var resourceOnPawn = (GameSettings.playerTurn == 0) ? resourcesOnPawnP1 : resourcesOnPawnP2;

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

    public void ManageHitSpace(RaycastHit hit)
    {
        var hitMesh = hit.transform.GetComponent<MeshRenderer>();
        hitMesh.enabled = false;

        var hitChild = hit.transform.gameObject.GetComponentsInChildren<MeshRenderer>();
        if (hitChild != null)
        {
            foreach (var item in hitChild)
            {
                if (item.gameObject != hitMesh.gameObject)
                {
                    item.enabled = true;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// TODO: WIP or Deprecated?
    /// </summary>
    public List<List<GameObject>> TempSharedMap()
    {
        var rowsSharedMap = new List<List<GameObject>>();
        
        Map m = new Map();

        int y = 0;
        foreach (var row in rowsSharedMap)
        {
            int x = 0;
            foreach (var cell in row)
            {
                m.UpdateCell(y, x, rowsSharedMap[y][x].name);
                x++;
            }
            x = 0;
            y++;
        }
        return rowsSharedMap;
    }

}