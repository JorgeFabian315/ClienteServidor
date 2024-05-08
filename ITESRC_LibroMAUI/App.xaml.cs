using ITESRC_LibroMAUI.Services;

namespace ITESRC_LibroMAUI
{
    public partial class App : Application
    {
        public static LibroService LibrosService { get; set; } = new();


        public App()
        {
            InitializeComponent();

            Thread thread = new Thread(Sincronizador) { IsBackground = true };
            thread.Start();

            MainPage = new AppShell();
        }


        async void Sincronizador()
        {
            while (true)
            {
                await LibrosService.GetLibros(); // _= Descartar la tarea 
                Thread.Sleep(TimeSpan.FromMinutes(1));
            }
        }
    }
}
