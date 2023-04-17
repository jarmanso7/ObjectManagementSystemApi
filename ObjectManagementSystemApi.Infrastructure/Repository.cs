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

        public async Task<string> GetAllObjects()
        {
            var request = $"g.V()";

            return await gremlinService.SubmitRequest(request);
        }

        public async Task DeleteObject(string generalObjectId)
        {
            var request = $"g.V('{generalObjectId}').drop()";

            await gremlinService.SubmitRequest(request);
        }

        public async Task UpdateObject(GeneralObject generalObject)
        {
            var request = $"g.V().hasId('{generalObject.Id}').property('description', '{generalObject.Name}').property('description', '{generalObject.Description}')";

            await gremlinService.SubmitRequest(request);
        }

        public async Task AddRelationship(Relationship relationship)
        {
            var request = $"g.V('{relationship.FromId}').addE('{relationship.Type}').property('id', '{relationship.Id}').to(g.V('{relationship.ToId}'))";

            await gremlinService.SubmitRequest(request);
        }

        public async Task<string> GetAllRelationships()
        {
            var request = $"g.E()";

            return await gremlinService.SubmitRequest(request);
        }

        public async Task DeleteRelationship(string relationshipId)
        {
            var request = $"g.E('{relationshipId}').drop()";

            await gremlinService.SubmitRequest(request);
        }

        public async Task UpdateRelationship(Relationship relationship)
        {
            await DeleteRelationship(relationship.Id);

            await AddRelationship(relationship);
        }
    }
}