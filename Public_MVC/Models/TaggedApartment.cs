namespace Public_MVC.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
