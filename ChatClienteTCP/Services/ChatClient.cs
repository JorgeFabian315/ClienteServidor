using ChatServidorTCP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace ChatClienteTCP.Services
{
    public class ChatClient
    {
        TcpClient cliente = null!;
        public string Equipo { get; set; } = null!;
        public void Conectar(IPAddress ip)
        {

            try
            {
                IPEndPoint ipe = new(ip, 9000);

                cliente = new TcpClient();
                cliente.Connect(ipe);

                Equipo = Dns.GetHostName();
                //Environment.UserName

                var msg = new MensajeDto
                {
                    Fecha = DateTime.Now,
                    Mensaje = "**HELLO",
                    Origen = Equipo
                };

                EnviarMensaje(msg);

                RecibirMensaje();

            }
            catch (Exception ex)
            {
                //Mostrar el error
            }
        }
        public void Desconectar()
        {
            var msg = new MensajeDto
            {
                Fecha = DateTime.Now,
                Mensaje = "**BYE",
                Origen = Equipo
            };
            EnviarMensaje(msg);

            cliente.Close();

        }

        public event EventHandler<MensajeDto>? MensajeRecibido;

        private void RecibirMensaje()
        {
            new Thread(() =>
            {
                try
                {
                    while (cliente.Connected)
                    {
                        if (cliente.Available > 0)
                        {
                            var ns = cliente.GetStream();
                            byte[] buffer = new byte[cliente.Available];
                            ns.Read(buffer, 0, buffer.Length);

                            var msg = JsonSerializer.Deserialize<MensajeDto>(Encoding.UTF8.GetString(buffer));

                            Application.Current.Dispatcher.Invoke((() =>
                            {
                                if (msg != null)
                                    MensajeRecibido?.Invoke(this, msg);
                            }));
                        }

                    }
                }
                catch (Exception ex)
                {

                }
            })
            {
                IsBackground = true
            }.Start();
        }
        public void EnviarMensaje(MensajeDto msg)
        {
            if (!string.IsNullOrWhiteSpace(msg.Mensaje))
            {
                var json = JsonSerializer.Serialize(msg);
                byte[] buffer = Encoding.UTF8.GetBytes(json);

                var ns = cliente.GetStream();
                ns.Write(buffer, 0, buffer.Length);
                ns.Flush();
            }
        }
    }
}
