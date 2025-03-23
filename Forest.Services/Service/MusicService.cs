using Forest.Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Data.Models.Domain;
using Forest.Data.DAO;
using Forest.Data.IDAO;
using System.Diagnostics;
using Forest.Data.Models.Repository;
using Forest.Services.IService;
using Forest.Services.Models;

namespace Forest.Services.Service
{
    public class MusicService: IMusicService
    {
        IMusicDAO musicDAO;
        IGenreDAO genreDAO;
        IArtistDAO artistDAO;
        IUserDAO userDAO;
        public MusicService()
        {
            musicDAO = new MusicDAO();
            genreDAO = new GenreDAO();
            artistDAO = new ArtistDAO();
            userDAO = new UserDAO();
        }

        //Get music by id
        public Music GetMusic(int id)
        {
            using(ForestContext context = new ForestContext())
            {
                return musicDAO.GetMusic(context, id);
            }

        }

        //Add music
        public bool AddMusic(MusicGenreArtist data, string userId)
        {
            try
            {
                #region(Prepare Music Object)
                Music music = new Music();
                music.Title = data.Title;
                music.Tracks = data.Tracks;
                music.minutes = data.minutes;
                music.ReleaseDate = data.ReleaseDate;
                music.price = data.Price;
                music.Image = data.Image;
                #endregion

                #region(Unit of Work - Do the work)
                using (ForestContext context = new ForestContext())
                {
                    musicDAO.AddMusic(context, music);
                    userDAO.AddMusicToCollection(context, userId, music);
                    Genre genre = genreDAO.GetGenre(context, data.GenreId);
                    genreDAO.AddMusicToCollection(context, genre, music);
                    Artist artist = artistDAO.GetArtist(context, data.ArtistId);
                    artistDAO.AddMusicToCollection(context, artist, music);
                    int changes = context.SaveChanges();
                    Debug.WriteLine("Number of records saved: " + changes);
                }
                #endregion
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error adding music: " + ex.Message);
                return false;
            }

        }



    }
}
