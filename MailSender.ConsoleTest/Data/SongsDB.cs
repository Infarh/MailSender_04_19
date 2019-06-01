using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.ConsoleTest.Data
{
    class SongsDB : DbContext
    {   
        public SongsDB() : this("name=SongsDB") { }
        public SongsDB(string ConnectionString) : base(ConnectionString) { }

        public DbSet<Track> Tracks { get; set; }
    }

    [Table("SongsSet")]
    class Track
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, Column("Artist")]
        public string ArtistName { get; set; }

        public int Length { get; set; }

        public TimeSpan TimeLength => TimeSpan.FromSeconds(Length);
    }
}
