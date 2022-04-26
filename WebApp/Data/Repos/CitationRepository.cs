using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Model;

namespace WebApp.Data.Repos
{
    public class CitationRepository : IRepository<Citation, int>
    {
        readonly AppDbContext context;

        public CitationRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Citation> GetAll()
        {
            return context.Citations
                .Include(c => c.Categories)
                .ToList();
        }

        public Citation Get(int id)
        {
            return context.Citations
                .Include(c => c.Categories)
                .FirstOrDefault(c => c.Id == id);
        }

        public bool Exists(int id)
        {
            return context.Citations.Any(c => c.Id == id);
        }

        public Citation Create(Citation citation)
        {
            context.Citations.Add(citation);
            context.SaveChanges();
            return citation;
        }
        public bool Update(Citation citation)
        {
            try
            {
                context.Citations.Update(citation);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            var categoryToDelete = context.Citations.Find(id);
            if (categoryToDelete == null)
            {
                return false;
            }
            try
            {
                context.Remove(categoryToDelete);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
