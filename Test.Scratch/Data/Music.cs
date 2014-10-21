namespace Test.Scratch.Data
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Music : DbContext
    {
        public Music()
            : base("name=Music")
        {
        }

        public virtual DbSet<Album> Albums { get; set; }
        public virtual DbSet<Song> Songs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>()
                .HasMany(e => e.Songs)
                .WithRequired(e => e.Album)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Song>()
                .Property(e => e.Length)
                .IsFixedLength();

            modelBuilder.Entity<Song>()
                .Property(e => e.Genre)
                .IsFixedLength();
        }
    }
}
