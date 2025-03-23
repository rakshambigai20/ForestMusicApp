using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Data.Models.Domain;
using Forest.Data.Models.Repository;

namespace Forest.Data.IDAO
{
    public interface IArtistDAO
    {
        //Get all artists
        IList<Artist> GetArtists(ForestContext context);

        //Get artist by id
        Artist GetArtist(ForestContext context, int id);

        //Add music to artist
        void AddMusicToCollection(ForestContext context, Artist artist, Music music);

        //Add artist
        void AddArtist(ForestContext context, Artist artist);
    }
}
