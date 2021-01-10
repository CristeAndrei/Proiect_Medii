namespace EvidentaModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Student")]
    public partial class Student
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Student()
        {
            Catalogs = new HashSet<Catalog>();
        }

        public int studentId { get; set; }

        [StringLength(50)]
        public string nume { get; set; }

        [StringLength(50)]
        public string prenume { get; set; }

        [StringLength(50)]
        public string nrTel { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Catalog> Catalogs { get; set; }
    }
}
