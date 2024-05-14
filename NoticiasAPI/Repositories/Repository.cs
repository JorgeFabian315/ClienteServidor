using NoticiasAPI.Models.Entities;

namespace NoticiasAPI.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public ItesrcneOctavoContext Context { get; set; }

        public Repository(ItesrcneOctavoContext context)
        {
            Context = context;
        }

        public virtual T? Get(object id)
        {
            return Context.Find<T>(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public virtual void Insert(T entity)
        {
            Context.Add(entity);
            Context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            Context.Update(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            Context.Remove(entity);
            Context.SaveChanges();
        }

    }
}
