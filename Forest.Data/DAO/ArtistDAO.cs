using Forest.Data.IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Data.Models.Domain;
using Forest.Data.Models.Repository;
using Microsoft.EntityFrameworkCore;

namespace Forest.Data.DAO
{
    public class ArtistDAO : IArtistDAO
    {
        public IList<Artist> GetArtists(ForestContext context)
        {
            return context.Artists.ToList();
        }

        public Artist GetArtist(ForestContext context, int id)
        {
            context.Artists
                .Include(artist => artist.Musics)
                .ToList();
            return context.Artists.Find(id);
        }
        public void AddMusicToCollection(ForestContext context, Artist artist, Music music)
        {
            artist.Musics.Add(music);
        }

        public void AddArtist(ForestContext context, Artist artist)
        {
            context.Artists.Add(artist);
        }
    }
}
