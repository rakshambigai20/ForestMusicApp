using Forest.OutServices.IService;
using Forest.OutServices.Models;
using Forest.Services.IService;
using Forest.Services.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Data.Models.Domain;

namespace Forest.OutServices.Service
{
    public class Service : IContract
    {
        IGenreService genreService;
        public Service()
        {
            genreService = new GenreService();
        }
        public Style[] GetRecordStyles()
        {
            Genre[] genres = genreService.GetGenres().ToList().ToArray();
            Style[] styles = new Style[genres.Length];
            if(genres.Length > 0) 
                for(int i = 0; i<styles.Length; i++)
                    styles[i] = genres[i];
            return styles;
        }
    }
}
