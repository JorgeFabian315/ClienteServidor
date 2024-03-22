using ChatClienteTCP.Services;
using ChatServidorTCP.Models;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace ChatClienteTCP.ViewModels
{
    public class ChatViewModel : INotifyPropertyChanged
    {
        //community tool

        public ObservableCollection<MensajeDto> Mensajes { get; set; } = new();
        public string Mensaje { get; set; } = "";

        public ICommand EnviarCommand { get; set; }

        public string Ip { get; set; } = "";
        public ICommand ConectarCommand { get; set; }
        public bool Conectado { get; set; }

        public int NumMensaje { get; set; }

        ChatClient cliente = new();
        public ChatViewModel()
        {
            cliente.MensajeRecibido += Cliente_MensajeRecibido;
            EnviarCommand = new RelayCommand(Enviar);
            ConectarCommand = new RelayCommand(Conectar);

        }

        private void Conectar()
        {
            IPAddress.TryParse(Ip, out IPAddress? ipAddress);

            if (ipAddress != null)
            {
                cliente.Conectar(ipAddress);
                Conectado = true;
                PropertyChanged?.Invoke(this, new(nameof(Conectado)));
            }


        }

        private void Enviar()
        {
            if (!string.IsNullOrWhiteSpace(Mensaje))
            {
                var ms = new MensajeDto()
                {
                    Fecha = DateTime.Now,
                    Origen = cliente.Equipo,
                    Mensaje = Mensaje
                };
                cliente.EnviarMensaje(ms);

                Mensajes.Add(ms);
            }
        }

        private void Cliente_MensajeRecibido(object? sender, MensajeDto e)
        {
            Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                if (e.Mensaje == "**HELLO")
                {
                    e.Mensaje = $"{e.Origen} se ha conectado";
                }
                if (e.Mensaje == "**BYE")
                {
                    e.Mensaje = $"{e.Origen} se ha desconectado";
                }

                Mensajes.Add(e);
                NumMensaje = Mensajes.Count - 1;
                PropertyChanged?.Invoke(this, new(nameof(NumMensaje)));
            });

        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
