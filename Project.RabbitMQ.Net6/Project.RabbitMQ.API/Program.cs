using Project.RabbitMQ.Infrastructure;
using Project.RabbitMQ.Infrastructure.Interface;
using Project.RabbitMQ.Services;
using Project.RabbitMQ.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddDbContext<DbContextData>();
builder.Services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();

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
