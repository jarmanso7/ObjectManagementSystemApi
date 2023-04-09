using ObjectManagementSystemApi.Domain;
using System.Collections.ObjectModel;

namespace ObjectManagementSystemApi.Application.Services
{
    /// <summary>
    ///   Enables persistence of objects and relationships
    /// </summary>
    public interface IPersistenceService
    {
        public Task AddObject(GeneralObject newObject);

        public Task<ReadOnlyCollection<GeneralObject>> GetAllObjects();

        public Task AddRelationship(Relationship relationship);

        public Task<List<string>> GetAllRelationshipDistinctNames();

        public Task<ReadOnlyCollection<Relationship>> GetAllRelationships();
    }
}