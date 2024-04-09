using CommunityToolkit.Mvvm.ComponentModel;
using MensajesClienteHTTP.Model;
using MensajesClienteHTTP.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajesClienteHTTP.ViewModels
{
    public partial class MensajeViewModel : ObservableObject
    {
        //Sevicios para que mi app reciba servidores y envie mensajes
        MensajesServices mensajeService = new();
        DiscoveryService discoveryService = new();

        public ObservableCollection<ServerModel> Servidores { get; set; } = new();

        public MensajeViewModel()
        {
            discoveryService.ServidorRecibido += DiscoveryService_ServidorRecibido;
        }

        private void DiscoveryService_ServidorRecibido(object? sender, ServerModel e)
        {
           Servidores.Add(e);
        }
    }
}
