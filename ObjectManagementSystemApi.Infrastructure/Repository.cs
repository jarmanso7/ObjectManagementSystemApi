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
            var request = $"g.addV('{newObject.Type}').property('id', '{newObject.Id}').property('name', '{newObject.Name}').property('description', '{newObject.Description}').property('pk', 'pk')";

            await gremlinService.SubmitRequest(request);
        }

        public async Task AddRelationship(string fromId, string toId, string relationshipName)
        {
            if (await UnidirectionalRelationshipExists(fromId, toId, relationshipName))
            {
                return;
            }
   
            var request = $"g.V('{fromId}').addE('{relationshipName}').to(g.V('{toId}'))";

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

            var count = Int32.Parse(await gremlinService.SubmitRequest(request));

            switch (count)
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