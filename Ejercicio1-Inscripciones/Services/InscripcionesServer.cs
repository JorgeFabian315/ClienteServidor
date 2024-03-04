using Ejercicio1_Inscripciones.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Ejercicio1_Inscripciones.Services
{
    public class InscripcionesServer
    {
        public InscripcionesServer()
        {
            var hilo = new Thread(new ThreadStart(Iniciar));
            hilo.IsBackground = true;
            hilo.Start();

        }

        public event EventHandler<InscripcionDto>? InscripcionRealizada;

        void Iniciar()
        {
            UdpClient server = new(5001);

            while (true)
            {

                IPEndPoint remoto = new(IPAddress.Any, 5001);

                byte[] buffer = server.Receive(ref remoto);

                InscripcionDto? dto = JsonSerializer.Deserialize<InscripcionDto>(Encoding.UTF8.GetString(buffer));

                if (dto != null)
                {

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        InscripcionRealizada?.Invoke(this, dto);
                    });


                }
            }
        }
    }
}
