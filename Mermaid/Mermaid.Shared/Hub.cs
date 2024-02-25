namespace Mermaid.Shared;

public interface IServerHub
{
    Task SendMessage(string message);
}

public interface IClientHub
{
    Task ReceiveMessage(string message);
}