using System;

namespace CheckConnection.Model
    {
        public class User : IEntity
        {
            public int Id { get; set; }            
            public DateTime DateFrom { get; set; }
            public DateTime DateTo { get; set; }
        }
    }

