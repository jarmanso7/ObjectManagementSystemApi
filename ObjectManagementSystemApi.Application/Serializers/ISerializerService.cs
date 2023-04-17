using ObjectManagementSystemApi.Domain;
using System.Collections.ObjectModel;

namespace ObjectManagementSystemApi.Application.Serializers
{
	/// <summary>
	/// Enables transformation of data between the Infrastructure and the Application layers
	/// </summary>
	public interface ISerializerService
    {
        /// <summary>
        /// Converts gremlin vertices to general objects.
        /// </summary>
        /// <param name="serializedVertices">The serialized vertices.</param>
        /// <returns></returns>
        public ReadOnlyCollection<GeneralObject> GremlinVerticesToGeneralObjects(string serializedVertices);

        /// <summary>
        /// Converts Gremlins Edges to Relationships.
        /// </summary>
        /// <param name="serializedEdges">The serialized edges.</param>
        /// <returns></returns>
        public ReadOnlyCollection<Relationship> GremlinEdgesToRelationships(string serializedEdges);
	}
}
