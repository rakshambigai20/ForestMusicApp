using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Forest.Services.IService;
using Forest.Services.Service;
using System.Diagnostics;

namespace Forest.Controllers
{
    public class MusicController : Controller
    {
        IMusicService musicService;
        public MusicController()
        {
            musicService = new MusicService();
        }
        
        public ActionResult GetMusic(int id)
        {

            return View(musicService.GetMusic(id));
        }

    }
}
