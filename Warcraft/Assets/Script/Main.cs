using System;
using UnityEngine;



class Main : MonoBehaviour
{
    SharedMap sharedMap;
    GameSettings gameSettings = new GameSettings();

    public void Start()
    {
        sharedMap = new SharedMap();
        ElementInit();
        ElementPlacement();

    }

    private void ElementPlacement()
    {
        throw new NotImplementedException();
    }

    private bool RandomizeOnBoard(int [] numElements)
    {
        var elementAtIndex = UnityEngine.Random.Range(0, gameSettings.ElementTypeCount - 1);
        if (numElements[elementAtIndex] > 0)
        {
            return true;
        }
        else if (elementAtIndex - 1 > 0)
        {
            if (numElements[elementAtIndex - 1] > 0)
            {
                return true;
            }
        }
        else if (elementAtIndex + 1 < numElements.Length - 1)
        {
            if (numElements[elementAtIndex - numElements.Length] < numElements.Length - 1)
            {
                return true;
            }
        }
        return false;
    }

    void ElementInit()
    {
        // raport
        // sa spunem ca avem 20 * 30: cazul de 2 persoane care se joaca
        // din acele 20 * 30
        // avem resources

        // vvvvv LE MAI AVEM PE ACESTEA? vvvv 
        // avem spaces
        // avem nonspaces (deadspace)
        // dangers
        // merceneries

        int total = gameSettings.BoardWidth * gameSettings.BoardHeight;

        int numNonspace = gameSettings.PercentageNonSpace * total;
        int numSpace = gameSettings.PercentageSpace * total;
        int numEnemy = gameSettings.PercentageEnemy * total;
        int numMercenery = gameSettings.PercentageMercenery * total;
        int numDangers = gameSettings.PercentageDangers * total;
        int numResources = total - numNonspace - numSpace - numEnemy - numMercenery - numDangers;
        int[] numElements = new int[] { numNonspace, numSpace, numEnemy, numMercenery, numDangers, numResources };

        while (true) { 
            
        }

        for (int i = 0; i < gameSettings.BoardHeight; i++)
        {
            for (int j = 0; j < gameSettings.BoardWidth; j++)
            {
                switch (ofType)
                {
                    case ElementDefinition.NONSPACE:
                        if (numNonspace > 0)
                        {
                            numNonspace--;
                            sharedMap.Spaces[j, i] = new Element();
                        }
                        else
                        {
                            RandomizeOnBoard
                        }
                        break;
                    case ElementDefinition.SPACE:

                        break;
                    case ElementDefinition.RESOURCE:
                        break;
                    case ElementDefinition.DANGER:
                        break;
                    case ElementDefinition.ENEMY:
                        break;
                    case ElementDefinition.MERCENARIES:
                        break;
                    default:
                        break;
                }

            }
        }
    }
}