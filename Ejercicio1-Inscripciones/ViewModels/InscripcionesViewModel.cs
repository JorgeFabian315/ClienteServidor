using Ejercicio1_Inscripciones.Models;
using Ejercicio1_Inscripciones.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ejercicio1_Inscripciones.ViewModels
{
    public class InscripcionesViewModel : INotifyPropertyChanged
    {
        public string IP { get; set; } = "0.0.0.0";

        public ObservableCollection<Taller> Talleres { get; set; } = new();

        List<Taller> talleres = new(); // Datos Persistentes

        InscripcionesServer servidor = new();


        public InscripcionesViewModel()
        {
            var ips = Dns.GetHostAddresses(Dns.GetHostName());
            IP = ips.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                .Select(x => x.ToString()).FirstOrDefault() ?? "0.0.0.0";

            Cargar();

            Actualizar();
            servidor.InscripcionRealizada += Servidor_InscripcionRealizada;
        }

        private void Servidor_InscripcionRealizada(object? sender, Models.Dtos.InscripcionDto e)
        {
            if (e.Taller == "Ninguno")
            {
                foreach (var x in talleres)
                {
                    var alumno = x.Alumnos.FirstOrDefault(x => x.Nombre == e.Nombre);
                    
                    if(alumno != null)
                    {
                        x.Alumnos.Remove(alumno);
                    }
                }
            }
            else
            {
                var taller = talleres.FirstOrDefault(x => x.Nombre == e.Taller);

                if(taller != null)
                {
                    taller.Alumnos.Add(new Alumno { Nombre = e.Nombre });

                }
            }

            Guardar();
            Actualizar();
        }

        private void Guardar()
        {
            var archiivo = File.Create("talleres.json");
            JsonSerializer.Serialize(archiivo, talleres);
            archiivo.Close();
        }

        public void Actualizar()
        {
            Talleres.Clear();
           
            foreach (var t in talleres)
            {
                Talleres.Add(t);
            }
        }

        public void Cargar()
        {

            if (File.Exists("talleres.json"))
            {
                var archivo = File.OpenRead("talleres.json");

                talleres = JsonSerializer.Deserialize<List<Taller>>(archivo) ?? new()
                {
                    new Taller(){Nombre = "Canto", Alumnos = new()},
                    new Taller() { Nombre = "Canto", Alumnos = new() }
            };

                archivo.Close();
            }

            else
            {
                talleres = new()
                {
                    new Taller(){Nombre = "Canto", Alumnos = new()},
                    new Taller() { Nombre = "Baile", Alumnos = new() }
                };
            }
        }


        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
