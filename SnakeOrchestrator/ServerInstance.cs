namespace SnakeOrchestrator;

public class ServerInstance
{
    public int Port { get; set; }
    public string ContainerId { get; set; } = "";
    public int PlayerCount { get; set; }
    public DateTime? EmptySince  { get; set; }
}