namespace EvidentaModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Catalog")]
    public partial class Catalog
    {
        public int catalogId { get; set; }

        public int? studentId { get; set; }

        public int? facultateId { get; set; }

        public virtual Student Student { get; set; }

        public virtual Facultate Facultate { get; set; }
    }
}
