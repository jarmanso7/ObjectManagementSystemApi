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

app.MapGet("/test", async (IPersistenceService persistenceService) =>
{
    var results = await persistenceService.GetAllRelationshipDistinctNames();

    return JsonConvert.SerializeObject(results);
});

app.MapGet("/objects", async (IPersistenceService persistenceService) => 
{
	var objects = await persistenceService.GetAllObjects();

	return Results.Text(JsonConvert.SerializeObject(objects), contentType: "application/json");
});

app.MapPost("/objects", async (GeneralObject newObject, IPersistenceService persistenceService) =>
{
	await persistenceService.AddObject(newObject);

	//TODO: return the newly created object
});

app.MapPut("/objects/{id}", () =>
{

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
	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();
	builder.Services.AddCors();
}

void ActivateServices(WebApplication app)
{
	app.UseCors(builder =>
	{
		builder.AllowAnyOrigin(); // Only for the PoC, in further developments proper cors restrictions must be set up.
	});

	app.UseSwagger();
	app.UseSwaggerUI();
}