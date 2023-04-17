using ObjectManagementSystemApi.Domain;
using System.Collections.ObjectModel;

namespace ObjectManagementSystemApi.Application.Services
{
    /// <summary>
    /// Enables persistence of objects and relationships.
    /// </summary>
    public interface IPersistenceService
    {
        /// <summary>
        /// Adds the object.
        /// </summary>
        /// <param name="newObject">The new object.</param>
        /// <returns></returns>
        public Task AddObject(GeneralObject newObject);

        /// <summary>
        /// Gets all objects.
        /// </summary>
        /// <returns></returns>
        public Task<ReadOnlyCollection<GeneralObject>> GetAllObjects();

        /// <summary>
        /// Deletes the object.
        /// </summary>
        /// <param name="objectId">The object identifier.</param>
        /// <returns></returns>
        public Task DeleteObject(string objectId);

        /// <summary>
        /// Updates the object.
        /// </summary>
        /// <param name="generalObject">The general object.</param>
        /// <returns></returns>
        public Task UpdateObject(GeneralObject generalObject);

        /// <summary>
        /// Adds the relationship.
        /// </summary>
        /// <param name="relationship">The relationship.</param>
        /// <returns></returns>
        public Task AddRelationship(Relationship relationship);

        /// <summary>
        /// Gets all relationships.
        /// </summary>
        /// <returns></returns>
        public Task<ReadOnlyCollection<Relationship>> GetAllRelationships();

        /// <summary>
        /// Deletes the relationship.
        /// </summary>
        /// <param name="relationship">The relationship.</param>
        /// <returns></returns>
        public Task DeleteRelationship(string relationship);

        /// <summary>
        /// Updates the relationship.
        /// </summary>
        /// <param name="relationship">The relationship.</param>
        /// <returns></returns>
        public Task UpdateRelationship(Relationship relationship);
    }
}