using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;

using Assets.Script.BoardTiles;
using System.Collections.Generic;

interface IPawnable{
    int MoveTo();
    bool CanMove();
    int Attack(int id);
    void Respawn();
    void TakeDamage(int[] damage);

    TileResChild Collect();
}

[Serializable()]
public class Pawn : IPawnable
{
    internal GameObject PawnObject { get; private set; }

    internal List<Resource> Resources { get; private set; }

    [XmlElement("Battery")]
    internal int Battery { get; set; }

    [XmlElement("OffensePower")]
    internal int OffensePower { get; set; }

    [XmlElement("DefensePower")]
    internal int DefensePower { get; set; }
    
    // TODO: maybe add the weight ones to the inventory class
    [XmlElement("WeightLimit")]
    internal int WeightLimit { get; set; }
    internal int CurrentWeight { get; set; }

    public Pawn(GameObject pawnObject)
    {
        PawnObject = pawnObject;
    }

    public int MoveTo()
    {
        return 0;
    }

    public bool CanMove()
    {
        return true;
    }

    public TileResChild Collect()
    {
        return null;
    }

    public int Attack(int id)
    {
        return 0;
    }

    public void Respawn()
    {

    }

    public void TakeDamage(int[] damage)
    {

    }
}
