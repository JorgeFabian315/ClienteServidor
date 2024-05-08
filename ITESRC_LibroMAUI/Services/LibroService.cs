using ITESRC_LibroMAUI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITESRC_LibroMAUI.Models.DTOs;
using System.Net.Http.Json;

namespace ITESRC_LibroMAUI.Services
{
    public class LibroService
    {
        HttpClient cliente;
        Repository.LibroRepository librosRepository = new();

        public LibroService()
        {
            cliente = new()
            {
                BaseAddress = new Uri("https://libros.itesrc.net/")
            };


        }

        public async Task Agregar(LibroDTO dto)
        {
            //    var json = JsonSerializer.Serialize(dto);
            //    var response = await cliente.PostAsync("api/libros", new StringContent(json, Encoding.UTF8,
            //        "application/json"));


            var response = await cliente.PostAsJsonAsync("api/libros", dto);

            if (response.IsSuccessStatusCode)
            {
                await GetLibros(); //Baja los libros de la api a la BD local
            }
            else
            {
                var errores = await response.Content.ReadAsStringAsync();
                throw new Exception(errores);
            }

        }

        public event Action DatosActualizados;
        public async Task GetLibros()
        {
            try
            {
                var fecha = Preferences.Get("UltimaFechaActualizacion", DateTime.MinValue);

                bool aviso = false;

                var response = await cliente.GetFromJsonAsync<List<LibroDTO>>($"api/libros/{fecha:yyyy-MM-dd}/{fecha:HH}/{fecha:mm}");

                if (response != null)
                {

                    foreach (LibroDTO libro in response)
                    {
                        var entidad = librosRepository.Get(libro.Id ?? 0);

                        if (entidad == null && libro.Eliminado == false) //SI no estaba en BD Local, lo agrego
                        {
                            entidad = new()
                            {
                                Id = libro.Id ?? 0,
                                Autor = libro.Autor,
                                Portada = libro.Portada,
                                Titulo = libro.Titulo
                            };

                            librosRepository.Insert(entidad);
                            aviso = true;
                        }
                        else
                        {
                            if (entidad != null)
                            {
                                if (libro.Eliminado ?? false)
                                {
                                    librosRepository.Delete(entidad);
                                    aviso = true;
                                }
                                else
                                {
                                    if (libro.Titulo != entidad.Titulo || libro.Portada != entidad.Portada || libro.Autor != entidad.Autor)
                                    {
                                        librosRepository.Update(entidad);
                                        aviso = true;
                                    }
                                }
                            }
                        }


                    }

                    if (aviso)
                    {

                        _ = MainThread.InvokeOnMainThreadAsync(() =>
                        {
                            DatosActualizados?.Invoke();
                        });
                    }

                    Preferences.Set("UltimaFechaActualizacion", response.Max(x => x.Fecha));
                }
            }
            catch
            {

            }
        }


        public async Task Eliminar(int id)
        {
            var response = await cliente.DeleteAsync($"api/libros/{id}");

            if(response.IsSuccessStatusCode)
            {
                await GetLibros();
            }

        }

        public async Task Editar(LibroDTO dto)
        {
            var response = await cliente.PutAsJsonAsync($"api/libros/",dto);

            if (response.IsSuccessStatusCode)
            {
                await GetLibros();
            }

        }


    }
}
