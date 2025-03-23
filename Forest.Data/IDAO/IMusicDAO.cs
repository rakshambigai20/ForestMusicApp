using Forest.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Forest.Data.Models.Repository;
using Forest.Data.Models.Domain;

namespace Forest.Data.IDAO
{
    public interface IMusicDAO
    {
        Music GetMusic(ForestContext context, int id);
        void AddMusic(ForestContext context, Music music);
    }
}
