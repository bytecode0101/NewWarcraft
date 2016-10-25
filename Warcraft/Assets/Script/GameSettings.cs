using Assets.Script;
using UnityEngine;

class GameSettings
{
    // temporary used static here. consider placing in another area
    internal static int DiceMoves = 0;
    internal bool hasDiceAdvantage = false;

    //
    internal int NumberOfDices = 2;
    internal int NumberOfPlayers = 2;

    internal int DiceMin = 1;
    internal int DiceMax = 7;
    
    internal float PercentageResources = .50F;
    internal float PercentageSpace = .20F;
    internal float PercentageNonSpace = .10F;
    internal float PercentageEnemy = .06F;
    internal float PercentageMercenery = .06F;
    internal float PercentageDangers = .08F;
    internal int ElementTypeCount;
    internal int totalPlayers = 2;
    internal int playerTurn = 0;

    public bool IsGameOn = true;
    internal int winTarget = 10;

    public GameSettings()
    {
        //ElementTypeCount = ElementDefinition.GetNames(typeof(ElementDefinition)).Length;
        BoardData = new BoardData();
        PawnTransition = 1f;
        PawnBaseMovementSpeed = 1.5f;
        BoardCameraCanTransition = true;
    }


    /////    
    internal string CurrentBoardMapPath { get; private set; }

    internal float PawnBaseMovementSpeed { get; private set; }
    internal float PawnTransition { get; private set; }

    internal BoardData BoardData { get; private set; }
    public bool BoardCameraCanTransition { get; set; }
}