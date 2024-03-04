using Ejercicio1_Cliente_Talleres.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ejercicio1_Cliente_Talleres.Services
{
    public class InscripcionesClient
    {
        private UdpClient cliente = new();

        public string Servidor { get; set; } = "0.0.0.0";
        public void EnviarInscripcion(InscripcionDto dto)
        {
            var ipe = new IPEndPoint(IPAddress.Parse(Servidor),5001);
            
            var json = JsonSerializer.Serialize(dto);

            byte[] buffer = Encoding.UTF8.GetBytes(json);

            cliente.Send(buffer, buffer.Length, ipe);
        }
    }
}
