using NoticiasAPI.Models.Entities;

namespace NoticiasAPI.Repositories
{
    public interface IRepository<T> where T : class
    {
        ItesrcneOctavoContext Context { get; set; }

        void Delete(T entity);
        T? Get(object id);
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
    }
}