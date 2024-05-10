using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CrystalSharp;
using CrystalSharp.EventStores.EventStoreDb.Extensions;
using CrystalSharp.MsSql.Extensions;
using CrystalSharpReadModelStoreSqlServerExample.Application.CommandHandlers;
using CrystalSharpReadModelStoreSqlServerExample.Application.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string eventStoreConnectionString = builder.Configuration.GetConnectionString("AppEventStoreConnectionString");
string readModelStoreConnectionString = builder.Configuration.GetConnectionString("AppReadModelStoreConnectionString");
MsSqlSettings readModelStoreDbSettings = new(readModelStoreConnectionString);

CrystalSharpAdapter.New(builder.Services)
    .AddCqrs(typeof(PlaceOrderCommandHandler))
    .AddEventStoreDbEventStore<int>(eventStoreConnectionString)
    .AddMsSqlReadModelStore<AppReadModelStoreDbContext, int>(readModelStoreDbSettings)
    .CreateResolver();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
