using ObjectManagementSystemApi.Application;
using ObjectManagementSystemApi.Domain;
namespace ObjectManagementSystemApi.Infrastructure
{
    /// <summary>
    /// Implements methods to interact with and persist information to an Apache Gremlin database via GremlinService.
    /// </summary>
    /// <seealso cref="ObjectManagementSystemApi.Application.IRepository" />
    public class Repository : IRepository
    {
        private readonly GremlinService gremlinService;

        public Repository(GremlinService gremlinService)
        {
            this.gremlinService = gremlinService;
        }

        /// <summary>
        /// Adds the object.
        /// </summary>
        /// <param name="newObject">The new object.</param>
        public async Task AddObject(GeneralObject newObject)
        {
            var request = $"g.addV('{newObject.Name}').property('id', '{newObject.Id}').property('name', '{newObject.Name}').property('description', '{newObject.Description}').property('type', '{newObject.Type}').property('pk', 'pk')";

            await gremlinService.SubmitRequest(request);
        }

        /// <summary>
        /// Gets all objects.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAllObjects()
        {
            var request = $"g.V()";

            return await gremlinService.SubmitRequest(request);
        }

        /// <summary>
        /// Deletes the object.
        /// </summary>
        /// <param name="generalObjectId">The general object identifier.</param>
        public async Task DeleteObject(string generalObjectId)
        {
            var request = $"g.V('{generalObjectId}').drop()";

            await gremlinService.SubmitRequest(request);
        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="generalObject">The general object.</param>
        public async Task UpdateObject(GeneralObject generalObject)
        {
            var request = $"g.V().hasId('{generalObject.Id}').property('description', '{generalObject.Name}').property('description', '{generalObject.Description}')";

            await gremlinService.SubmitRequest(request);
        }

        /// <summary>
        /// Adds the relationship.
        /// </summary>
        /// <param name="relationship">The relationship.</param>
        public async Task AddRelationship(Relationship relationship)
        {
            var request = $"g.V('{relationship.FromId}').addE('{relationship.Type}').property('id', '{relationship.Id}').to(g.V('{relationship.ToId}'))";

            await gremlinService.SubmitRequest(request);
        }

        /// <summary>
        /// Gets all relationships.
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAllRelationships()
        {
            var request = $"g.E()";

            return await gremlinService.SubmitRequest(request);
        }

        /// <summary>
        /// Deletes the relationship.
        /// </summary>
        /// <param name="relationshipId">The relationship identifier.</param>
        public async Task DeleteRelationship(string relationshipId)
        {
            var request = $"g.E('{relationshipId}').drop()";

            await gremlinService.SubmitRequest(request);
        }

        /// <summary>
        /// Updates the relationship.
        /// </summary>
        /// <param name="relationship">The relationship.</param>
        public async Task UpdateRelationship(Relationship relationship)
        {
            await DeleteRelationship(relationship.Id);

            await AddRelationship(relationship);
        }
    }
}