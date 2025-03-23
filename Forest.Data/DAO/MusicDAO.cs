using Forest.Data.IDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Data.Models.Domain;
using Forest.Data.Models.Repository;
using System.Diagnostics;

namespace Forest.Data.DAO
{
    public class MusicDAO : IMusicDAO
    {
        public MusicDAO()
        {
        }

        //Get music by id
        public Music GetMusic(ForestContext context, int id)
        {
            
            return context.Musics.Find(id);
        }

        //Add music
        public void AddMusic(ForestContext context, Music music)
        {
            context.Musics.Add(music);
        }

    }
}
