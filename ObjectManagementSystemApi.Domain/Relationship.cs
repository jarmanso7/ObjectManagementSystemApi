namespace ObjectManagementSystemApi.Domain
{
    public class Relationship
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string FromType { get; set; }

        public string ToType { get; set; }
    }
}