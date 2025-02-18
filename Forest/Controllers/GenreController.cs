using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Forest.Services.IService;
using Forest.Services.Service;
using Forest.Data.Models.Domain;
using Forest.Data.Models.Repository;
using Newtonsoft.Json;

namespace Forest.Controllers
{
    public class GenreController : ForestController
    {
        //IGenreService genreService;
        IList<Genre> genres;
        ForestContext context;
        public GenreController()
        {
            //genreService = new GenreService();
            string str = httpContextAccessor.HttpContext.Session.GetString("genres");
            genres = JsonConvert.DeserializeObject<IList<Genre>>(str);
        }
        public ActionResult GetGenres()
        {
            //return View(genreService.GetGenres());
            string userId = HttpContext.Session.GetString("user_id");
            return View(genres);
        }

        public ActionResult GetGenre(int id)
        {
            Genre genre = null;
            //genre = new Genre();
            while (genre == null)
            {
                for (int i = 0; i < genres.Count; i++)
                {
                    if (genres[i].Id == id)
                        genre = genres[i];
                }
            }
            return View(genre);
        }

        
    }
}
