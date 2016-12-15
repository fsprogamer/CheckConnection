
namespace Common
{
    public abstract class DBConnection: ClassWithLog
    {
        private string _conn_string;

        public string conn_string { get; set; }
  
    }

}
