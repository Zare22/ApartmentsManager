namespace Public_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

        public virtual AspNetUsers AspNetUsers { get; set; }
    }
}
