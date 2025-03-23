using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Data.Models.Domain;
using Forest.Data.Models.Repository;

namespace Forest.Data.IDAO
{
    public interface IUserDAO
    {
        //Add music to user
        void AddMusicToCollection(ForestContext context, string userID, Music music);
    }
}
