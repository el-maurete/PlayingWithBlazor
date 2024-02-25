using Mermaid.Shared;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHostedService<StateRotation>();
builder.Services.AddSignalR();
builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p
  .AllowAnyOrigin()
  .AllowAnyHeader()
  .AllowAnyMethod()));

var app = builder.Build();
app.UseCors("AllowAll");
app.MapHub<MyTestHub>("/chathub");
app.Run();

class StateRotation : BackgroundService
{
    private readonly IHubContext<MyTestHub> _hub;

    public StateRotation(IHubContext<MyTestHub> hub) => _hub = hub;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var states = new string[] { "A", "B", "C", "D" };
        var x = 0;
        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            x = ++x % states.Length;
            await _hub.Clients.All.SendAsync(nameof(MyTestHub.ReceiveMessage), states[x]);
        }
    }
}

class MyTestHub : Hub, IServerHub, IClientHub
{
    public async Task SendMessage(string message) =>
        await Clients.All.SendAsync(nameof(ReceiveMessage), message);
    
    // Client hub implementations are only to catch mistakes at build time
    public Task ReceiveMessage(string message) => throw new NotImplementedException();
}
