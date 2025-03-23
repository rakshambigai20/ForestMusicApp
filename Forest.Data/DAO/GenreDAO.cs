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
    public class GenreDAO: IGenreDAO
    {
        public GenreDAO()
        {

        }

        //Get all genres
        public IList<Genre> GetGenres(ForestContext context)
        {
            return context.Genres.ToList();
        }
        
        //Get genre by id
        public Genre GetGenre(ForestContext context, int id)
        {
            return context.Genres
                .Include(genre => genre.Musics)
                .FirstOrDefault(g => g.Id == id);
        }

        //Add music to genre
        public void AddMusicToCollection(ForestContext context, Genre genre, Music music)
        {
            genre.Musics.Add(music);
        }

        public void AddGenre(ForestContext context, Genre genre)
        {
            context.Genres.Add(genre);
        }
    }
}
