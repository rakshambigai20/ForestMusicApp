using Forest.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forest.OutServices.Models
{
    public class Record
    {
        Record (int id, string name, double cost)
        {
            Id = id; Name = name; Cost = cost;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public double Cost { get; set; }
        public static implicit operator Record (Music music) 
        {
            return new Record(music.Id, music.Title, music.Price);  
        }
    }
}
