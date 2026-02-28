namespace SnakeServer;

public class Snake(int startingX, int startingY)
{
    private readonly Coord _snakeHead = new(startingX, startingY);
    private readonly Queue<Coord> _snakeTail = new();
    private Direction _movementDirection = Direction.Down;

    public int Score => _snakeTail.Count;
    public int X => _snakeHead.X;
    public int Y => _snakeHead.Y;

    public void ApplyMovementDirection(Direction direction, bool isEating)
    {
        if (direction != Direction.Invalid && !IsOppositeDirection(direction))
            _movementDirection = direction;
        
        if (isEating)
            GrowTail();
        else
            MaintainTail();

        AdvanceSnakeHead();
    }

    private bool IsOppositeDirection(Direction newDirection)
    {
        if (_snakeTail.Count == 0) return false;

        return (newDirection == Direction.Up && _movementDirection == Direction.Down) ||
               (newDirection == Direction.Down && _movementDirection == Direction.Up) ||
               (newDirection == Direction.Left && _movementDirection == Direction.Right) ||
               (newDirection == Direction.Right && _movementDirection == Direction.Left);
    }

    private void AdvanceSnakeHead()
    {
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
    
    public void Respawn()
    {
        _snakeTail.Clear();
        _snakeHead.X = 10;
        _snakeHead.Y = 2;
        _movementDirection = Direction.Down;
    }

    private void MaintainTail()
    {
        _snakeTail.Enqueue(new Coord(_snakeHead.X, _snakeHead.Y));
        if (_snakeTail.Count > 0)
            _snakeTail.Dequeue();
    }

    private void GrowTail()
    {
        _snakeTail.Enqueue(new Coord(_snakeHead.X, _snakeHead.Y));
    }
}