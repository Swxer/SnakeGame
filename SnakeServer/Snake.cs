namespace SnakeServer;

public class Snake
{
    private static readonly Queue<Coord> _snakeTail = new();
    private static Direction _movementDirection = Direction.Down;

    private readonly Coord _snakeHead;


    public Snake(int startingX, int startingY)
    {
        _snakeHead = new Coord(startingX, startingY);
    }

    public int Score { get; set; }
    public int TailLength { get; set; }
    public int X => _snakeHead.X;
    public int Y => _snakeHead.Y;

    public void UpdateSnakeTail(Coord apple)
    {
        _snakeTail.Enqueue(new Coord(apple.X, apple.Y));
        if (_snakeTail.Count > TailLength)
            _snakeTail.Dequeue();
    }

    public void ApplyMovementDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                _snakeHead.X--;
                _movementDirection = Direction.Left;
                break;
            case Direction.Right:
                _snakeHead.X++;
                _movementDirection = Direction.Right;
                break;
            case Direction.Up:
                _snakeHead.Y--;
                _movementDirection = Direction.Up;
                break;
            case Direction.Down:
                _snakeHead.Y++;
                _movementDirection = Direction.Down;
                break;
            case Direction.Invalid:
                ApplyMovementDirection(_movementDirection);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }
    }
}