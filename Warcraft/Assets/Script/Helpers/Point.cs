class Point
{
    int x;
    int y;

    public Point(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public static Point operator +(Point p1, Point p2)
    {
        return new Point(p1.x + p2.x, p1.y + p2.y);
    }

    public static Point operator -(Point p1, Point p2)
    {
        return new Point(p1.x - p2.x, p1.y - p2.y);
    }

    public static Point operator +(int variable, Point p2)
    {
        return new Point(variable + p2.x, variable + p2.y);
    }

    public static Point operator +(Point p1, int variable )
    {
        return variable + p1;
    }

    public static Point operator -(int variable, Point p2)
    {
        return new Point(variable - p2.x, variable - p2.y);
    }

    public static Point operator -(Point p1, int variable)
    {
        return new Point(p1.x - variable, p1.y - variable);
    }
}