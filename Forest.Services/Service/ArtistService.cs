using Forest.Data.IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Data.Models.Domain;
using Forest.Data.Models.Repository;
using Forest.Services.IService;
using Forest.Data.DAO;
using Forest.Data.IDAO;

namespace Forest.Services.Service
{
    public class ArtistService: IArtistService
    {
        IArtistDAO artistDAO;
        public ArtistService() 
        {
            artistDAO = new ArtistDAO();
        }

        //Get all artists
        public IList<Artist> GetArtists()
        {
            using(ForestContext context = new ForestContext())
            {
                return artistDAO.GetArtists(context);
            }
        }

        //Get artist by id
        public Artist GetArtist(int id)
        {
            using (ForestContext context = new ForestContext())
            {
                return artistDAO.GetArtist(context, id);
            }
        }

        public void AddArtist(Artist artist)
        {
            using (ForestContext context = new ForestContext())
            {
                artistDAO.AddArtist(context, artist);
                context.SaveChanges();
            }
        }
    }
}
