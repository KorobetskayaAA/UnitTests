using System.Collections.Generic;

namespace WebApp.Data.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Citation> Citations { get; set; }
    }
}
