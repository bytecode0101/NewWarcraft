using System.Collections.Generic;

class DiceLogger
{
    internal List<int> sums;

    public DiceLogger()
    {
        sums = new List<int>();
    }
    internal void Add(int diceSum)
    {
        sums.Add(diceSum);
    }    
}
