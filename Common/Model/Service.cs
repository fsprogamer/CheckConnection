namespace CheckConnection.Model
{
    public class Service : Entity, INameEntity
    {
        public string Name { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
    }
}
