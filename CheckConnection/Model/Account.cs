using System;
using System.ComponentModel.DataAnnotations;
using SQLite;

namespace CheckConnection.Model
{
    public class Account : INameEntity
    {
        [PrimaryKey, AutoIncrement, Unique]
        public int Id { get; set; }
        [NotNull]
        public string Caption { get; set; }
        [NotNull]
        public string Description { get; set; }
        [NotNull]
        public string Domain { get; set; }
        [NotNull]
        public bool LocalAccount { get; set; }
        [NotNull, Indexed]
        public string Name { get; set; }
        [NotNull]
        public string SID { get; set; }
        [NotNull]
        public string Status { get; set; }
        [NotNull]
        public string SIDType { get; set; }
    }
}
