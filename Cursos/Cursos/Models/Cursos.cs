using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace AppRegistrosC.Models
{
    
        public class Curssos
        {
            [PrimaryKey, AutoIncrement]

            public int IDCur { get; set; }

            [MaxLength(50)]

            public string Nombre { get; set; }

            [MaxLength(50)]

            public string Tipo { get; set; }

            [MaxLength(50)]

            public string Descripcion { get; set; }

            [MaxLength(50)]

            public int Horas { get; set; }

        }




    public class Empleados
    {
        [PrimaryKey, AutoIncrement]

        public int IDEmp { get; set; }

        [MaxLength(50)]

        public byte[] Imagen { get; set; }

        public string Nombre { get; set; }

        [MaxLength(50)]

        public string Direccion { get; set; }

        [MaxLength(50)]

        public string CURP { get; set; }

        [MaxLength(50)]

        public string Tipo { get; set; }

        [MaxLength(50)]

        public int Telefono { get; set; }

        public int Edad { get; set; }

    }

    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int UserId { get; set; }

        [MaxLength(30)]
        public string EmailUser { get; set; }

        [MaxLength(16)]
        public string EmailPassword { get; set; }

        [MaxLength(30)]
        public string NombreCompleto { get; set; }

        public int Edad { get; set; }

        public DateTime FechaCreacion { get; set; }
    }

    public class SeguimientoEmpleados
    {
        [PrimaryKey, AutoIncrement]

        public int IDSeg { get; set; }

        [MaxLength(100)]

        public string NombreSeg { get; set; }

        [MaxLength(50)]

        public string Curso { get; set; }

        [MaxLength(50)]
        public string Lugar { get; set; }

        [MaxLength(50)]

        public string Fecha { get; set; }

        [MaxLength(50)]

        public string Hora { get; set; }

        [MaxLength(50)]

        public string Estatus { get; set; }

        [MaxLength(50)]

        public int Calificacion { get; set; }

        public SeguimientoEmpleados() { }
    }



}


