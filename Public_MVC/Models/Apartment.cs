namespace Public_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Apartment")]
    public partial class Apartment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Apartment()
        {
            ApartmentPicture = new HashSet<ApartmentPicture>();
            ApartmentReservation = new HashSet<ApartmentReservation>();
            ApartmentReview = new HashSet<ApartmentReview>();
            TaggedApartment = new HashSet<TaggedApartment>();
        }

        public int Id { get; set; }

        public Guid Guid { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedAt { get; set; }

        public int StatusId { get; set; }

        public int? CityId { get; set; }

        [StringLength(250)]
        public string Address { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int? MaxAdults { get; set; }

        public int? MaxChildren { get; set; }

        public int? TotalRooms { get; set; }

        public int? BeachDistance { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ApartmentStatus ApartmentStatus { get; set; }

        public virtual City City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApartmentPicture> ApartmentPicture { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApartmentReservation> ApartmentReservation { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApartmentReview> ApartmentReview { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaggedApartment> TaggedApartment { get; set; }
    }
}
