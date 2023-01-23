using System;

namespace DataLayer.Models
{
    public class City
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}
