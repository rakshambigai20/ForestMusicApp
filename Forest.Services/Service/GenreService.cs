using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Services.IService;
using Forest.Data.Models.Domain;
using Forest.Data.IDAO;
using Forest.Data.DAO;
using Forest.Data.Models.Repository;

namespace Forest.Services.Service
{
    public class GenreService : IGenreService
    {
        IGenreDAO genreDAO;
        public GenreService()
        {
            genreDAO = new GenreDAO();
        }
        public IList<Genre> GetGenres()
        {
            using(ForestContext context = new ForestContext())
            {
                return genreDAO.GetGenres(context);
            }
        }

        public Genre GetGenre(int id)
        {
            using (ForestContext context = new ForestContext())
            {
                var genre = genreDAO.GetGenre(context, id);
                if (genre == null)
                    throw new Exception($"Genre with ID {id} not found.");
                return genre;
            }
        }

        public void AddGenre(Genre genre)
        {
            using (ForestContext context = new ForestContext())
            {
                genreDAO.AddGenre(context, genre);
                context.SaveChanges();
            }
        }

        public Genre GetGenreByMusic(Music music)
        {
            using (ForestContext context = new ForestContext())
            {
                return genreDAO.GetGenreByMusic(context, music);
            }
        }
    }
}
