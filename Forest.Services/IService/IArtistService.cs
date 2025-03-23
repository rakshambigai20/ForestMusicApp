using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Data.Models.Domain;
using Forest.Data.Models.Repository;

namespace Forest.Services.IService
{
    public interface IArtistService
    {
        //Get all artists
        IList<Artist> GetArtists();

        //Get artist by id
        Artist GetArtist(int id);

        //Add artist
        void AddArtist(Artist artist);
    }
}
