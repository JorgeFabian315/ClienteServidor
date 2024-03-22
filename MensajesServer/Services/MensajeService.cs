using MensajesServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MensajesServer.Services
{
    public class MensajeService
    {
        HttpListener servidor = new();

        public MensajeService()
        {
            servidor.Prefixes.Add("http://*:7002/mensajitos/");
            servidor.Start();

            new Thread(RecibirPeticiones) { IsBackground = true}.Start();
        }

        public event EventHandler<Mensaje>? MensajeRecibido;
        void RecibirPeticiones()
        {
            while (true)
            {
                var context = servidor.GetContext();
                if (context != null)
                {
                    if (context.Request.QueryString["texto"] != null)// Verificando si me mandaron el mensaje por querystring
                    {
                        Mensaje mensaje = new Mensaje()
                        {
                            Texto = context.Request.QueryString["texto"] ?? "",
                            ColorLetra = context.Request.QueryString["letra"] ?? "#000",
                            ColorFondo = context.Request.QueryString["colorfondo"] ?? "#fff"
                        };

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            MensajeRecibido?.Invoke(this, mensaje);
                        });
                    }


                }

            }
        }


    }
}
