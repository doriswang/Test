namespace Test.Scratch.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Album
    {
        public Album()
        {
            Songs = new HashSet<Song>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ArtistName { get; set; }

        public virtual ICollection<Song> Songs { get; set; }
    }
}
