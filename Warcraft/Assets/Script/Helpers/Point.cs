using System;
using System.Xml.Serialization;

/// <summary>
/// TODO: Is this still necessary?
/// </summary>
[Serializable()]
class Point
{
    int x;
    int y;

    [XmlElement("X")]
    public int X
    {
        get
        {
            return x;
        }

        set
        {
            x = value;
        }
    }

    [XmlElement("Y")]
    public int Y
    {
        get
        {
            return y;
        }

        set
        {
            y = value;
        }
    }

    public Point(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public static Point operator +(Point p1, Point p2)
    {
        return new Point(p1.X + p2.X, p1.Y + p2.Y);
    }

    public static Point operator -(Point p1, Point p2)
    {
        return new Point(p1.X - p2.X, p1.Y - p2.Y);
    }

    public static Point operator +(int variable, Point p2)
    {
        return new Point(variable + p2.X, variable + p2.Y);
    }

    public static Point operator +(Point p1, int variable )
    {
        return variable + p1;
    }

    public static Point operator -(int variable, Point p2)
    {
        return new Point(variable - p2.X, variable - p2.Y);
    }

    public static Point operator -(Point p1, int variable)
    {
        return new Point(p1.X - variable, p1.Y - variable);
    }

    public override string ToString()
    {
        return X + ", " + Y;
    }
}