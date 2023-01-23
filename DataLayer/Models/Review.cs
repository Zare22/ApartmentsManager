using System;

namespace DataLayer.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int ApartmentId { get; set; }
        public string UserName { get; set; }
        public string Details { get; set; }
        public int Stars { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
