using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Data.IDAO;
using Forest.Data.Models.Domain;
using Forest.Data.Models.Repository;

namespace Forest.Data.DAO
{
    public class UserDAO:IUserDAO
    {
        public void AddMusicToCollection(ForestContext context, string userId, Music music)
        {
            User user = context.Users.Find(userId);
            user.Musics.Add(music);

        }
    }
}
