using Mermaid.Shared;
using Microsoft.AspNetCore.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSignalR();
builder.Services.AddCors(options => options.AddPolicy("AllowAll", p => p
  .AllowAnyOrigin()
  .AllowAnyHeader()
  .AllowAnyMethod()));

var app = builder.Build();
app.UseCors("AllowAll");
app.MapHub<MyTestHub>("/chathub");
app.Run();

class MyTestHub : Hub, IServerHub, IClientHub
{
    public async Task SendMessage(string message) =>
        await Clients.All.SendAsync(nameof(ReceiveMessage), message);
    
    // Client hub implementations are only to catch mistakes at build time
    public Task ReceiveMessage(string message) => throw new NotImplementedException();
}
