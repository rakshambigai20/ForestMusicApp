using Forest.Data.Models.Domain;
using Forest.Services.IService;
using Forest.Services.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Forest.Controllers
{
    public class ArtistController : Controller
    {
        IArtistService _artistService;
        public ArtistController() 
        {
            _artistService = new ArtistService();
        }

        // GET: ArtistController
        public ActionResult GetArtists()
        {
            return View(_artistService.GetArtists());
        }

        // GET: ArtistController/Details/id
        public ActionResult GetArtist(int id)
        {
            return View(_artistService.GetArtist(id));
        }

        // GET: ArtistController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ArtistController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Artist collection)
        {
            try
            {
                _artistService.AddArtist(collection);
                return RedirectToAction(nameof(GetArtists));

            }
            catch
            {
                return View();
            }
        }

        // GET: ArtistController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ArtistController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ArtistController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ArtistController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
