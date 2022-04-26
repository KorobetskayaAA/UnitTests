using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Data.Model
{
    public class Citation
    {
        public int Id { get; set; }
        public string Quote { get; set; }
        public string Author { get; set; }
        public string? Hero { get; set; }
        public string? Title { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
