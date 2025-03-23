using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Forest.Services.IService;
using Forest.Services.Service;
using System.Diagnostics;
using Forest.Data.Models.Domain;

namespace Forest.Controllers
{
    public class MusicController : Controller
    {
        IMusicService musicService;
        IGenreService genreService;
        public MusicController()
        {
            musicService = new MusicService();
            genreService = new GenreService();
        }
        
        public ActionResult GetMusic(int id)
        {
            Music music = musicService.GetMusic(id);
            ViewBag.GenreId = genreService.GetGenreByMusic(music).Id;
            return View(music);
        }

    }
}
