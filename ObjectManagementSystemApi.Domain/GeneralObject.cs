namespace ObjectManagementSystemApi.Domain
{
    /// <summary>
    /// Represents a General Object existing in the Object Management System.
    /// </summary>
    public class GeneralObject
	{
		public string Id { get; set; }
		public string Type { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}