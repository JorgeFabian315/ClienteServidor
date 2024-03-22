using ServidorPosti.Models;
using ServidorPosti.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServidorPosti.ViewModels
{
    public class NotasViewModel : INotifyPropertyChanged
    {

        NotasServer servidor = new();
        public ObservableCollection<Nota> Notas { get; set; } = new();
        public string IP
        {
            get
            {
                return string.Join(",",Dns.GetHostAddresses(Dns.GetHostName())
                    .Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    .Select(x => x.ToString()));
            }
        }
        public NotasViewModel()
        {
            servidor.NotaRecibida += Servidor_NotaRecibida;
            servidor.Iniciar();

        }
        Random r = new();
        private void Servidor_NotaRecibida(object? sender, Nota e)
        {
            e.Angulo = r.Next(-5, 6);
            Notas.Add(e);
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
