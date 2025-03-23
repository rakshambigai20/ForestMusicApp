using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forest.Data.Models.Domain
{
    public class Music
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Tracks { get; set; }
        public int minutes { get; set; }

        public DateTime ReleaseDate { get; set; }
        public double price { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Orderline> Orderlines { get; set; } = new List<Orderline>();
    }
}
