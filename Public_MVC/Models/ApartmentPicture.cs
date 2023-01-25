namespace Public_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ApartmentPicture")]
    public partial class ApartmentPicture
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedAt { get; set; }

        public int? ApartmentId { get; set; }

        [StringLength(250)]
        public string Path { get; set; }

        public string Base64Content { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public bool? IsRepresentative { get; set; }

        public virtual Apartment Apartment { get; set; }
    }
}
