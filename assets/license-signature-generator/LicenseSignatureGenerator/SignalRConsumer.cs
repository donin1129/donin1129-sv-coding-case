using Microsoft.AspNetCore.SignalR.Client;

namespace LicenseSignatureGenerator;

public sealed class SignalRConsumer : IAsyncDisposable
{
    private readonly string _hostDomain;

    private HubConnection _hubConnection;

    public SignalRConsumer(string hostDomain)
    {
        _hostDomain = hostDomain;

        Console.WriteLine("Initializing SignalR client. ");

        _hubConnection = new HubConnectionBuilder()
            .WithUrl(new Uri($"{_hostDomain}/LicenseSignatureGenerator"), options =>
            {
                options.UseDefaultCredentials = true;
            })
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.StartAsync().Wait();

        Console.WriteLine("HubConnectionId: " + _hubConnection.ConnectionId);

        _hubConnection.On<string>(
            "GenerateLicense", OnGenerateLicenseAsync);
    }

    public Task StartNotificationConnectionAsync() =>
        _hubConnection.StartAsync();
    
    public async Task OnGenerateLicenseAsync(string userId)
    {
        Console.WriteLine($"Generate license request for user with Id: {userId}");

        string license = "ll-" + Guid.NewGuid().ToString();

        Console.WriteLine($"Generated license string: {license}");

        await _hubConnection.InvokeAsync<Tuple<string, string>> ("OnLicenseGenerated", new Tuple<string, string>(userId, license));
    }

    public async ValueTask DisposeAsync()
    {
        if (_hubConnection is not null)
        {
            await _hubConnection.DisposeAsync();
            _hubConnection = null;
        }
    }
}