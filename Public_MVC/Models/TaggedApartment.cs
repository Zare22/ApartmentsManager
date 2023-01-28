using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Public_MVC.Models
{
    [Table("TaggedApartment")]
    public partial class TaggedApartment
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public int ApartmentId { get; set; }

        public int TagId { get; set; }

        public virtual Apartment Apartment { get; set; }

        public virtual Tag Tag { get; set; }
    }
}
