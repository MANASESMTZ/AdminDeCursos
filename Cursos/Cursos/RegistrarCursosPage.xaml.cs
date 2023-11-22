using AppRegistrosC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Cursos
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarCursosPage : ContentPage
    {
        public RegistrarCursosPage()
        {
            InitializeComponent();
            txtTipo.Items.Add("Interno");
            txtTipo.Items.Add("Externo");
            llenarDatos();
        }

        private async void Guardar_Button_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
                Curssos curs = new Curssos
                {
                    Nombre = txtNombre.Text,
                    Tipo = txtTipo.SelectedItem.ToString(),
                    Descripcion = txtDescripcion.Text,
                    Horas = int.Parse(txtHoras.Text),
                };

                await App.SQLiteDB.SaveCursoAsync(curs);

                txtNombre.Text = "";
                txtTipo.SelectedItem = "";
                txtDescripcion.Text = "";
                txtHoras.Text = "";

                await DisplayAlert("AVISO", "Se Guardo de Manera Exitosa", "Ok");
                llenarDatos();

                var EmpleadosList = await App.SQLiteDB.GetCursosAsync();
                if (EmpleadosList != null)
                {
                    lsEmpleados.ItemsSource = EmpleadosList;
                }

            }
            else
            {
                await DisplayAlert("AVISO", "Ingresar los Datos", "Ok");
            }
        }
        private async void Button_Actualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdCur.Text))
            {
                Curssos curso = new Curssos()
                {
                    IDCur = int.Parse(txtIdCur.Text),
                    Nombre = txtNombre.Text,
                    Tipo = txtTipo.SelectedItem.ToString(),
                    Descripcion = txtDescripcion.Text,
                    Horas = int.Parse(txtHoras.Text),

                };

                await App.SQLiteDB.SaveCursoAsync(curso);
                txtIdCur.Text = "";
                txtNombre.Text = "";
                txtTipo.SelectedItem = "";
                txtDescripcion.Text = "";
                txtHoras.Text = "";

                txtIdCur.IsVisible = false;
                btnGuardar.IsVisible = true;
                btnActualizar.IsVisible = false;

                await DisplayAlert("Aviso", "Se Actualizo Registro de Manera Exitosa", "OK");
                llenarDatos();
            }
        }
        public async void Borrar_Button_Clicked(object sender, EventArgs e)
        {
            var curso = await App.SQLiteDB.GetCursoByIdAsync(int.Parse(txtIdCur.Text));
            if (curso != null)
            {
                await App.SQLiteDB.DeleteCursosAsync(curso);
                await DisplayAlert("AVISO", "Se Elimino el Registro de Manera Exitosa", "OK");
                txtIdCur.Text = "";
                txtNombre.Text = "";
                txtTipo.SelectedItem = "";
                txtDescripcion.Text = "";
                txtHoras.Text = "";

                txtIdCur.IsVisible = false;
                btnGuardar.IsVisible = true;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                llenarDatos();
            }
        }
        public async void llenarDatos()
        {
            var EmpleadoList = await App.SQLiteDB.GetCursosAsync();
            if (EmpleadoList != null)
            {
                lsEmpleados.ItemsSource = EmpleadoList;
            }
        }

        private async void lstEmpleados_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Curssos)e.SelectedItem;

            btnGuardar.IsVisible = false;
            txtIdCur.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.IDCur.ToString()))
            {
                var emplea = await App.SQLiteDB.GetCursoByIdAsync(obj.IDCur);

                if (emplea != null)
                {
                    txtIdCur.Text = emplea.IDCur.ToString();
                    txtNombre.Text = emplea.Nombre;
                    txtTipo.SelectedItem = emplea.Tipo;
                    txtDescripcion.Text = emplea.Descripcion;
                    txtHoras.Text = emplea.Horas.ToString();
                }
            }
        }

        public bool validarDatos()
        {
            bool respuesta;

            if (string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtTipo.SelectedItem.ToString()))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtHoras.Text))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }
    }
}