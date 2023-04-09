using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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
			throw new NotImplementedException();
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
					FromId = edge.inV,
					ToId = edge.outV,
				});
			}

			return new ReadOnlyCollection<Relationship>(relationships);
		}
	}
}