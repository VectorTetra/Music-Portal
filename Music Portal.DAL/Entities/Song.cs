using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.DAL.Entities
{
    public class Song
    {
        public int Id { get; set; }
        public string Name { get; set; }   
        public List<string> Singers{ get; set; }
        public string Path { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}
