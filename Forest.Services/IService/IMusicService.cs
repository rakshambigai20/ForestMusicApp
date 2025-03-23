using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Data.Models.Domain;
using Forest.Services.Models;

namespace Forest.Services.IService
{
    public interface IMusicService
    {
        //Get music by id
        Music GetMusic(int id);

        //Add music
        bool AddMusic(MusicGenreArtist musicGenreArtist, string userId);
    }
}
