using Newtonsoft.Json;
using ObjectManagementSystemApi.Application;
using ObjectManagementSystemApi.Application.Serializers;
using ObjectManagementSystemApi.Application.Services;
using ObjectManagementSystemApi.Domain;
using ObjectManagementSystemApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

RegisterDependencies(builder.Services);
AddServices(builder.Services); 

var app = builder.Build();

ActivateServices(app);

app.MapGet("/objects", async (IPersistenceService persistenceService) => 
{
	var objects = await persistenceService.GetAllObjects();

	return Results.Text(JsonConvert.SerializeObject(objects), contentType: "application/json");
});

app.MapPost("/objects", async (GeneralObject newObject, IPersistenceService persistenceService) =>
{
	await persistenceService.AddObject(newObject);
});

app.MapDelete("/objects/{id}", async (string id, IPersistenceService persistenceService) =>
{
    await persistenceService.DeleteObject(id);
});

app.MapPut("/objects", async (GeneralObject generalObject, IPersistenceService persistenceService) =>
{
    await persistenceService.UpdateObject(generalObject);
});

app.MapGet("/relationships", async (IPersistenceService persistenceService) =>
{
    var relationships = await persistenceService.GetAllRelationships();

    return Results.Text(JsonConvert.SerializeObject(relationships), contentType: "application/json");
});

app.MapPost("/relationships", async (Relationship relationship, IPersistenceService persistenceService) =>
{
    await persistenceService.AddRelationship(relationship);
});

app.MapDelete("/relationships/{id}", async (string id, IPersistenceService persistenceService) =>
{
    await persistenceService.DeleteRelationship(id);
});

app.MapPut("/relationships", async (Relationship relationship, IPersistenceService persistenceService) =>
{
    await persistenceService.UpdateRelationship(relationship);
});

app.Run();

void RegisterDependencies(IServiceCollection services)
{
	builder.Services.AddSingleton<GremlinService>();
	builder.Services.AddSingleton<IRepository, Repository>();
	builder.Services.AddTransient<ISerializerService, SerializerService>();
	builder.Services.AddTransient<IPersistenceService, PersistenceService>();
}

void AddServices(IServiceCollection services)
{
    builder.Services.AddCors();
    builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();
}

void ActivateServices(WebApplication app)
{
    app.UseCors(builder => builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .SetIsOriginAllowed((host) => true)
                    .AllowCredentials()
                ); // Warning: Only for the PoC application, in further developments proper cors restrictions must be set up.

    app.UseSwagger();
	app.UseSwaggerUI();
}