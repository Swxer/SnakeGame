using Microsoft.AspNetCore.SignalR;
using SnakeShared;


namespace SnakeServer;

public class GameHub : Hub
{
    private readonly GameEngine _gameEngine;
    
    public GameHub(GameEngine gameEngine)
    {
        _gameEngine = gameEngine;
    }
    
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("Snake Player", $"Player {Context.ConnectionId} connected");
  
    }
    
    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _gameEngine.RemovePlayer(Context.ConnectionId);
        return Task.CompletedTask;
    }

    public Task Move(Direction direction)
    {
        _gameEngine.QueueInput(Context.ConnectionId, direction);
        return Task.CompletedTask;
    }
}