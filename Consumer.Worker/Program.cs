using Consumer.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<ConsumerService>();

var host = builder.Build();
host.Run();
