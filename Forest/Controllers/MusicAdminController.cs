using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Forest.Services.IService;
using Forest.Services.Service;
using Forest.Services.Models;
using System.Diagnostics;

namespace Forest.Controllers
{
    public class MusicAdminController : Controller
    {
        private IMusicService _musicService;

        public MusicAdminController()
        {
            _musicService = new MusicService();
        }

        // GET: Create Music
        public ActionResult Create()
        {
            ViewBag.GenreList = new Helper().GetGenreDropdown();
            ViewBag.ArtistList = new Helper().GetArtistDropdown();
            return View();
        }

        // POST: Create Music
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MusicGenreArtist collection)
        {
            try
            {
                _musicService.AddMusic(collection, "1");
                return RedirectToAction("GetGenre", "Genre", new { id = collection.GenreId });
            }
            catch
            {
                return View();
            }
        }

        
    }
}
