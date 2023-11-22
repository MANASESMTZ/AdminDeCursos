using AppRegistrosC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace Cursos
{
    
    public partial class RegistrarEmpleadosPage : ContentPage
    {
        public RegistrarEmpleadosPage()
        {
            InitializeComponent();
            txtTipo.Items.Add("Planta");
            txtTipo.Items.Add("Temporal");
            llenarDatos();
        }

        private async void Guardar_Button_Clicked(object sender, EventArgs e)
        {
            if (validarDatos())
            {
               byte[] imagenBytes = ConvertirImagenABytes(txtFoto.Source as FileImageSource);

                Empleados emple = new Empleados
                {
                    Imagen = imagenBytes,
                    Nombre = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    CURP = txtCurp.Text,
                    Edad = int.Parse(txtEdad.Text),
                    Telefono = int.Parse(txtTelefono.Text),
                    Tipo = txtTipo.SelectedItem.ToString(),
                };

                await App.SQLiteDB.SaveEmpleadoAsync(emple);

                txtNombre.Text = "";
                txtDireccion.Text = "";
                txtCurp.Text = "";
                txtEdad.Text = "";
                txtTelefono.Text = "";
                txtTipo.SelectedItem = "";
                await DisplayAlert("AVISO", "Se Guardo de Manera Exitosa", "Ok");
                llenarDatos();

                var EmpleadosList = await App.SQLiteDB.GetEmpleadosAsync();
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
            if (!string.IsNullOrEmpty(txtIdEmp.Text))
            {
                Empleados empleado = new Empleados()
                {
                    IDEmp = int.Parse(txtIdEmp.Text),
                    Nombre = txtNombre.Text,
                    Direccion = txtDireccion.Text,
                    CURP = txtCurp.Text,
                    Edad = int.Parse(txtEdad.Text),
                    Telefono = int.Parse(txtTelefono.Text),
                    Tipo = txtTipo.SelectedItem.ToString(),

                };

                await App.SQLiteDB.SaveEmpleadoAsync(empleado);
                txtIdEmp.Text = "";
                txtNombre.Text = "";
                txtDireccion.Text = "";
                txtCurp.Text = "";
                txtEdad.Text = "";
                txtTelefono.Text = "";
                txtTipo.SelectedItem = "";

                txtIdEmp.IsVisible = false;
                btnGuardar.IsVisible = true;
                btnActualizar.IsVisible = false;

                await DisplayAlert("Aviso", "Se Actualizo Registro de Manera Exitosa", "OK");
                llenarDatos();
            }
        }
        public async void Borrar_Button_Clicked(object sender, EventArgs e)
        {
            var empleado = await App.SQLiteDB.GetEmpleadoByIdAsync(int.Parse(txtIdEmp.Text));
            if (empleado != null)
            {
                await App.SQLiteDB.DeleteEmpleadosAsync(empleado);
                await DisplayAlert("AVISO", "Se Elimino el Registro de Manera Exitosa", "OK");
                txtIdEmp.Text = "";
                txtNombre.Text = "";
                txtDireccion.Text = "";
                txtCurp.Text = "";
                txtEdad.Text = "";
                txtTelefono.Text = "";
                txtTipo.SelectedItem = "";

                txtIdEmp.IsVisible = false;
                btnGuardar.IsVisible = true;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                llenarDatos();
            }
        }
        public async void llenarDatos()
        {
            var EmpleadoList = await App.SQLiteDB.GetEmpleadosAsync();
            if (EmpleadoList != null)
            {
                lsEmpleados.ItemsSource = EmpleadoList;
            }
        }

        private async void lstEmpleados_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (Empleados)e.SelectedItem;

            btnGuardar.IsVisible = false;
            txtIdEmp.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.IDEmp.ToString()))
            {
                var emplea = await App.SQLiteDB.GetEmpleadoByIdAsync(obj.IDEmp);

                if (emplea != null)
                {
                    txtIdEmp.Text = emplea.IDEmp.ToString();
                    txtNombre.Text = emplea.Nombre;
                    txtDireccion.Text = emplea.Direccion;
                    txtCurp.Text = emplea.CURP;
                    txtEdad.Text = emplea.Edad.ToString();
                    txtTelefono.Text = emplea.Telefono.ToString();
                    txtTipo.SelectedItem = emplea.Tipo;
                }
            }
        }

        public bool validarDatos()
        {
            bool respuesta;

           // if(string.IsNullOrEmpty(txtFoto.imagenBytes))
            //if (string.IsNullOrEmpty(Foto.stream.ToArray().ToString()))
            //{
            //    respuesta = false;
           // }
             if(string.IsNullOrEmpty(txtNombre.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtDireccion.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtCurp.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtEdad.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                respuesta = false;
            }
            else if (string.IsNullOrEmpty(txtTipo.SelectedItem.ToString()))
            {
                respuesta = false;
            }
            else
            {
                respuesta = true;
            }
            return respuesta;
        }

        private async void TomarFoto_Clicked(object sender, EventArgs e)
        {
            var foto = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions());

            if (foto != null)
            {
                txtFoto.Source = ImageSource.FromStream(() =>
                {
                    return foto.GetStream();
                });
            }
        }


        private byte[] ConvertirImagenABytes(ImageSource imagen)
        {
            if (imagen is FileImageSource fileImageSource)
            {
                string rutaImagen = fileImageSource.File;
                if (!string.IsNullOrEmpty(rutaImagen))
                {
                    byte[] imagenBytes = File.ReadAllBytes(rutaImagen);
                    return imagenBytes;
                }
            }
            return null; // Devuelve null si la imagen no es válida o si la conversión no es posible
        }



    }
}