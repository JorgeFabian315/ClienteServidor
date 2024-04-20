using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MensajesServer.Services
{
    public class DiscoveryServices
    {
        UdpClient server = new()
        {
            EnableBroadcast = true
        };
        IPEndPoint destino = new(IPAddress.Broadcast, 7000);

        byte[] buffer;
        public DiscoveryServices()
        {
            var usuario = Environment.UserName;
            buffer = Encoding.UTF8.GetBytes(usuario);

            Saludar(); // CUANDO LO CRAMOS SALUDAMOS EN LA RED
            new Thread(RecibirSaludo) { IsBackground = true }.Start(); //PARA ESPERAR A QUE NOS SALUDEN
            new Thread(StillAlive) { IsBackground = true }.Start(); // PARA INFORMAR QUE SEGUIMOS VIVOS

        }

        public void Saludar()
        {
            server.Send(buffer, buffer.Length, destino);
        }

        private void StillAlive()
        {
            while (true)
            {
                Thread.Sleep(TimeSpan.FromSeconds(30));
                Saludar();
            }
        }

        private void RecibirSaludo() // voy a responder el saludo cuando me saludan
        {
            UdpClient udp2 = new UdpClient(7001);
            while (true)
            {
                IPEndPoint remoto = new(IPAddress.Any, 0);
                byte[] buffer = udp2.Receive(ref remoto);

                server.Send(buffer, buffer.Length, remoto);

            }
        }
    }
}
