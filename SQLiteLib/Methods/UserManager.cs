using System.Collections.Generic;
using SQLite;

using CheckConnection.Model;

namespace CheckConnection.Methods
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepo _repository;

        public UserManager(SQLiteConnection conn)
        {
            _repository = new UserRepo(conn);
        }

    }

}