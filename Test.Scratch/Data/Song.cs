namespace Test.Scratch.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Song
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long AlbumId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [StringLength(10)]
        public string Length { get; set; }

        public int? TrackNumber { get; set; }

        [StringLength(10)]
        public string Genre { get; set; }

        public DateTime? DateAdded { get; set; }

        public DateTime? DateModified { get; set; }

        public virtual Album Album { get; set; }
    }
}
