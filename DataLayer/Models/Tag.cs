using System;

namespace DataLayer.Models
{
    [Serializable]
    public class Tag
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool IsInUse { get; set; }

        public override bool Equals(object obj)
            => obj is Tag other && this.Id.Equals(other.Id);

        public override int GetHashCode()
            => this.Id.GetHashCode();

        public override string ToString()
            => Name;
    }
}
