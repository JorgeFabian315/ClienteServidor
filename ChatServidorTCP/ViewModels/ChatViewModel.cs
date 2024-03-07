using ChatServidorTCP.Models;
using ChatServidorTCP.Service;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace ChatServidorTCP.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        public ChatServer Server { get; set; } = new();
        public ObservableCollection<string> Usuarios { get; set; } = new();
        public ICommand IniciarServerCommand { get; set; }
        public ICommand DetenerServerCommand { get; set; }
        public string IP { get; set; } = "0.0.0.0";
        public ObservableCollection<MensajeDto> Mensajes { get; set; } = new();
        public int NumMensaje { get;  set; }

        public ChatViewModel()
        {
            var direcciones = Dns.GetHostAddresses(Dns.GetHostName());
            if (direcciones != null)
            {
                IP = string.Join(",", direcciones.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(x => x.ToString()));
            }
            IniciarServerCommand = new RelayCommand(IniciarServer);
            DetenerServerCommand = new RelayCommand(DetenerCommand);
            Server.MensajeRecibido += Server_MensajeRecibido;
        }

        private void Server_MensajeRecibido(object? sender, MensajeDto e)
        {
           
                if (e.Mensaje == "**HELLO")
                {
                    e.Mensaje = $"{e.Origen} se ha conectado";
                    Usuarios.Add(e.Origen);
                }
                else if (e.Mensaje == "**BYE")
                {
                    e.Mensaje = $"{e.Origen} se ha desconectado";
                    Usuarios.Remove(e.Origen);
                }
                Mensajes.Add(e);
            NumMensaje = Mensajes.Count - 1;
            PropertyChanged?.Invoke(this, new(nameof(NumMensaje)));

        }

        public void DetenerCommand()
        {
            Mensajes.Clear();
            Usuarios.Clear();
            Server.Detener();
        }
        public void IniciarServer()
        {
            Server.Iniciar();
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
