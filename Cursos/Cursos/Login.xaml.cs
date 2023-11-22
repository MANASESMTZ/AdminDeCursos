using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cursos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void Button_Login(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmailLog.Text))
            {

                await DisplayAlert("Aviso ", "Debe escribor un Email en el campo", "OK");
                return;
            }
            if (string.IsNullOrEmpty(txtContraLog.Text))
            {

                await DisplayAlert("Aviso ", "Debe escribir la contraseña", "OK");
                return;
            }

            var resultado = await App.SQLiteDB.GetUsersValidate(txtEmailLog.Text, txtContraLog.Text);

            if (resultado.Count > 0)
            {
                txtEmailLog.Text = "";
                txtContraLog.Text = "";
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("AVISO", "El email o contraseña esta incorrecto", "OK");
                txtEmailLog.Text = "";
                txtContraLog.Text = "";
            }

        }

        private async void Button_Registrarse(object sender, EventArgs e)
        {
            txtEmailLog.Text = "";
            txtContraLog.Text = "";


            await Navigation.PushAsync(new Registro());
        }

    }
}