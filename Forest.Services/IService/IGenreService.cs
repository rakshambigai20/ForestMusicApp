using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Data.Models.Domain;

namespace Forest.Services.IService
{
    public interface IGenreService
    {
        IList<Genre> GetGenres();
        Genre GetGenre(int id);

        void AddGenre(Genre genre);
    }
}
