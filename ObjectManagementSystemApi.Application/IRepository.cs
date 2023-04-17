using ObjectManagementSystemApi.Domain;

namespace ObjectManagementSystemApi.Application
{
	/// <summary>
	/// Enables storage of data in a repository.
	/// </summary>
	public interface IRepository
    {
        public Task AddObject(GeneralObject newObject);

        public Task AddRelationship(Relationship relationship);

        public Task DeleteObject(string objectId);

        public Task DeleteRelationship(string relationshipId);

        public Task<string> GetAllObjects();

        public Task<string> GetAllRelationships();

        public Task<string> CountAllRelationshipsByLabel();
    }
}