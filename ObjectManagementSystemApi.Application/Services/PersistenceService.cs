using Newtonsoft.Json;
using ObjectManagementSystemApi.Application.Serializers;
using ObjectManagementSystemApi.Domain;
using System.Collections.ObjectModel;

namespace ObjectManagementSystemApi.Application.Services
{
    /// <summary>
    ///   Enables persistence of objects and relationships
    /// </summary>
    public class PersistenceService : IPersistenceService
    {
        private readonly IRepository repository;
        private readonly ISerializerService serializerService;

        public PersistenceService(
            IRepository repository,
            ISerializerService serializerService)
        {
            this.repository = repository;
            this.serializerService = serializerService;
        }

        public async Task AddObject(GeneralObject newObject)
        {
            newObject.Id = string.IsNullOrEmpty(newObject.Id) ? Guid.NewGuid().ToString() : newObject.Id;

            await repository.AddObject(newObject);
        }

        public async Task AddRelationship(Relationship relationship)
        {
			relationship.Id = string.IsNullOrEmpty(relationship.Id) ? Guid.NewGuid().ToString() : relationship.Id;

			await repository.AddRelationship(relationship);
        }

        public async Task<List<string>> GetDistinctRelationshipsNames()
        {
            var result = await repository.CountAllRelationshipsByLabel();

            var resultsDictionary = JsonConvert.DeserializeObject<IEnumerable<Dictionary<string, int>>>(result);

            return resultsDictionary?.SelectMany(x => x.Keys).ToList() ?? new List<string>();
        }

        public async Task<ReadOnlyCollection<GeneralObject>> GetAllObjects()
        {
            var gremlinVertices = await repository.GetAllObjects();

            return serializerService.GremlinVerticesToGeneralObjects(gremlinVertices);
        }

        public async Task<ReadOnlyCollection<Relationship>> GetAllRelationships()
        {
            var gremlinEdges = await repository.GetAllRelationships();

            return serializerService.GremlinEdgesToRelationships(gremlinEdges);
        }

        public async Task<List<string>> GetAllRelationshipDistinctNames()
        {
            var result = await repository.CountAllRelationshipsByLabel();

            var resultsDictionary = JsonConvert.DeserializeObject<IEnumerable<Dictionary<string, int>>>(result);

            return resultsDictionary?.SelectMany(x => x.Keys).ToList() ?? new List<string>();
        }
    }
}