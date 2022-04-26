using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Data.Model;

namespace WebApp.Data.Repos
{
    public class CategoryRepository : IRepository<Category, int>
    {
        readonly AppDbContext context;

        public CategoryRepository(AppDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return context.Categories.ToList();
        }

        public Category Get(int id)
        {
            return context.Categories
                .Include(cat => cat.Citations)
                .FirstOrDefault(cat => cat.Id == id);
        }
        public bool Exists(int id)
        {
            return context.Categories.Any(c => c.Id == id);
        }

        public Category Create(Category category)
        {
            context.Categories.Add(category);
            context.SaveChanges();
            return category;
        }
        public bool Update(Category category)
        {
            try
            {
                context.Categories.Update(category);
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
            var categoryToDelete = context.Categories.Find(id);
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
