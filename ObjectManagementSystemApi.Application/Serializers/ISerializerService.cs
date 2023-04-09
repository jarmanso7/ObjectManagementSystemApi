using ObjectManagementSystemApi.Domain;
using System.Collections.ObjectModel;

namespace ObjectManagementSystemApi.Application.Serializers
{
	/// <summary>
	/// Enables transformation of data between the Infrastructure and the Application layers
	/// </summary>
	public interface ISerializerService
    {
		public ReadOnlyCollection<GeneralObject> GremlinVerticesToGeneralObjects(string serializedVertices);

		public ReadOnlyCollection<Relationship> GremlinEdgesToRelationships(string serializedEdges);
	}
}
