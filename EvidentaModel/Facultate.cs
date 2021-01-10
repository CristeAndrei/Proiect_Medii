namespace EvidentaModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Facultate")]
    public partial class Facultate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Facultate()
        {
            Catalogs = new HashSet<Catalog>();
        }

        public int facultateId { get; set; }

        [StringLength(50)]
        public string numeFacultate { get; set; }

        [StringLength(50)]
        public string nrTelFacultate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog> Catalogs { get; set; }
    }
}
