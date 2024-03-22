using CommunityToolkit.Mvvm.ComponentModel;
using MensajesServer.Models;
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
        public Mensaje? mensaje;
        public MensajeViewModel()
        {
           
        }




        
    }
}
