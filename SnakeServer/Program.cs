namespace SnakeServer;

public class Program
{
    private const int FrameDelayMs = 100;
    private static readonly Coord GridDimensions = new(50, 20);
    private static readonly Random Rand = new();
    private static Coord _snakePos = new(10, 2);
    private static Coord _applePos = new(Rand.Next(1, GridDimensions.X - 1),
        Rand.Next(1, GridDimensions.Y - 1));

    private static Direction _movementDirection = Direction.Down;
    private static List<Coord> _snakeTail = new();
    private static int _tailLength = 1;

    private static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.Clear();

        while (true)
        {
            Console.SetCursorPosition(0, 0);
            _snakePos.ApplyMovementDirection(_movementDirection);
            RenderGame(GridDimensions, _snakePos, _applePos);
            UpdateSnakeTail(_snakePos);
            GetMovementInput();
        }
    }

    private static void GetMovementInput()
    {
        var time = DateTime.Now;
        while ((DateTime.Now - time).TotalMilliseconds < FrameDelayMs)
        {
            if (!Console.KeyAvailable) continue;
            var key = Console.ReadKey(true).Key;

            _movementDirection = key switch
            {
                ConsoleKey.LeftArrow or ConsoleKey.A => Direction.Left,
                ConsoleKey.RightArrow or ConsoleKey.D => Direction.Right,
                ConsoleKey.UpArrow or ConsoleKey.W => Direction.Up,
                ConsoleKey.DownArrow or ConsoleKey.S => Direction.Down,
                _ => _movementDirection
            };
        }
    }

    private static void RenderGame(Coord grid, Coord snake, Coord apple)
    {
        for (var y = 0; y < grid.Y; y++)
        {
            for (var x = 0; x < grid.X; x++)
                if (x == snake.X && y == snake.Y || _snakeTail.Contains(new Coord(x, y)))
                    Console.Write('■');
                else if (x == apple.X && y == apple.Y)
                    Console.Write('A');
                else if (x == 0 || y == 0 || x == grid.X - 1 || y == grid.Y - 1)
                    Console.Write('#');
                else
                    Console.Write(' ');
            Console.WriteLine();
        }
    }

    private static void UpdateSnakeTail(Coord snake)
    {
        _snakeTail.Add(new Coord(snake.X, snake.Y));
        if (_snakeTail.Count > _tailLength)
            _snakeTail.RemoveAt(0);
    }
}