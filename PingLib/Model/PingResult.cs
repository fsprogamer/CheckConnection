using System;
using SQLite;

namespace PingLib.Model
{
   public class PingResult
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        [NotNull, Indexed]
        public DateTime Date { get; set; }
        [NotNull]
        public int Connection_Id { get; set; }
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public string Ip_Address { get; set; }
        [NotNull]
        public string StatusCode { get; set; }
        [NotNull]
        public string ErrMessage { get; set; }
        [NotNull]
        public string ResponseTime { get; set; }
        public PingResult(string strHostAddress)
        {
            Date = DateTime.Now;
            Name = strHostAddress;
        }
    }
}