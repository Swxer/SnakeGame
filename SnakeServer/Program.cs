using SnakeServer;

Coord gridDimensions = new Coord(50,20);
Coord snakePos = new Coord(10,0);

for (int y = 0; y < gridDimensions.Y; y++)
{
    for (int x = 0; x < gridDimensions.X; x++)
    {
        if (x == 0 || y == 0 || x == gridDimensions.X - 1 || y == gridDimensions.Y - 1)
        {
            Console.Write('#');
        }
        else
        {
            Console.Write(' ');
        }
    }
    Console.WriteLine();
}
