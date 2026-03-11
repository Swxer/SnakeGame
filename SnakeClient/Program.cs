using System.Numerics;
using Microsoft.AspNetCore.SignalR.Client;
using SnakeShared;

public class Program
{
    const int Width = 50;
    const int Height = 20;
    public static async Task Main()
    {
        var hubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/gameHub")
            .Build();
        
        hubConnection.On<GameState>("GameState", (state) => {
            RenderGame(state); 
        });
        await hubConnection.StartAsync();
        
        _ = Task.Run(async () => {
            while (true)
            {
                var direction = GetDirectionFromKey();
                await hubConnection.InvokeAsync("Move", direction);
                await Task.Delay(50); 
            }
        });
        Console.ReadLine();
    }

    private static Direction GetDirectionFromKey()
    {
        if (!Console.KeyAvailable) return Direction.Invalid;
        var key = Console.ReadKey(true).Key;

        return key switch
        {
            ConsoleKey.LeftArrow or ConsoleKey.A => Direction.Left,
            ConsoleKey.RightArrow or ConsoleKey.D => Direction.Right,
            ConsoleKey.UpArrow or ConsoleKey.W => Direction.Up,
            ConsoleKey.DownArrow or ConsoleKey.S => Direction.Down,
            _ => Direction.Invalid
        };
    }

    private static void RenderGame(GameState state)
    {
        Console.SetCursorPosition(0, 0);
        Console.Write("\u001b[H");
            
        var snakes = state.Snakes;
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                var currentPos = new Vector2(x, y);

                foreach (var snake in snakes)
                {
                    foreach (var segment in snake.Body)
                    {
                        if (segment == currentPos)
                            Console.Write("■");
                    }
                }
                
                if (currentPos == state.ApplePosition)
                    Console.Write("A");
                else if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
                    Console.Write('#');
                else
                    Console.Write(' ');
            }
            Console.WriteLine();
        }
    }
}