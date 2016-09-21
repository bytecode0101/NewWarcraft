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
    public List<Dice> DicesScripts;
    public List<Text> DicesTexts;

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
            // don't remove the gameObject casting as is.
            var myObj = (GameObject)Instantiate(diceGameObject);

            //if(myObj) // CONSIDER: maybe needed?
            //Dices.Add(myObj);

            myObj.transform.SetParent(diceHolderGobj.transform);
            //CONSIDER: maybe add a check if it is component present

            myObj.transform.localPosition = new Vector3(-50, 20 * (i), 0);
            myObj.transform.localScale = Vector3.one;
            
            DicesScripts.Add(myObj.GetComponent<Dice>());
            DicesTexts.Add(myObj.GetComponent<Text>());
        }
    }

    public void Throw()
    {
        int index = 0;
        int diceSum = 0;
        
        foreach (var item in DicesScripts)
        {
            var valueReceived = item.Throw();
            diceSum += item.Throw();
            Debug.Log(valueReceived);
            DicesTexts[index].text = "Dice [" + (index + 1) + "]: " + valueReceived;
            GameSettings.diceMoves += valueReceived;
            index++;
        }
        GameSettings.diceMoves += GameSettings.hasDiceAdvantage ? 1 : 0;

        Log(diceSum);


       // return diceSum;
    }

    internal void Log(int diceSum)
    {
        diceLogger.Add(diceSum);
        //TODO: implement later
    }
}
