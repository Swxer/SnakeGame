using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace SnakeServer;
using System.Numerics;

public class Program
{
    private static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.Clear();
        Run();
    }

    private static void RenderGame(Vector2 grid, List<Snake> snakes, Apple apple)
    {
        var width = (int)grid.X;
        var height = (int)grid.Y;

        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var currentPos = new Vector2(x, y);
                
                if (snakes.Any(s => s.HeadExistsAtCoordinate(currentPos)))
                    Console.Write('■');
                else if (snakes.Any(s => s.TailIntersectsWithCoordinate(currentPos)))
                    Console.Write('T');
                else if (Apple.AppleExistsAtCoordinate(currentPos))
                    Console.Write('A');
                else if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
                    Console.Write('#');
                else
                    Console.Write(' ');
            }
            Console.WriteLine();
        }
    }

    public static Task Run()
    {
        var builder = WebApplication.CreateBuilder();
    
        builder.Services.AddSignalR();
        builder.Services.AddSingleton<GameEngine>();
    
        var app = builder.Build();
    
        var engine = app.Services.GetRequiredService<GameEngine>();
        engine.SetHubContext(app.Services.GetRequiredService<IHubContext<GameHub>>());
    
        app.MapHub<GameHub>("/gameHub");
    
        Console.WriteLine("Fight for snake oil");
    
        _ = engine.StartAsync();
        app.Run();
        return Task.CompletedTask;
    }
}