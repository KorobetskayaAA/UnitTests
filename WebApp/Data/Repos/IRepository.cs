using System.Collections.Generic;

namespace WebApp.Data.Repos
{
    public interface IRepository<T, TId>
    {
        T Get(TId id);
        IEnumerable<T> GetAll();

        bool Exists(TId id);
        T Create(T entity);
        bool Update(T entity);
        bool Delete(TId id);
    }
}