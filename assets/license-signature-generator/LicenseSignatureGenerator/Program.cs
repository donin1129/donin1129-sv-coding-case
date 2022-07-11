using LicenseSignatureGenerator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton(new SignalRConsumer("https://localhost:5001"));

var app = builder.Build();

app.Run();