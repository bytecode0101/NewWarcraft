using Assets.Script.Models;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Main : MonoBehaviour
{

    //
    // !! Space data is only temporary. remove after testing to something more well thought
    //
    public Text debugDiceMovementObj;

    int startOnWidth = 0;
    int startOnHeight = 0;

    public SharedMap sharedMap;
    GameSettings gameSettings = new GameSettings();

    public GameObject boardHolder;

    public GameObject emptySpace, filledSpace, dangerSpace, mercenarySpace, resourceSpace, baseSpace;

    public GameObject DiceGui, wonGui;

    public List<GameObject> PawnObject = new List<GameObject>();

    public List<GameObject> resourcesOnPawnP1 = new List<GameObject>();
    public List<GameObject> resourcesOnPawnP2 = new List<GameObject>();

    public List<GameObject> resourcesInBaseP1 = new List<GameObject>();
    public List<GameObject> resourcesInBaseP2 = new List<GameObject>();


    public void Start()
    {
        //sharedMap = new SharedMap();
        ElementInit();
    }

    Vector3 playerMoveTarget, playerStartPoint;

    public int debugDiceMovement = 0;

    public bool playerNotMoving = true;
    public void Update()
    {

        debugDiceMovement = GameSettings.diceMoves;

        if (Input.GetMouseButtonDown(0) && playerNotMoving)
        {
            if (GameSettings.diceMoves > 0)
            {

                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, 100))
                {

                    var pawnPos = PawnObject[GameSettings.playerTurn].GetComponent<SpaceData>();
                    var currPos = hit.transform.GetComponent<SpaceData>();

                    // wall collide - to place in a switch | state area
                    if (hit.transform.CompareTag("onBoardElement") &&
                        !sharedMap.Spaces[currPos.X, currPos.Y].transform.name.Contains(filledSpace.name))
                    {

                        if (!(pawnPos.X == currPos.X && pawnPos.Y == currPos.Y) &&
                            (((pawnPos.X + 1 == currPos.X || pawnPos.X - 1 == currPos.X) && pawnPos.Y == currPos.Y) ||
                            ((pawnPos.Y + 1 == currPos.Y || pawnPos.Y - 1 == currPos.Y) && pawnPos.X == currPos.X)))
                        {
                            Debug.Log("Got here");
                            var vec = new Vector3((currPos.X - pawnPos.X) * tileDistanceX,
                                                                             0,
                                                                             (currPos.Y - pawnPos.Y) * tileDistanceY);

                            playerNotMoving = false;
                            playerMoveTarget = vec;
                            playerStartPoint = PawnObject[GameSettings.playerTurn].transform.position;

                            pawnPos.X = currPos.X;
                            pawnPos.Y = currPos.Y;

                            // to use broadcast or navigate in node from diceThrower to the main since they are siblings
                            GameSettings.diceMoves--;

                            debugDiceMovementObj.text = GameSettings.diceMoves.ToString();

                            var currHitState = hit.transform.GetComponent<MeshRenderer>().enabled;
                            if (!hit.transform.name.Contains("Base3D") && currHitState)
                            {
                                ManageHitSpace(hit);
                            }
                            if (currHitState)
                            {
                                DoPlayerUpdate(hit);
                            }


                            CheckBattle();

                            if (GameSettings.diceMoves == 0 && GameSettings.IsGameOn)
                            {
                                GameSettings.playerTurn = (++GameSettings.playerTurn) % GameSettings.totalPlayers;

                                Camera.main.GetComponent<StandardAssetFollowTarget>().target = PawnObject[GameSettings.playerTurn].transform;

                            }

                            if (!GameSettings.IsGameOn)
                            {
                                Debug.Log("Game Ended! Someone won");
                                DiceGui.SetActive(false);
                                wonGui.SetActive(true);
                                foreach (var item in PawnObject)
                                {
                                    item.SetActive(false);
                                }
                            }

                        }
                    }

                }
            }
            else
            {
                // add logic here
                Debug.developerConsoleVisible = true;
                Debug.Log("Please roll dice to proceed");
            }
        }


        var onX = playerStartPoint.x - PawnObject[GameSettings.playerTurn].transform.position.x;
        var onZ = playerStartPoint.z - PawnObject[GameSettings.playerTurn].transform.position.z;
        if (!playerNotMoving &&
            Math.Abs(onX) < tileDistanceX &&
            Math.Abs(onZ) < tileDistanceY)
        {
            PawnObject[GameSettings.playerTurn].transform.Translate(playerMoveTarget * Time.deltaTime * 1.5f);

            //Camera.main.transform.position = new Vector3(PawnObject[GameSettings.playerTurn].transform.position.x,
            //                                              Camera.main.transform.position.y,
            //                                            PawnObject[GameSettings.playerTurn].transform.position.z);
        }
        else
        {
            if (!playerNotMoving)
            {
                playerNotMoving = true;
                PawnObject[GameSettings.playerTurn].transform.position = new Vector3(-105 + PawnObject[GameSettings.playerTurn].GetComponent<SpaceData>().X * tileDistanceX,
                                                                PawnObject[GameSettings.playerTurn].transform.position.y,
                                                                -75 + PawnObject[GameSettings.playerTurn].GetComponent<SpaceData>().Y * tileDistanceY);
            }
        }
    }

    private void CheckBattle()
    {
        // place the judge pattern from here
    }

    private void DoPlayerUpdate(RaycastHit hit)
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

    private void DangerResource()
    {
        ChanceResource(2);
    }

    private void MercenaryResource()
    {
        ChanceResource(8);
    }

    private void ChanceResource(int winChance)
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

    private int DisplaceResources()
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

    private bool CheckIfGameWonTemporary(int total)
    {
        return (total > GameSettings.winTarget);
    }

    private void GetRandomResource()
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

    private void ManageHitSpace(RaycastHit hit)
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

    void ElementInit()
    {

        ////////////////////// row 1

        var rowsSharedMap = TempSharedMap();

        ListSharedMap(rowsSharedMap);
    }

    public float tileDistanceX = 10f;
    public float tileDistanceY = 10f;

    void ListSharedMap(List<List<GameObject>> rowsSharedMap)
    {
        GameObject tempobj = null;

        int offX = 0;
        int offY = 0;
        float onx = tileDistanceX;
        float ony = tileDistanceY;

        var itemCount = rowsSharedMap.Count;
        var itemInCount = rowsSharedMap[0].Count;
        sharedMap = new SharedMap(itemInCount, itemCount);

        for (int j = 0; j < itemCount; j++)
        {
            for (var i = 0; i < itemInCount; i++)
            {

                tempobj = (GameObject)Instantiate(rowsSharedMap[j][i]);
                tempobj.transform.SetParent(boardHolder.transform);
                tempobj.transform.localPosition = new Vector3(i * onx + offX, tempobj.transform.localScale.y + .05f, j * ony + offY);

                // temporary solution
                tempobj.GetComponent<SpaceData>().X = i;
                tempobj.GetComponent<SpaceData>().Y = j;

                sharedMap.Spaces[i, j] = tempobj;
            }
        }

        // temporary hack so that the resources get offseted in the center of the tile rather than the start of the tile
        boardHolder.transform.Translate(new Vector3(5f, 0, 5f));
    }

    List<List<GameObject>> TempSharedMap()
    {
        var rowsSharedMap = new List<List<GameObject>>();

        rowsSharedMap.Add(new List<GameObject>() {
                baseSpace, emptySpace, emptySpace, resourceSpace,
                emptySpace, filledSpace, resourceSpace, filledSpace,
                mercenarySpace, filledSpace, resourceSpace, emptySpace,
                filledSpace, filledSpace, emptySpace, emptySpace,
                emptySpace, emptySpace, emptySpace, emptySpace,
                emptySpace, emptySpace });

        rowsSharedMap.Add(new List<GameObject>() {
                emptySpace, filledSpace, filledSpace, emptySpace,
                emptySpace, filledSpace, dangerSpace, dangerSpace,
                emptySpace, filledSpace, emptySpace, resourceSpace,
                emptySpace, emptySpace, emptySpace, resourceSpace,
                emptySpace, filledSpace, emptySpace, emptySpace,
                filledSpace, emptySpace });

        rowsSharedMap.Add(new List<GameObject>(){
                emptySpace, emptySpace, filledSpace, dangerSpace,
                filledSpace, filledSpace, resourceSpace, emptySpace,
                emptySpace, filledSpace, filledSpace, filledSpace,
                emptySpace, emptySpace, filledSpace, filledSpace,
                emptySpace, filledSpace, resourceSpace, filledSpace,
                emptySpace, emptySpace});

        rowsSharedMap.Add(new List<GameObject>(){
                emptySpace, emptySpace, resourceSpace, dangerSpace,
                dangerSpace, emptySpace, resourceSpace, resourceSpace,
                dangerSpace, emptySpace, mercenarySpace, filledSpace,
                dangerSpace, emptySpace, dangerSpace, dangerSpace,
                emptySpace, dangerSpace, resourceSpace, filledSpace,
                dangerSpace, filledSpace});

        rowsSharedMap.Add(new List<GameObject>(){
                dangerSpace, emptySpace, filledSpace, emptySpace,
                resourceSpace, filledSpace, filledSpace, emptySpace,
                emptySpace, emptySpace, resourceSpace, mercenarySpace,
                dangerSpace, filledSpace, mercenarySpace, resourceSpace,
                emptySpace, dangerSpace, resourceSpace, dangerSpace,
                dangerSpace, resourceSpace});

        rowsSharedMap.Add(new List<GameObject>(){
            resourceSpace,dangerSpace,filledSpace,filledSpace,
            resourceSpace,emptySpace,filledSpace,resourceSpace,
            emptySpace,filledSpace,filledSpace,filledSpace,
            emptySpace,filledSpace,emptySpace,filledSpace,
            emptySpace,filledSpace,filledSpace,filledSpace,
            filledSpace,emptySpace
        });

        rowsSharedMap.Add(new List<GameObject>()
        {
            filledSpace,emptySpace,emptySpace,dangerSpace,
            dangerSpace,dangerSpace,filledSpace,resourceSpace,
            dangerSpace,resourceSpace,filledSpace,resourceSpace,
            emptySpace,dangerSpace,resourceSpace,resourceSpace,
            resourceSpace,emptySpace,emptySpace,resourceSpace,
            emptySpace,resourceSpace
        });

        rowsSharedMap.Add(new List<GameObject>()
        {
            resourceSpace,dangerSpace,emptySpace,filledSpace,
            emptySpace,resourceSpace,resourceSpace,emptySpace,
            emptySpace,emptySpace,filledSpace,filledSpace,
            emptySpace,filledSpace,filledSpace,dangerSpace,
            dangerSpace,filledSpace,mercenarySpace,emptySpace,
            dangerSpace,filledSpace
        });

        rowsSharedMap.Add(new List<GameObject>()
        {
            filledSpace,emptySpace,filledSpace,filledSpace,
            mercenarySpace,filledSpace,filledSpace,mercenarySpace,
            resourceSpace,resourceSpace,filledSpace,mercenarySpace,
            dangerSpace,emptySpace,emptySpace,emptySpace,
            emptySpace,filledSpace,filledSpace,resourceSpace,
            emptySpace,mercenarySpace
        });

        rowsSharedMap.Add(new List<GameObject>()
        {
            emptySpace,emptySpace,resourceSpace,filledSpace,
            emptySpace,dangerSpace,emptySpace,emptySpace,
            emptySpace,resourceSpace,dangerSpace,dangerSpace,
            resourceSpace,emptySpace,filledSpace,filledSpace,
            emptySpace,emptySpace,filledSpace,resourceSpace,
            emptySpace,filledSpace
        });

        rowsSharedMap.Add(new List<GameObject>()
        {
            dangerSpace,emptySpace,emptySpace,emptySpace,
            emptySpace,emptySpace,filledSpace,dangerSpace,
            dangerSpace,resourceSpace,filledSpace,filledSpace,
            resourceSpace,dangerSpace,filledSpace,emptySpace,
            filledSpace,filledSpace,filledSpace,emptySpace,
            emptySpace,emptySpace
        });

        rowsSharedMap.Add(new List<GameObject>()
        {
            dangerSpace,filledSpace,emptySpace,dangerSpace,
            filledSpace,resourceSpace,emptySpace,emptySpace,
            emptySpace,emptySpace,emptySpace,emptySpace,
            dangerSpace,emptySpace,emptySpace,dangerSpace,
            emptySpace,emptySpace,dangerSpace,mercenarySpace,
            filledSpace,emptySpace
        });

        rowsSharedMap.Add(new List<GameObject>()
        {
            emptySpace,dangerSpace,emptySpace,emptySpace,
            filledSpace,resourceSpace,mercenarySpace,filledSpace,
            emptySpace,filledSpace,dangerSpace,resourceSpace,
            dangerSpace,emptySpace,filledSpace,emptySpace,
            filledSpace,emptySpace,emptySpace,emptySpace,
            filledSpace,emptySpace
        });

        rowsSharedMap.Add(new List<GameObject>()
        {
            emptySpace,filledSpace,resourceSpace,emptySpace,
            emptySpace,emptySpace,emptySpace,dangerSpace,
            emptySpace,filledSpace,dangerSpace,emptySpace,
            emptySpace,resourceSpace,filledSpace,filledSpace,
            filledSpace,emptySpace,dangerSpace,filledSpace,
            resourceSpace,emptySpace
        });

        rowsSharedMap.Add(new List<GameObject>()
        {
            emptySpace,filledSpace,emptySpace,dangerSpace,
            filledSpace,filledSpace,mercenarySpace,dangerSpace,
            emptySpace,mercenarySpace,emptySpace,filledSpace,
            emptySpace,resourceSpace,emptySpace,emptySpace,
            emptySpace,filledSpace,emptySpace,filledSpace,
            resourceSpace,emptySpace
        });

        rowsSharedMap.Add(new List<GameObject>()
        {
            emptySpace,emptySpace,dangerSpace,resourceSpace,
            resourceSpace,dangerSpace,emptySpace,filledSpace,
            filledSpace,dangerSpace,emptySpace,filledSpace,
            filledSpace,resourceSpace,resourceSpace,filledSpace,
            dangerSpace,resourceSpace,resourceSpace,dangerSpace,
            dangerSpace,baseSpace
        });

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