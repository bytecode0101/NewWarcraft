using Assets.Script.BoardTiles;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Is there a need for a player gamestate different from a general state?
/// </summary>
enum PlayerGameState
{
    OnBoard,
    InCitadel
}

public class Player
{
    internal List<Pawn> Pawns { get; private set; }
    internal List<TileResChild> Resources { get; private set; }
    
    public Player(GameObject pawnObject)
    {
        Pawns = new List<Pawn>() { new Pawn(pawnObject) };       
    }

}