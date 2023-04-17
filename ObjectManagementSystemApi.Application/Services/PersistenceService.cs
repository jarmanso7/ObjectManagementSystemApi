using ObjectManagementSystemApi.Application.Serializers;
using ObjectManagementSystemApi.Domain;
using System.Collections.ObjectModel;

namespace ObjectManagementSystemApi.Application.Services
{
    /// <summary>
    /// Enables persistence of objects and relationships.
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

        /// <summary>
        /// Adds the object.
        /// </summary>
        /// <param name="newObject">The new object.</param>
        public async Task AddObject(GeneralObject newObject)
        {
            newObject.Id = string.IsNullOrEmpty(newObject.Id) ? Guid.NewGuid().ToString() : newObject.Id;

            await repository.AddObject(newObject);
        }

        /// <summary>
        /// Gets all objects.
        /// </summary>
        /// <returns></returns>
        public async Task<ReadOnlyCollection<GeneralObject>> GetAllObjects()
        {
            var gremlinVertices = await repository.GetAllObjects();

            return serializerService.GremlinVerticesToGeneralObjects(gremlinVertices);
        }

        /// <summary>
        /// Deletes the object.
        /// </summary>
        /// <param name="generalObjectId">The general object identifier.</param>
        public async Task DeleteObject(string generalObjectId)
        {
            await repository.DeleteObject(generalObjectId);
        }

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="updateObject">The update object.</param>
        public async Task UpdateObject(GeneralObject updateObject)
        {
            await repository.UpdateObject(updateObject);
        }

        /// <summary>
        /// Adds the relationship.
        /// </summary>
        /// <param name="relationship">The relationship.</param>
        public async Task AddRelationship(Relationship relationship)
        {
			relationship.Id = string.IsNullOrEmpty(relationship.Id) ? Guid.NewGuid().ToString() : relationship.Id;

			await repository.AddRelationship(relationship);
        }

        /// <summary>
        /// Gets all relationships.
        /// </summary>
        /// <returns></returns>
        public async Task<ReadOnlyCollection<Relationship>> GetAllRelationships()
        {
            var gremlinEdges = await repository.GetAllRelationships();

            return serializerService.GremlinEdgesToRelationships(gremlinEdges);
        }

        /// <summary>
        /// Deletes the relationship.
        /// </summary>
        /// <param name="relationshipId">The relationship identifier.</param>
        public async Task DeleteRelationship(string relationshipId)
        {
            await repository.DeleteRelationship(relationshipId);
        }

        /// <summary>
        /// Updates the relationship.
        /// </summary>
        /// <param name="relationship">The relationship.</param>
        public async Task UpdateRelationship(Relationship relationship)
        {
            await repository.UpdateRelationship(relationship);
        }
    }
}