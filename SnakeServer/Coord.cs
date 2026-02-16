namespace SnakeServer
{
    public class Coord(int x, int y) : IEquatable<Coord>
    {
        public int X { get; private set; } = x;
        public int Y { get; private set; } = y;

        public bool Equals(Coord? other)
        {
            if (other is null) return false;
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object? obj) => Equals(obj as Coord);
        public override int GetHashCode() => HashCode.Combine(X, Y);

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
}