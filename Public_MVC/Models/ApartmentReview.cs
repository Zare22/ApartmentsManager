using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Public_MVC.Models
{

    [Table("ApartmentReview")]
    public partial class ApartmentReview
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }

        public int ApartmentId { get; set; }

        public int UserId { get; set; }

        [StringLength(1000)]
        public string Details { get; set; }

        public int? Stars { get; set; }

        public virtual Apartment Apartment { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }
    }
}
