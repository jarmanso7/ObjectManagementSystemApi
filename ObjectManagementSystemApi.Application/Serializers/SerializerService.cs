using Newtonsoft.Json;
using ObjectManagementSystemApi.Domain;
using System.Collections.ObjectModel;

namespace ObjectManagementSystemApi.Application.Serializers
{
	/// <summary>
	/// Enables transformation of data between the Infrastructure and the Application layers
	/// </summary>
	public class SerializerService : ISerializerService
	{
		public ReadOnlyCollection<GeneralObject> GremlinVerticesToGeneralObjects(string serializedVertices)
		{
			var deserializedVertices = JsonConvert.DeserializeObject<dynamic>(serializedVertices);

			var objects = new List<GeneralObject>();

			foreach(var vertex in deserializedVertices)
			{
				objects.Add(new GeneralObject()
				{
					Id = vertex.id,
					Type = vertex.label,
					Name = vertex.properties.name[0].value,
					Description = vertex.properties.description[0].value
				});
			}

			return new ReadOnlyCollection<GeneralObject>(objects);
		}

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