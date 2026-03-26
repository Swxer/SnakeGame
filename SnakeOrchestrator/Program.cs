using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SnakeOrchestrator;

public class Program
{

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSingleton<OrchestratorService>();
        builder.Services.AddSingleton<PollingService>();

        var app = builder.Build();

        var orchestrator = app.Services.GetRequiredService<OrchestratorService>();
        var pollingService = app.Services.GetRequiredService<PollingService>();
    }

}

