
namespace Common
{
    public abstract class DBConnection: ClassWithLogger<DBConnection>
    {
        private string _conn_string;

        public string conn_string { get; set; }
  
    }

}
