using SQLite;

namespace CheckConnection.Model
{
    public class Entity: IEntity
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
    }
}
