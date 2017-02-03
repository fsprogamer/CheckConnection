namespace CheckConnection.Model
{
    public class Service : INameEntity, IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
    }
}
