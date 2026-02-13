
namespace SnakeServer
{
    public class Coord : IEquatable<Coord>
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        
        public Coord(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
        
        public bool Equals(Coord? other)
        {
            if (other is null) return false;
            return X == other.X && Y == other.Y;
        }
        public override bool Equals(object? obj) => Equals(obj as Coord);
        public override int GetHashCode() => HashCode.Combine(X, Y);
    }
}

