using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Forest.Services.IService;
using Forest.Services.Service;
using Forest.Data.Models.Domain;

namespace Forest.Controllers
{
    public class GenreController : Controller
    {
        IGenreService genreService;
        public GenreController()
        {
            genreService = new GenreService();
        }
        public ActionResult GetGenres()
        {
            IList<Genre> genres = genreService.GetGenres();
            return View(genres);
        }
        public ActionResult GetGenre(int id)
        {
            Genre genre = genreService.GetGenre(id);
            return View(genre);
        }

        [HttpGet]
        public ActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGenre(Genre genre)
        {
            genreService.AddGenre(genre);
            return RedirectToAction("GetGenres");
        }
    }
}
