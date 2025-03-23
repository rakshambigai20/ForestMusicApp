using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Forest.Services.IService;
using Forest.Services.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using Forest.Data.Models.Domain;

namespace Forest.Controllers
{
    public class Helper : Controller
    {
        IGenreService genreService;
        IArtistService artistService;

        public Helper() 
        {
            genreService = new GenreService();
            artistService = new ArtistService();

        }
        public List<SelectListItem> GetGenreDropdown()
        {
            List<SelectListItem> genreList = new List<SelectListItem>();
            IList<Genre> genres = genreService.GetGenres();
            foreach (Genre genre in genres)
            {
                genreList.Add(
                new SelectListItem { 
                    Text = genre.Name, 
                    Value = genre.Id.ToString(),
                    Selected = (genre.Name == genres[0].Name ? true : false)});

            }
            return genreList;
        }

        public List<SelectListItem> GetArtistDropdown()
        {
            List<SelectListItem> artistList = new List<SelectListItem>();
            IList<Artist> artists = artistService.GetArtists();
            foreach (Artist artist in artists)
            {
                artistList.Add(
                new SelectListItem
                {
                    Text = artist.Name,
                    Value = artist.Id.ToString(),
                    Selected = (artist.Name == artists[0].Name ? true : false)
                });
            }
            return artistList;
        }

    }
}
