namespace SnakeServer;
using System.Numerics;

public class Apple
{
    private static readonly Random Rand = new();
    private static Vector2 _apple = new(0, 0);

    public Apple(Vector2 gridDimensions)
    {
        PickRandomAppleLocation(gridDimensions);
    }

    public static int X => (int)_apple.X;
    public static int Y => (int)_apple.Y;

    public static bool AppleExistsAtCoordinate(Vector2 coord)
    {
        return _apple == coord;
    }

    public static void PickRandomAppleLocation(Vector2 gridDimensions)
    {
        var appleRandX = Rand.Next(1, (int)(gridDimensions.X - 1));
        var appleRandY = Rand.Next(1, (int)(gridDimensions.Y - 1));
        _apple = new Vector2(appleRandX, appleRandY);
    }
}