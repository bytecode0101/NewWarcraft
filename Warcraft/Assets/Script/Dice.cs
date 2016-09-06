using UnityEngine;

public class Dice : MonoBehaviour
{
    GameSettings gameSettings = new GameSettings();
    internal int Number;
    internal int Throw()
    {
        return Random.Range(gameSettings.DiceMin,
                            gameSettings.DiceMax);
    }
}
