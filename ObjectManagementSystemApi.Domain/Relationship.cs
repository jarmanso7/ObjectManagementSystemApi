namespace ObjectManagementSystemApi.Domain
{
    /// <summary>
    /// Represents a relationship between two general objects existing in the Object Management System
    /// </summary>
    public class Relationship
	{
        public string Id { get; set; }
        public string Type { get; set; }
        public string FromId { get; set; }
        public string ToId { get; set; }
    }
}