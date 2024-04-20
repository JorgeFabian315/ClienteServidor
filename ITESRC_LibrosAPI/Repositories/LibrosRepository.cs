using ITESRC_LibrosAPI.Models.Entities;

namespace ITESRC_LibrosAPI.Repositories
{
    public class LibrosRepository
    {
        private readonly ItesrcneLibrosContext context;

        public LibrosRepository(ItesrcneLibrosContext context)
        {
            this.context = context;
        }


        public IEnumerable<Libros> GetAll()
        {
            return context.Libros.OrderBy(c => c.Titulo);
        }

        public Libros? Get(int id)
        {
            return context.Libros.Find(id);
        }

        public void Insert(Libros libros)
        {
            context.Libros.Add(libros);
            context.SaveChanges();
        }

        public void Update(Libros libros)
        {
            context.Libros.Update(libros); 
            context.SaveChanges();
        }

        public void Delete(Libros libros)
        {
            context.Remove(libros);
            context.SaveChanges();
        }

    }
}
