using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forest.Data.Models.Domain
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public string DeliveryAddress { get; set; }

        public virtual ICollection<Orderline> Orderlines { get; set; } = new List<Orderline>();
    }
}
