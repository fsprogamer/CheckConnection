using SQLite;

namespace CheckConnection.Model
{
    public class DNS : Entity
    {
        //[PrimaryKey, AutoIncrement, Unique]
        //public int Id { get; set; }
        [NotNull, Indexed]
        public int Connection_Id { get; set; }
        [NotNull]
        public string DNSServer { get; set; }
        [NotNull]
        public int Order_Id { get; set; }
       
    }
}
