using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace CustomSerialization.DB
{
    [DataContract(IsReference = true)]
    public partial class Territory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Territory()
        {
            Employees = new HashSet<Employee>();
        }

        [DataMember]
        [StringLength(20)]
        public string TerritoryID { get; set; }

        [DataMember]
        [Required]
        [StringLength(50)]
        public string TerritoryDescription { get; set; }

        [DataMember]
        public int RegionID { get; set; }

        [DataMember]
        public virtual Region Region { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
