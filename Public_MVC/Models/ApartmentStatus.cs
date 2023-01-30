using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Public_MVC.Models
{
    public partial class ApartmentStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ApartmentStatus()
        {
            Apartments = new HashSet<Apartment>();
        }

        public int Id { get; set; }

        public Guid Guid { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Apartment> Apartments { get; set; }
    }
}
