namespace SnakeOrchestrator;

public class PollingService
{
    private readonly OrchestratorService _orchestrator;
    private readonly PeriodicTimer _timer;
    
    private const int PollIntervalSeconds = 5;
    private const int MaxPlayerPerServer = 3;

    public PollingService(OrchestratorService orchestrator)
    {
        _orchestrator = orchestrator;
        _timer = new PeriodicTimer(TimeSpan.FromSeconds(PollIntervalSeconds));
    }
}