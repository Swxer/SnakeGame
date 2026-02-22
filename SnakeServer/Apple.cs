namespace SnakeServer;

public class Apple
{
    private static readonly Random Rand = new();
    private static Coord _apple = new Coord(0, 0);

    public Apple(Coord gridDimensions)
    {
        PickRandomAppleLocation(gridDimensions);
    }

    public int X => _apple.X;
    public int Y => _apple.Y;

    public static void PickRandomAppleLocation(Coord gridDimensions)
    {
        var appleRandX = Rand.Next(1, gridDimensions.X - 1);
        var appleRandY = Rand.Next(1, gridDimensions.Y - 1);
        _apple = new Coord(appleRandX, appleRandY);
    }
}