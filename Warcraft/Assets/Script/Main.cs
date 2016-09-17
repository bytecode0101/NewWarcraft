using System;
using System.Collections.Generic;
using UnityEngine;

class Main : MonoBehaviour
{
    int startOnWidth = 0;
    int startOnHeight = 0;

    public SharedMap sharedMap;
    GameSettings gameSettings = new GameSettings();

    public GameObject boardHolder;

    public GameObject emptySpace, filledSpace, dangerSpace, mercenarySpace, resourceSpace;

    public void Start()
    {
        //sharedMap = new SharedMap();
        ElementInit();

    }

    void ElementInit()
    {

        ////////////////////// row 1

        var rowsSharedMap = TempSharedMap();

        ListSharedMap(rowsSharedMap);
    }

    void ListSharedMap(List<List<GameObject>> rowsSharedMap)
    {
        GameObject tempobj = null;

        int offX = 0;
        int offY = 0;
        int onx = 1;
        int ony = 1;

        var itemCount = rowsSharedMap.Count;
        var itemInCount = rowsSharedMap[0].Count;
        sharedMap = new SharedMap(itemInCount, itemCount);

        for (int j = 0; j < itemCount; j++)
        {
            for (var i = 0; i < itemInCount; i++)
            {
                tempobj = (GameObject)Instantiate(rowsSharedMap[j][i],
                                                    new Vector3(i * onx + offX, 1, j * ony + offY),
                                                    Quaternion.identity);
                tempobj.transform.SetParent(boardHolder.transform);
                sharedMap.Spaces[i, j] = tempobj;
            }
        }
    }

    List<List<GameObject>> TempSharedMap()
    {
        var rowsSharedMap = new List<List<GameObject>>();

        rowsSharedMap.Add(new List<GameObject>() {
                emptySpace, emptySpace, emptySpace, resourceSpace,
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
            dangerSpace,emptySpace
        });

        return rowsSharedMap;
    }
}