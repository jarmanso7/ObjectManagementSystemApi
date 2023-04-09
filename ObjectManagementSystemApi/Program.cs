using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ObjectManagementSystemApi.Application;
using ObjectManagementSystemApi.Domain;
using ObjectManagementSystemApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<GremlinService>();
builder.Services.AddSingleton<IRepository,Repository>();

builder.Services.AddTransient<IPersistenceService, PersistenceService>();

builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.MapGet("/test", async (IPersistenceService persistenceService) =>
{
    var results = await persistenceService.GetAllRelationshipDistinctNames();

    return JsonConvert.SerializeObject(results);
});

app.MapGet("/objects", () =>
{
    var results = new List<GeneralObject>()
    {
        new GeneralObject
        {
            Id = Guid.NewGuid(),
            Name = "Xavi",
            Description = "A funny guy.",
            Type = "Person"
        },
        new GeneralObject
        {
            Id = Guid.NewGuid(),
            Name = "Marc",
            Description = "A very handsome guy.",
            Type = "Person"
        },
        new GeneralObject
        {
            Id = Guid.NewGuid(),
            Name = "Martí",
            Description = "A smart guy.",
            Type = "Person"
        },
    };
    return JsonConvert.SerializeObject(results);
});

app.MapPost("/objects", async (GeneralObject newObject, IPersistenceService persistenceService) =>
{
    await persistenceService.AddObject(newObject);
});

app.MapPut("/objects/{id}", () =>
{

});

app.MapGet("/relationships", () =>
{
    var results = new List<Relationship>()
    {
        new Relationship
        {
            Name = "Knows",
            FromType = "Person",
            ToType = "Person"
        },
        new Relationship
        {
            Name = "Has",
            FromType = "Person",
            ToType = "Tool"
        }
    };
    return JsonConvert.SerializeObject(results);
});

app.MapPost("/relationships", async (string fromId, string toId, string relationshipName, IPersistenceService persistenceService) =>
{
    await persistenceService.AddRelationship(fromId, toId, relationshipName);
});

app.Run();