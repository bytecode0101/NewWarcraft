class GameSettings
{
    internal int NumberOfDices = 2;
    internal int NumberOfPlayers = 3;

    internal int DiceMin = 1;
    internal int DiceMax = 7;

    internal int BoardWidth;
    internal int BoardHeight;

    internal int PercentageResources = 50;
    internal int PercentageSpace = 20;
    internal int PercentageNonSpace = 10;
    internal int PercentageEnemy = 6;
    internal int PercentageMercenery = 6;
    internal int PercentageDangers = 8;
    internal int ElementTypeCount;
   

    public GameSettings()
    {
        ElementTypeCount = ElementDefinition.GetNames(typeof(ElementDefinition)).Length;
        BoardWidth = 15 * NumberOfPlayers;
        BoardHeight = 10 * NumberOfPlayers;
    }

}