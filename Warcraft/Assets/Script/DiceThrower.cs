using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class DiceThrower : MonoBehaviour {

    GameSettings gameSettings = new GameSettings();
    DiceLogger diceLogger;
    public GameObject diceHolderGobj;
    public GameObject diceGameObject;
    //internal List<GameObject> Dices;
    internal List<Dice> DicesScripts;
    internal List<Text> DicesTexts;

    void Start()
    {
        diceLogger = new DiceLogger();

        InitializeDices();
    }

    private void InitializeDices()
    {
        //Dices = new List<GameObject>();
        DicesScripts = new List<Dice>();
        DicesTexts = new List<Text>();

        int numOfDices = gameSettings.NumberOfDices;
        for (var i = 0; i < numOfDices; i++)
        {
            var myObj = (GameObject)Instantiate(diceGameObject, new Vector2(100, -30 * (i + 1)), Quaternion.identity);

            //if(myObj) // CONSIDER: maybe needed?
            //Dices.Add(myObj);
            
            myObj.transform.SetParent(diceHolderGobj.transform);
            //CONSIDER: maybe add a check if it is component present
            
            DicesScripts.Add(myObj.GetComponent<Dice>());
            DicesTexts.Add(myObj.GetComponent<Text>());
        }
    }

    public void Throw()
    {
        int index = 0;
        int diceSum = 0;

        Debug.Log("aaaaa" + DicesScripts.Count);
        foreach (var item in DicesScripts)
        {
            Debug.Log("bbbbb" + DicesTexts[index]);
            var valueReceived = item.Throw();
            diceSum += item.Throw();
            Debug.Log(valueReceived);
            DicesTexts[index].text = "Dice [" + (index + 1) + "]: " + valueReceived;
            index++;
        }

        Log(diceSum);


       // return diceSum;
    }

    internal void Log(int diceSum)
    {
        diceLogger.Add(diceSum);
        //TODO: implement later
    }
}
