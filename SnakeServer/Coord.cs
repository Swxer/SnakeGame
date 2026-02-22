namespace SnakeServer;

public record Coord
{
    public Coord(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; set; }
    public int Y { get; set; }
}