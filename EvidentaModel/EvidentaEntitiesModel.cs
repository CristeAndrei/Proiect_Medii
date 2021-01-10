namespace EvidentaModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EvidentaEntitiesModel : DbContext
    {
        public EvidentaEntitiesModel()
            : base("name=EvidentaEntitiesModel")
        {
        }

        public virtual DbSet<Catalog> Catalogs { get; set; }
        public virtual DbSet<Facultate> Facultates { get; set; }
        public virtual DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Facultate>()
                .Property(e => e.numeFacultate)
                .IsUnicode(false);

            modelBuilder.Entity<Facultate>()
                .Property(e => e.nrTelFacultate)
                .IsUnicode(false);

            modelBuilder.Entity<Facultate>()
                .HasMany(e => e.Catalogs)
                .WithOptional(e => e.Facultate)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Student>()
                .Property(e => e.nume)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.prenume)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .Property(e => e.nrTel)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Catalogs)
                .WithOptional(e => e.Student)
                .WillCascadeOnDelete();
        }
    }
}
