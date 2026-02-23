using Auth.Application.Interfaces;
using Auth.Application.Interfaces.Repository;
using Auth.Application.Services;
using Auth.Infra;
using Auth.Infra.Repositories;
using Basket.Consumer;
using Basket.Infra;
using Catalog.Infra;
using GetirYemek.Middleware;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Order.Infra;
using Payment.Infra;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped<IUserRepository,UserRepository>();

var connectionString = builder.Configuration.GetConnectionString("ModularMonolithDb");

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<PaymentDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<CatalogDbContext>(options =>
    options.UseSqlServer(connectionString));

BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDb") ?? "mongodb://root:guest@localhost:27017";
builder.Services.AddSingleton<IMongoClient>(new MongoClient(mongoConnectionString));

builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IMongoClient>().GetDatabase("BasketDb"));


builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<UserCreatedConsumer>();


    x.AddEntityFrameworkOutbox<AuthDbContext>(o =>
    {
        o.UseSqlServer();
        o.UseBusOutbox(); 
    });
/*
    x.AddEntityFrameworkOutbox<CatalogDbContext>(o =>
    {
        o.UseSqlServer();
        o.UseBusOutbox();
    });

    x.AddEntityFrameworkOutbox<OrderDbContext>(o =>
    {
        o.UseSqlServer();
        o.UseBusOutbox();
    });

    x.AddEntityFrameworkOutbox<PaymentDbContext>(o =>
    {
        o.UseSqlServer();
        o.UseBusOutbox();
    });
*/

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var database = scope.ServiceProvider.GetRequiredService<IMongoDatabase>();
    await BasketIndexes.CreateIndexesAsync(database);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
