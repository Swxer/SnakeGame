
namespace SnakeServer
{
    internal class Coord
    {
        public int X { get; set; }
        public int Y { get; set; }
        
        public Coord(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object? obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            
            Coord other = (Coord)obj;
            return this.X == other.X && this.Y == other.Y;
        }
    }
}

