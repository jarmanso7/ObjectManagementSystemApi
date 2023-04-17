using Newtonsoft.Json;
using ObjectManagementSystemApi.Domain;
using System.Collections.ObjectModel;

namespace ObjectManagementSystemApi.Application.Serializers
{
	/// <summary>
	/// Enables transformation of data between the Infrastructure and the Application layers.
	/// </summary>
	public class SerializerService : ISerializerService
	{
        /// <summary>
        /// Converts gremlin vertices to general objects.
        /// </summary>
        /// <param name="serializedVertices">The serialized vertices.</param>
        /// <returns></returns>
        public ReadOnlyCollection<GeneralObject> GremlinVerticesToGeneralObjects(string serializedVertices)
		{
			var deserializedVertices = JsonConvert.DeserializeObject<dynamic>(serializedVertices);

			var objects = new List<GeneralObject>();

			foreach(var vertex in deserializedVertices)
			{
				objects.Add(new GeneralObject()
				{
					Id = vertex.id,
					Type = vertex.properties.type[0].value,
					Name = vertex.properties.name[0].value,
					Description = vertex.properties.description[0].value
				});
			}

			return new ReadOnlyCollection<GeneralObject>(objects);
		}

        /// <summary>
        /// Converts Gremlins Edges to Relationships.
        /// </summary>
        /// <param name="serializedEdges">The serialized edges.</param>
        /// <returns></returns>
        public ReadOnlyCollection<Relationship> GremlinEdgesToRelationships(string serializedEdges)
		{
			var deserializedEdges = JsonConvert.DeserializeObject<dynamic>(serializedEdges);

			var relationships = new List<Relationship>();	

			foreach(var edge in deserializedEdges)
			{
				relationships.Add(new Relationship
				{
					Id = edge.id,
					Type = edge.label,
					FromId = edge.outV,
					ToId = edge.inV,
				});
			}

			return new ReadOnlyCollection<Relationship>(relationships);
		}
	}
}