using UnityEngine;
using System.Collections;


public class Pawn : MonoBehaviour
{

    internal int battery;
    internal int position;
    internal int id;

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
