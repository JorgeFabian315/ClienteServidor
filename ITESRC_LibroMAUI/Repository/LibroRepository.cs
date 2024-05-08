using ITESRC_LibroMAUI.Models.Entities;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITESRC_LibroMAUI.Repository
{
    public class LibroRepository
    {

        SQLiteConnection context;
        public LibroRepository()
        {
            string ruta = FileSystem.AppDataDirectory + "/libros.db3";
            context = new(ruta);
            context.CreateTable<Libro>();
        }

        public void Insert(Libro libro)
        {
            context.Insert(libro);
        }

        public void Update(Libro libro)
        {
            context.Update(libro);
        }
        public void InsertOrReplace(Libro libro)
        {
            context.InsertOrReplace(libro);
        }
        public void Delete(Libro libro)
        {
            context.Delete(libro);
        }

        public IEnumerable<Libro> GetAll()
        {
            return context.Table<Libro>().OrderBy(x => x.Titulo, StringComparer.OrdinalIgnoreCase);
        }
        public Libro Get(int id)
        {
            return context.Find<Libro>(id);
        }

    
    }
}
