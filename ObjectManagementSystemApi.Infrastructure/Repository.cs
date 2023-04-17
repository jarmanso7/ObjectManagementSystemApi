using Newtonsoft.Json;
using ObjectManagementSystemApi.Application;
using ObjectManagementSystemApi.Domain;
namespace ObjectManagementSystemApi.Infrastructure
{
    public class Repository : IRepository
    {
        private readonly GremlinService gremlinService;

        public Repository(GremlinService gremlinService)
        {
            this.gremlinService = gremlinService;
        }

        public async Task AddObject(GeneralObject newObject)
        {
            var request = $"g.addV('{newObject.Name}').property('id', '{newObject.Id}').property('name', '{newObject.Name}').property('description', '{newObject.Description}').property('type', '{newObject.Type}').property('pk', 'pk')";

            await gremlinService.SubmitRequest(request);
        }

        public async Task AddRelationship(Relationship relationship)
        {
            if (await UnidirectionalRelationshipExists(relationship.FromId, relationship.ToId, relationship.Type))
            {
                return;
            }
   
            var request = $"g.V('{relationship.FromId}').addE('{relationship.Type}').property('id', '{relationship.Id}').to(g.V('{relationship.ToId}'))";

            await gremlinService.SubmitRequest(request);
        }

        public async Task DeleteObject(string generalObjectId)
        {
            var request = $"g.V('{generalObjectId}').drop()";

            await gremlinService.SubmitRequest(request);
        }

        public async Task DeleteRelationship(string relationshipId)
        {
            var request = $"g.E('{relationshipId}').drop()";

            await gremlinService.SubmitRequest(request);
        }

        public async Task<string> CountAllRelationshipsByLabel()
        {
            var request = $"g.E().groupCount().by(label)";

            return await gremlinService.SubmitRequest(request);
        }

        public async Task<string> GetAllObjects()
        {
            var request = $"g.V()";

			return await gremlinService.SubmitRequest(request);
		}

        public async Task<string> GetAllRelationships()
        {
            var request = $"g.E()";

			return await gremlinService.SubmitRequest(request);
		}

        public async Task<bool> UnidirectionalRelationshipExists(string fromId, string toId, string relationshipName)
        {
            var request = $"g.V('{fromId}').outE('{relationshipName}').where(otherV().is('{toId}')).Count()";

            var count = JsonConvert.DeserializeObject<int[]>(await gremlinService.SubmitRequest(request));

            switch (count[0])
            {
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    throw new Exception("Error: the relationship has been found more than once");
            }
        }
    }
}