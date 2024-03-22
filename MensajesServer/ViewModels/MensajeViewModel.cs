using CommunityToolkit.Mvvm.ComponentModel;
using MensajesServer.Models;
using MensajesServer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MensajesServer.ViewModels
{
    public partial class MensajeViewModel : ObservableObject
    {
        [ObservableProperty]
        public Mensaje? mensaje = new();

        MensajeService server = new();
        DiscoveryServices discoveryServices = new DiscoveryServices();
        public MensajeViewModel()
        {
            server.MensajeRecibido += Server_MensajeRecibido;
        }

        private void Server_MensajeRecibido(object? sender, Mensaje e)
        {
            if (Mensaje != null)
            {
                Mensaje = e;
            }
        }
    }
}
