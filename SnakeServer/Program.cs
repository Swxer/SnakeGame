namespace SnakeServer;

public class Program
{
    private const int TargetFps = 16;
    private const int XDimension = 50;
    private const int YDimension = 20;
    private static readonly Coord GridDimensions = new(XDimension, YDimension);

    private static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.Clear();
        Snake snake = new(10, 2);
        Apple apple = new(GridDimensions);

        while (true)
        {
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Score: " + snake.Score);

            var isEating = CheckAppleCollision(snake, apple);
            if (isEating)
                Apple.PickRandomAppleLocation(GridDimensions);

            snake.ApplyMovementDirection(GetMovementInput(), isEating);
            RenderGame(GridDimensions, snake, apple);

            if (CheckSelfCollision(snake) || CheckWallCollision(snake, GridDimensions))
                snake.Respawn();

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

    private static void RenderGame(Coord grid, Snake snake, Apple apple)
    {
        for (var y = 0; y < grid.Y; y++)
        {
            for (var x = 0; x < grid.X; x++)
            {
                var currentPos = new Coord(x, y);
                if (snake.HeadExistsAtCoordinate(currentPos) || snake.TailExistsAtCoordinate(currentPos))
                    Console.Write('■');
                else if (apple.AppleExistsAtCoordinate(currentPos))
                    Console.Write('A');
                else if (x == 0 || y == 0 || x == grid.X - 1 || y == grid.Y - 1)
                    Console.Write('#');
                else
                    Console.Write(' ');
            }

            Console.WriteLine();
        }
    }

    private static bool CheckAppleCollision(Snake snake, Apple apple)
    {
        return snake.X == apple.X && snake.Y == apple.Y;
    }

    private static bool CheckWallCollision(Snake snake, Coord gridDimension)
    {
        return snake.X <= 0 ||
               snake.X >= gridDimension.X - 1 ||
               snake.Y <= 0 ||
               snake.Y >= gridDimension.Y - 1;
    }

    private static bool CheckSelfCollision(Snake snake)
    {
        return snake.TailExistsAtCoordinate(new Coord(snake.X, snake.Y));
    }
}