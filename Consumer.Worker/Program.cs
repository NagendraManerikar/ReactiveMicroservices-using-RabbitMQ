using Consumer.Worker;
using Infrastructure.Messaging;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<IConnectionProvider>(x =>
{
    var connectionString =
        builder.Configuration["RabbitMQ:ConnectionString"];

    return ConnectionProvider
        .CreateAsync(connectionString!)
        .GetAwaiter()
        .GetResult();
});

builder.Services.AddHostedService<ConsumerService>();

var host = builder.Build();
host.Run();
