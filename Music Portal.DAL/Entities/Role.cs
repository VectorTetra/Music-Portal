using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Music_Portal.DAL.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ICollection<User>? Users { get; set; }
    }
}
