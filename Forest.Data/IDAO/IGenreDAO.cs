using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Data.Models.Domain;
using Forest.Data.Models.Repository;

namespace Forest.Data.IDAO
{
    public interface IGenreDAO
    {
        //Get all genres
        IList<Genre> GetGenres(ForestContext context);

        //Get genre by id
        Genre GetGenre(ForestContext context, int id);

        //Add music to genre
        void AddMusicToCollection(ForestContext context, Genre genre, Music music);

        //Add genre
        void AddGenre(ForestContext context, Genre genre);

        //Get genre by music
        Genre GetGenreByMusic(ForestContext context, Music music);
    }
}
