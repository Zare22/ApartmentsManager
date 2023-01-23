using DataLayer.Enums;
using System;
using System.Collections.Generic;

namespace DataLayer.Models
{

    [Serializable]
    public class Apartment
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string Name { get; set; }
        public int MaxAdults { get; set; }
        public int MaxChildren { get; set; }
        public int TotalRooms { get; set; }
        public decimal Price { get; set; }
        public string CityName { get; set; }
        public string Address { get; set; }
        public int BeachDistance { get; set; }
        public int ImageCount { get; set; }
        public Status Status { get; set; }
        public IList<Tag> Tags { get; set; }
        public IList<Image> Images { get; set; }
        public IList<Review> Reviews { get; set; }

        public override bool Equals(object obj)
            => obj is Apartment other && Guid.Equals(other.Guid);

        public override int GetHashCode()
            => Guid.GetHashCode();
    }
}
