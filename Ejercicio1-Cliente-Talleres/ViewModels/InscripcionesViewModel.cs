using Ejercicio1_Cliente_Talleres.Models.Dtos;
using Ejercicio1_Cliente_Talleres.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ejercicio1_Cliente_Talleres.ViewModels
{
    public class InscripcionesViewModel : INotifyPropertyChanged
    {

        public InscripcionDto Datos { get; set; } = new();

        InscripcionesClient ClienteUDP = new();
        public ICommand InscripcionesCommand { get; set; }

        public string IP { get; set; } = "0.0.0.0";
        public InscripcionesViewModel()
        {
            InscripcionesCommand = new RelayCommand(Inscribir);
        }

        private void Inscribir()
        {
            ClienteUDP.Servidor = IP;

            ClienteUDP.EnviarInscripcion(Datos);



        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
