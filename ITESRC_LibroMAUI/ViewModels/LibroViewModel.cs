using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ITESRC_LibroMAUI.Models.Entities;
using ITESRC_LibroMAUI.Models.DTOs;
using ITESRC_LibroMAUI.Models.Validators;
using ITESRC_LibroMAUI.Repository;
using ITESRC_LibroMAUI.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITESRC_LibroMAUI.ViewModels
{
    public partial class LibroViewModel : ObservableObject
    {
        LibroRepository librosRepository = new();
        public LibroViewModel()
        {
            ActualizarLibros();
            App.LibrosService.DatosActualizados += LibrosService_DatosActualizados;
        }

        private void LibrosService_DatosActualizados()
        {
            ActualizarLibros();
        }

        public ObservableCollection<Libro> Libros { get; set; } = new();

        LibroService service = new();

        [ObservableProperty]
        private LibroDTO libro = new();

        [ObservableProperty]
        private string error = "";

        [ObservableProperty]
        private Libro libroSeleccionado = new();

        [RelayCommand]
        public void Nuevo()
        {
            Libro = new();
            Shell.Current.GoToAsync("//Agregar");
        }

        [RelayCommand]
        public void Cancelar()
        {
            Libro = null;
            Error = "";
            Shell.Current.GoToAsync("//Lista");
        }

        LibroValidator validador = new();

        [RelayCommand]
        public async Task Agregar()
        {
            try
            {
                if (Libro != null)
                {
                    var resultado = validador.Validate(Libro);
                    if (resultado.IsValid)
                    {
                        await service.Agregar(Libro);
                        ActualizarLibros();
                        Cancelar();
                    }
                    else
                    {
                        Error = string.Join("\n", resultado.Errors.Select(x => x.ErrorMessage));
                    }
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }



        [RelayCommand]
        public async Task Eliminar()
        {

            if (LibroSeleccionado != null)
            {
                var result = await Shell.Current.DisplayAlert("confirmar", $"Estas seguro de eliminar el libro llamado: {LibroSeleccionado.Titulo}?", "Si", "No");

                if (result)
                {
                   await service.Eliminar(LibroSeleccionado.Id);
                    ActualizarLibros();
                }
            
            }




        }

        public void Editar()
        {
            if(LibroSeleccionado != null)
            {
                Error = "";
                Shell.Current.GoToAsync("//Editar");

            }
        }

        public void Guardar()
        {

        }


        void ActualizarLibros()
        {
            Libros.Clear();
            foreach (var libro in librosRepository.GetAll())
            {
                Libros.Add(libro);
            }
        }
    }
}
