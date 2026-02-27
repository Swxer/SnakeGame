namespace SnakeServer;

public class Snake(int startingX, int startingY)
{
    private readonly Coord _snakeHead = new(startingX, startingY);
    private readonly Queue<Coord> _snakeTail = new();
    private bool _hasEaten;
    private Direction _movementDirection = Direction.Down;

    public int Score => _snakeTail.Count;
    public int X => _snakeHead.X;
    public int Y => _snakeHead.Y;
    public bool HasEaten { set => _hasEaten = value; }

    public void ApplyMovementDirection(Direction direction)
    {
        GrowTail();
        if (direction != Direction.Invalid) _movementDirection = direction;

        switch (_movementDirection)
        {
            case Direction.Left: _snakeHead.X--; break;
            case Direction.Right: _snakeHead.X++; break;
            case Direction.Up: _snakeHead.Y--; break;
            case Direction.Down: _snakeHead.Y++; break;
        }
    }

    public bool HeadExistsAtCoordinate(Coord coord)
    {
        return coord == _snakeHead;
    }

    public bool TailExistsAtCoordinate(Coord coord)
    {
        return _snakeTail.Contains(coord);
    }

    private void GrowTail()
    {
        _snakeTail.Enqueue(new Coord(_snakeHead.X, _snakeHead.Y));
        if (_hasEaten)
            _hasEaten = false;
        else if (_snakeTail.Count > 0)
            _snakeTail.Dequeue();
    }
}