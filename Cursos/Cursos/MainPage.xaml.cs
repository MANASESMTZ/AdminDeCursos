using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Cursos
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private async void OnRegistrarEmpleadosClicked(object sender, EventArgs e)
        {
            // Navegar a la página de registro de empleados
            await Navigation.PushAsync(new RegistrarEmpleadosPage());
        }

        private async void OnRegistrarCursosClicked(object sender, EventArgs e)
        {
            // Navegar a la página de registro de cursos
            await Navigation.PushAsync(new RegistrarCursosPage());
        }

        private async void OnSeguimientoClicked(object sender, EventArgs e)
        {
            // Navegar a la página de seguimiento cursos
            await Navigation.PushAsync(new Seguimiento());
        }
    }
}
