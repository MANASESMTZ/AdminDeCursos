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
	public partial class Seguimiento : ContentPage
	{
		public Seguimiento ()
		{
            InitializeComponent();
            txtEstatus.Items.Add("Programado");
            txtEstatus.Items.Add("En Progreso");
            txtEstatus.Items.Add("Completo");

            llenarDatos2();
            LlenarPicker();

        }
        private async void LlenarPicker()
        {
            var EmpleadosList = await App.SQLiteDB.GetEmpleadosAsync();
            if (EmpleadosList != null)
            {
                txtNombreEmp.ItemsSource = EmpleadosList;
            }

            var CursosList = await App.SQLiteDB.GetCursosAsync();
            if (CursosList != null)
            {
                txtNombreCurso.ItemsSource = CursosList;
            }
        }

        private async void btnGuardarSeg_Clicked(object sender, EventArgs e)
        {
            if (validarDatos2())
            {
                if (txtNombreEmp.SelectedItem == null || txtNombreCurso.SelectedItem == null)
                {
                    await DisplayAlert("AVISO", "Por favor, seleccione un empleado y un curso.", "Ok");
                    return;
                }

                SeguimientoEmpleados segui = new SeguimientoEmpleados
                            {
                                NombreSeg = txtNombreEmp.SelectedItem.ToString(),
                                Curso = txtNombreCurso.SelectedItem.ToString(),
                                Lugar = txtLugar.Text,
                                Fecha = txtFecha.Date.ToString("yyyy-MM-dd"),
                                Hora = txtHora.Time.ToString(@"hh\:mm\:ss"),
                                Estatus = txtEstatus.SelectedItem.ToString(),
                                Calificacion = int.Parse(txtCalificacion.Text),
                            };

                            await App.SQLiteDB.SaveSeguimientoEmpleadoAsync(segui);

                            txtNombreEmp.SelectedItem = "";
                            txtNombreCurso.SelectedItem = "";
                            txtLugar.Text = "";
                            txtFecha.Date = DateTime.Now;
                            txtHora.Time = new TimeSpan();
                            txtEstatus.SelectedItem = "";
                            txtCalificacion.Text = "";
                            await DisplayAlert("AVISO", "Se Guardo de Manera Exitosa" + txtNombreEmp.SelectedItem, "Ok");
                            llenarDatos2();

                            var EmpleadosList2 = await App.SQLiteDB.GetSeguimientoEmpleadosAsync();
                            if (EmpleadosList2 != null)
                            {
                                lsEmpleados2.ItemsSource = EmpleadosList2;
                            }
                        
            }
            else
            {
                await DisplayAlert("AVISO", "Ingresar los Datos", "Ok");
            }
        }




        public async void llenarDatos2()
        {
            var EmpleadoList2 = await App.SQLiteDB.GetSeguimientoEmpleadosAsync();
            if (EmpleadoList2 != null)
            {
                lsEmpleados2.ItemsSource = EmpleadoList2;
            }
        }

        private async void btnActualizar_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtIdEmp.Text))
            {
                SeguimientoEmpleados segui = new SeguimientoEmpleados()
                {
                    IDSeg = int.Parse(txtIdEmp.Text),
                    NombreSeg = txtNombreEmp.SelectedItem.ToString(),
                    Curso = txtNombreCurso.SelectedItem.ToString(),
                    Lugar = txtLugar.Text,
                    Fecha = txtFecha.Date.ToString("yyyy-MM-dd"),
                    Hora = txtHora.Time.ToString(@"hh\:mm\:ss"),
                    Estatus = txtEstatus.SelectedItem.ToString(),
                    Calificacion = int.Parse(txtCalificacion.Text),

                };

                await App.SQLiteDB.SaveSeguimientoEmpleadoAsync(segui);

                txtIdEmp.Text = "";
                txtNombreEmp.SelectedItem = "";
                txtNombreCurso.SelectedItem = "";
                txtLugar.Text = "";
                txtFecha.Date = DateTime.Now;
                txtHora.Time = new TimeSpan();
                txtEstatus.SelectedItem = "";
                txtCalificacion.Text = "";

                txtIdEmp.IsVisible = false;
                btnGuardarSeg.IsVisible = true;
                btnActualizar.IsVisible = false;

                await DisplayAlert("Aviso", "Se Actualizo Registro de Manera Exitosa", "OK");
                llenarDatos2();
            }
        }

        private async void btnEliminar_Clicked(object sender, EventArgs e)
        {
            var seguimientoo = await App.SQLiteDB.GetSeguimientoEmpleadoByIdAsync(int.Parse(txtIdEmp.Text));
            if (seguimientoo != null)
            {
                await App.SQLiteDB.DeleteSeguimientoEmpleadosAsync(seguimientoo);
                await DisplayAlert("AVISO", "Se Elimino el Registro de Manera Exitosa", "OK");
                txtIdEmp.Text = "";
                txtNombreEmp.SelectedItem = "";
                txtNombreCurso.SelectedItem = "";
                txtLugar.Text = "";
                txtFecha.Date = DateTime.Now;
                txtHora.Time = new TimeSpan();
                txtEstatus.SelectedItem = "";
                txtCalificacion.Text = "";

                txtIdEmp.IsVisible = false;
                btnGuardarSeg.IsVisible = true;
                btnActualizar.IsVisible = false;
                btnEliminar.IsVisible = false;
                llenarDatos2();
            }

        }

        private async void lsEmpleados2_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var obj = (SeguimientoEmpleados)e.SelectedItem;

            btnGuardarSeg.IsVisible = false;
            txtIdEmp.IsVisible = true;
            btnActualizar.IsVisible = true;
            btnEliminar.IsVisible = true;

            if (!string.IsNullOrEmpty(obj.IDSeg.ToString()))
            {
                var seguimi = await App.SQLiteDB.GetSeguimientoEmpleadoByIdAsync(obj.IDSeg);

                if (seguimi != null)
                {
                    txtIdEmp.Text = seguimi.IDSeg.ToString();
                    txtNombreEmp.SelectedItem = seguimi.NombreSeg;
                    txtNombreCurso.SelectedItem = seguimi.Curso;
                    txtLugar.Text = seguimi.Lugar;
                    if (DateTime.TryParse(seguimi.Fecha, out DateTime fecha))
                        txtFecha.Date = fecha;

                    if (TimeSpan.TryParse(seguimi.Hora, out TimeSpan hora))
                        txtHora.Time = hora;
                    txtEstatus.SelectedItem = seguimi.Estatus;
                    txtCalificacion.Text = seguimi.Calificacion.ToString();
                }

            }
        }


        public bool validarDatos2()
        {
            bool respuesta;

            if (string.IsNullOrEmpty(txtNombreEmp.SelectedItem.ToString()))
            {
                respuesta = false;
            }

            if (string.IsNullOrEmpty(txtNombreCurso.SelectedItem.ToString()))
            {
                respuesta = false;
            }

            if (string.IsNullOrEmpty(txtLugar.Text))
            {
                respuesta = false;
            }

            if (txtFecha.Date < DateTime.Now)
            {
                respuesta = false;
            }

            if (txtHora.Time < DateTime.Now.TimeOfDay)
            {
                respuesta = false;
            }

            if (string.IsNullOrEmpty(txtEstatus.SelectedItem.ToString()))
            {
                respuesta = false;
            }

            if (string.IsNullOrEmpty(txtCalificacion.Text))
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