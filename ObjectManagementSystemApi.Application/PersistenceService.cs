using Newtonsoft.Json;
using ObjectManagementSystemApi.Domain;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Linq;

namespace ObjectManagementSystemApi.Application
{
    /// <summary>
    ///   Enables persistence of objects and relationships
    /// </summary>
    public class PersistenceService : IPersistenceService
    {
        private readonly IRepository repository;

        public PersistenceService(IRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddObject(GeneralObject newObject)
        {
            await repository.AddObject(newObject);
        }

        public async Task AddRelationship(string fromId, string toId, string relationshipName)
        {
            await repository.AddRelationship(fromId, toId, relationshipName);
        }

        public async Task<List<string>> GetAllRelationshipDistinctNames()
        {
            var result = await repository.CountAllRelationshipsByLabel();

            var resultsDictionary = JsonConvert.DeserializeObject<IEnumerable<Dictionary<string, int>>>(result);

            return resultsDictionary?.SelectMany(x => x.Keys).ToList() ?? new List<string>();
        }
    }
}