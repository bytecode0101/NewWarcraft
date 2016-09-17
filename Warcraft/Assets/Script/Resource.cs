using System;

public abstract class Resource : Element, ICollectable
{
    public bool IsCollectable()
    {
        return true;
    }
}

interface IBlockable
{
    bool IsBlockable();
}

interface IHazardable
{
    bool IsHazardable();
}

interface IAttackable
{
    bool IsAttackable();
}

class FilledSpace : Element, IBlockable
{
    public bool IsBlockable()
    {
        return true;
    }
}

class EmptySpace : Element
{

}

class ResourceSpace : Resource
{

}

class DangerSpace : Element, IHazardable
{
    public bool IsHazardable()
    {
        return true;
    }
}

class EnemySpace : Resource, IHazardable, IAttackable
{
    public bool IsAttackable()
    {
        return true;
    }

    public bool IsHazardable()
    {
        return true;
    }
}

class MercenarySpace : Resource, IHazardable, IAttackable
{
    public bool IsAttackable()
    {
        return true;
    }

    public bool IsHazardable()
    {
        return true;
    }
}