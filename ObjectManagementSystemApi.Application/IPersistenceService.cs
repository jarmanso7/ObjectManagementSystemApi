using ObjectManagementSystemApi.Domain;

namespace ObjectManagementSystemApi.Application
{
    /// <summary>
    ///   Enables persistence of objects and relationships
    /// </summary>
    public interface IPersistenceService
    {
        public Task AddObject(GeneralObject newObject);

        public Task AddRelationship(string fromId, string toId, string relationshipName);

        public Task<List<string>> GetAllRelationshipDistinctNames();
    }
}