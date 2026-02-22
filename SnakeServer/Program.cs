namespace SnakeServer;

public class Program
{
    private const int TargetFps = 16;
    private static readonly Coord GridDimensions = new(50, 20);
    private static readonly Random Rand = new();

    private static Coord _applePos = new(Rand.Next(1, GridDimensions.X - 1),
        Rand.Next(1, GridDimensions.Y - 1));

    private static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.Clear();
        Snake snake = new(10, 2);

        while (true)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Score: " + snake.Score);
            snake.ApplyMovementDirection(GetMovementInput());
            CheckAppleCollision(snake, _applePos);
            RenderGame(GridDimensions, snake, _applePos);
            Thread.Sleep(1000 / TargetFps);
        }
    }

    private static Direction GetMovementInput()
    {
        if (!Console.KeyAvailable) return Direction.Invalid;
        var key = Console.ReadKey(true).Key;

        var movementDirection = key switch
        {
            ConsoleKey.LeftArrow or ConsoleKey.A => Direction.Left,
            ConsoleKey.RightArrow or ConsoleKey.D => Direction.Right,
            ConsoleKey.UpArrow or ConsoleKey.W => Direction.Up,
            ConsoleKey.DownArrow or ConsoleKey.S => Direction.Down,
            _ => Direction.Invalid
        };

        return movementDirection;
    }

    private static void RenderGame(Coord grid, Snake snake, Coord apple)
    {
        for (var y = 0; y < grid.Y; y++)
        {
            for (var x = 0; x < grid.X; x++)
                if (x == snake.X && y == snake.Y)
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


    private static void CheckAppleCollision(Snake snake, Coord apple)
    {
        if (snake.X != apple.X || snake.Y != apple.Y) return;
        snake.TailLength++;
        snake.Score++;
        _applePos = new Coord(Rand.Next(1, GridDimensions.X - 1), Rand.Next(1, GridDimensions.Y - 1));
        snake.UpdateSnakeTail(apple);
    }
}