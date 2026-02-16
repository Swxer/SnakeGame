namespace SnakeServer;

public record Coord
{
    public Coord(int x, int y)
    {
        X = x;
        Y = y;
    }

    public int X { get; private set; }
    public int Y { get; private set; }


    public void ApplyMovementDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                X--;
                break;
            case Direction.Right:
                X++;
                break;
            case Direction.Up:
                Y--;
                break;
            case Direction.Down:
                Y++;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }
}