using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatServidorTCP.Models
{
    public class MensajeDto
    {
        public string Mensaje { get; set; } = "";

        public string Origen { get; set; } = "";

        public DateTime Fecha { get; set; }
    }
}
