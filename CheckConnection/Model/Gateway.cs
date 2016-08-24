using SQLite;

namespace CheckConnection.Model
{
    class Gateway
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        [NotNull]
        public int Connection_Id { get; set; }
        [NotNull]
        public string IP_Address { get; set; }
    }
}
