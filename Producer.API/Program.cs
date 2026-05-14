using Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConnectionProvider>(x =>
{
    var connectionString =
        builder.Configuration["RabbitMQ:ConnectionString"];

    return ConnectionProvider
        .CreateAsync(connectionString!)
        .GetAwaiter()
        .GetResult();
});


// Add services to the container.
builder.Services.AddSingleton<IProducer, Producer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
