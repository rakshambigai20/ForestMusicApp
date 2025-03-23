using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forest.Services.Models
{
    public class MusicGenreArtist
    {
        public string Title { get; set; }   
        public int Tracks { get; set; }
        public int minutes { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public int GenreId { get; set; }
        public int ArtistId { get; set; }
    }
}
