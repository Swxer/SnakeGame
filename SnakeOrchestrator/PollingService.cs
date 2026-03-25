namespace SnakeOrchestrator;

public class PollingService
{
    private readonly OrchestratorService _orchestrator;
    private readonly PeriodicTimer _timer;
    
    private const int PollIntervalSeconds = 5;
    private const int MaxPlayersPerServer = 3;

    public PollingService(OrchestratorService orchestrator)
    {
        _orchestrator = orchestrator;
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(PollIntervalSeconds));
    }

    public async Task StartAsync()
    {
        while (await _timer.WaitForNextTickAsync())
        {
            await PollServersAsync();
        }
    }

    private async Task PollServersAsync()
    {
        await UpdateServerPlayerCount();
        await ScaleUpIfNeeded();
        await _orchestrator.CleanupEmptyServers();
    }

    private async Task UpdateServerPlayerCount()
    {
        foreach (var server in _orchestrator.GetAllServers())
        {
            var playerCount = await _orchestrator.GetPlayerCountAsync(server.Port);
            server.PlayerCount = playerCount;
        }
    }

    private async Task ScaleUpIfNeeded()
    {
        var servers = _orchestrator.GetAllServers();

        if (servers.Count == 0)
        {
            Console.WriteLine("[SCALE] No servers, starting initial server...");
            await _orchestrator.StartServerAsync();
            return;
        }

        var hasSpace = servers.Any(s => s.PlayerCount < MaxPlayersPerServer);

        if (!hasSpace)
        {
            Console.WriteLine($"[SCALE] All servers at max capacity ({MaxPlayersPerServer}), starting new server...");
            await _orchestrator.StartServerAsync();
        }
    }
}