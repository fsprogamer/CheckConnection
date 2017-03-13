using SQLite;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    class UserRepo : GenericRepo<SQLiteConnection, User>, IUserRepo
    {
        public UserRepo(SQLiteConnection conn) : base(conn)
        {
            log.Info("UserRepo costructor");
        }

    }
    
}
