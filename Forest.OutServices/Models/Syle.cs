using Forest.Data.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forest.OutServices.Models
{
    public class Style
    {
        Style(int id, string name, Record[] records)
        {
            Id = id; Name = name; Records = records;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Record[] Records { get; set; }

        public static implicit operator Style(Genre genre)
        {
            Music[] musics = genre.Musics.ToArray();
            Record[] records = new Record[musics.Length];
            if (musics.Length > 0)
                for (int i = 0; i < musics.Length; i++)
                    records[i] = musics[i];
            return new Style(genre.Id, genre.Name, records);
        }
    }
}
