using SQLite;

namespace CheckConnection.Model
{
    public class Gateway : Entity
    {
        [NotNull,Indexed]
        public int Connection_Id { get; set; }
        [NotNull]
        public string IPGateway { get; set; }
    }
}
