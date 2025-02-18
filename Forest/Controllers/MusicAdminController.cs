using Forest.Data.Models.Domain;
using Forest.Data.Models.Repository;
using Forest.Services.Models;
using Forest.Services.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Forest.Controllers
{
    public class MusicAdminController : Controller
    {
        Helper helper;
        MusicService musicService;
        GenreService genreService;
        ArtistService artistService;
        ForestContext context;
        private readonly BlobServiceClient blobService;
        public MusicAdminController() 
        {
            helper = new Helper();
            musicService = new MusicService();
            genreService = new GenreService();
            artistService = new ArtistService();
            context = new ForestContext();
            blobService = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=stforestsouthukdev001;AccountKey=jDABENqdM6AtsdyH8bRq1ir+N7OGz6ryOwvhG3aZhVqxbh5rDbvHmg1MI3y+rYB4oL5XomwWZUyY+ASt4sVYSA==;EndpointSuffix=core.windows.net");
        }
        
        
        

        // GET: MusicAdminController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Music music = musicService.GetMusic(id);
                return View(music);
            }
            catch
            {
                return View();
            }
        }
        // GET: MusicAdminController/Create
        [Authorize (Roles ="Admin, Staff")]
        public ActionResult Create()
        {
            ViewBag.genreList = helper.GetGenreDropdown();
            ViewBag.artistList = helper.GetArtistDropdown();
            return View();
        }


        // GET: MusicAdminController/Edit/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Edit(int id)
        {
            Music music = musicService.GetMusic(id);
            return View(music);
         
        }

        // POST: MusicAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Music collection)
        {
            try
            {
  
                musicService.UpdateMusic(collection,id );
                Music music = musicService.GetMusic(id);
                Genre genre = genreService.GetGenre(music);
                return RedirectToAction("GetGenre", "Genre", new { id = genre.Id });
         
              
            }
            catch
            {
                return View();
            }
        }

        // GET: MusicAdminController/Delete/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult Delete(int id)
        {
            Music music = musicService.GetMusic(id);
            return View(music);
        }

        // POST: MusicAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                Music music = musicService.GetMusic(id);
                Genre genre = genreService.GetGenre(music);
                musicService.DeleteMusic(id);
                return RedirectToAction("GetGenre","Genre", new { id = genre.Id });
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MusicGenreArtist collection)
        {
            try
            {
                musicService.AddMusic(collection, "mo");
               return RedirectToAction("GetGenre", "Genre", new { id = collection.GenreId });
          }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult AddMusic()
        {
            ViewBag.genreList = helper.GetSelectList<Genre>(
                genreService.GetGenres(), x => x.Id, x => x.Name);
            ViewBag.artistList = helper.GetSelectList<Artist>(
                artistService.GetArtists(), x => x.Id, x => x.Name);
            return View();
        }
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult AddGenre()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> LoadFiles(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Json(new { success = false, message = "No file selected" });
            }

            try
            {
                var containerClient = blobService.GetBlobContainerClient("web");

                await containerClient.CreateIfNotExistsAsync(PublicAccessType.BlobContainer);

                string newBlobName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

                BlobClient blobClient = containerClient.GetBlobClient(newBlobName);

                using (var stream = file.OpenReadStream())
                {
                    await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
                }

                string blobUrl = blobClient.Uri.ToString();

                return Json(new { success = true, message = "File uploaded successfully", fileName = newBlobName, fileUrl = blobUrl });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Upload failed: " + ex.Message });
            }
        }



        // POST: GenreController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddGenre(Genre genre)
        {
            try
            {
                genreService.AddGenre(genre, "31c78147 - 7630 - 4321 - bda5 - 6b535b227ff1");
                return RedirectToAction("GetGenres", "Genre");
            }
            catch
            {
                return View();
            }
        }
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult DeleteGenre(int id)
        {
            Genre genre = genreService.GetGenre(id);
            return View(genre);
        }

        // POST: MusicAdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGenre(int id, IFormCollection collection)
        {
            try
            {
                Genre genre = genreService.GetGenre(id);
                genreService.DeleteGenre(id);
                return RedirectToAction("GetGenres", "Genre");
            }
            catch
            {
                return View();
            }
        }
        // GET: MusicAdminController/Edit/5
        [Authorize(Roles = "Admin, Staff")]
        public ActionResult EditGenre(int id)
        {
            Genre genre = genreService.GetGenre(id);
            return View(genre);

        }

        // POST: MusicAdminController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGenre(Genre collection , int id)
        {
            try
            {

                genreService.UpdateGenre(collection, id);
                return RedirectToAction("GetGenres", "Genre" );


            }
            catch
            {
                return View();
            }
        }



    }
}
