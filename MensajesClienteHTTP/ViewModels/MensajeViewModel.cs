using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MensajesClienteHTTP.Model;
using MensajesClienteHTTP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MensajesClienteHTTP.ViewModels
{
    public partial class MensajeViewModel : ObservableObject
    {
        //Sevicios para que mi app reciba servidores y envie mensajes
        MensajesServices mensajeService = new();
        DiscoveryService discoveryService = new();

        public ServerModel Seleccionado { get; set; } = null!;
        public MensajeDto Mensaje { get; set; } = new();
        public ObservableCollection<ServerModel> Servidores { get; set; } = new();
        public List<SolidColorBrush> Colores { get; set; } = new();

        public MensajeViewModel()
        {
            foreach (var propiedad in typeof(Brushes).GetProperties())
            {
                Colores.Add((SolidColorBrush)(propiedad.GetValue(null) ?? new SolidColorBrush()));
            }

            //Colores = new()
            discoveryService.ServidorRecibido += DiscoveryService_ServidorRecibido;
        }

        private void DiscoveryService_ServidorRecibido(object? sender, ServerModel e)
        {

            //Agregar si no esta

            var server = Servidores.FirstOrDefault(x => x.NombreServer == e.NombreServer);

            if (server == null)
            {
                Servidores.Add(e);
            }
            else
            {
                server.KeepAlive = e.KeepAlive;
            }

            foreach (var s in Servidores.ToList())
            {
                if ((DateTime.Now - s.KeepAlive).TotalSeconds > 30)
                {
                    Servidores.Remove(s);
                }
            }

        }

        [RelayCommand]
        public void Enviar()
        {
            mensajeService.EnviarMensaje(Seleccionado, Mensaje);
        }


    }
}
