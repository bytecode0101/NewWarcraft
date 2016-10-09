using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;

[Serializable()]
public class Pawn : MonoBehaviour
{
    private int battery;
    private Point position;
    private int id;

    [XmlElement("Position")]
    internal Point Position
    {
        get
        {
            return position;
        }

        set
        {
            position = value;
        }
    }

    [XmlElement("Id")]
    internal int Id
    {
        get
        {
            return id;
        }

        set
        {
            id = value;
        }
    }

    [XmlElement("Battery")]
    internal int Battery
    {
        get
        {
            return battery;
        }

        set
        {
            battery = value;
        }
    }

    internal int MoveTo()
    {
        return 0;
    }

    internal bool CanMove()
    {
        return true;
    }

    internal Resource Collect()
    {
        return null;
    }

    internal int Attack(int id)
    {
        return 0;
    }

    internal void Respawn()
    {

    }

    internal void TakeDamage(int[] damage)
    {

    }

    // Use this for initialization
    internal void Start()
    {

    }

    // Update is called once per frame
    internal void Update()
    {

    }
}
