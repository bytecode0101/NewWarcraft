class GameSettings
{
    // temporary used static here. consider placing in another area
    internal static int diceMoves = 0;
    internal static bool hasDiceAdvantage = false;

    //
    internal int NumberOfDices = 2;
    internal int NumberOfPlayers = 3;

    internal int DiceMin = 1;
    internal int DiceMax = 7;

    internal int BoardWidth;
    internal int BoardHeight;

    internal float PercentageResources = .50F;
    internal float PercentageSpace = .20F;
    internal float PercentageNonSpace = .10F;
    internal float PercentageEnemy = .06F;
    internal float PercentageMercenery = .06F;
    internal float PercentageDangers = .08F;
    internal int ElementTypeCount;    

    public GameSettings()
    {
        ElementTypeCount = ElementDefinition.GetNames(typeof(ElementDefinition)).Length;
        BoardWidth = 10;//15 * NumberOfPlayers;
        BoardHeight = 5;//10 * NumberOfPlayers;
    }
    
}