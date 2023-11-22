using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using AppRegistrosC.Models;
using System.Threading.Tasks;


namespace AppRegistrosC.Data
{
    public class SQLiteHelper
    {
        SQLiteAsyncConnection db;

        public SQLiteHelper(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Curssos>().Wait();
            db.CreateTableAsync<Empleados>().Wait();
            db.CreateTableAsync<Users>().Wait();
            db.CreateTableAsync<SeguimientoEmpleados>().Wait();
        }

        public Task<int> SaveCursoAsync(Curssos curs)
        {
            if (curs.IDCur != 0)
            {
                return db.UpdateAsync(curs);
            }
            else
            {
                return db.InsertAsync(curs);
            }
        }
        public Task<List<Curssos>> GetCursosAsync()
        {
            return db.Table<Curssos>().ToListAsync();
        }
        public Task<Curssos> GetCursoByIdAsync(int IdCur)
        {
            return db.Table<Curssos>().Where(a => a.IDCur == IdCur).FirstOrDefaultAsync();
        }
        public Task<int> DeleteCursosAsync(Curssos curso)
        {
            return db.DeleteAsync(curso);
        }

        

        public Task<int> SaveEmpleadoAsync(Empleados emple)
        {
            if (emple.IDEmp != 0)
            {
                return db.UpdateAsync(emple);
            }
            else
            {
                return db.InsertAsync(emple);
            }
        }
        public Task<List<Empleados>> GetEmpleadosAsync()
        {
            return db.Table<Empleados>().ToListAsync();
        }
        public Task<Empleados> GetEmpleadoByIdAsync(int IdEmp)
        {
            return db.Table<Empleados>().Where(a => a.IDEmp == IdEmp).FirstOrDefaultAsync();
        }
        public Task<int> DeleteEmpleadosAsync(Empleados empleado)
        {
            return db.DeleteAsync(empleado);
        }





        public Task<int> SaveUserModelAsync(Users usr)
        {
            if (usr.UserId != 0)
            {
                return db.UpdateAsync(usr);
            }
            else
            {
                return db.InsertAsync(usr);
            }
        }

        public Task<List<Users>> GetUsersValidate(string email, string password)
        {
            return db.QueryAsync<Users>("SELECT * FROM Users WHERE EmailUser='" + email + "'AND EmailPassword='" + password + "'");
        }


        public Task<int> SaveSeguimientoEmpleadoAsync(SeguimientoEmpleados segui)
        {
            if (segui.IDSeg != 0)
            {
                return db.UpdateAsync(segui);
            }
            else
            {
                return db.InsertAsync(segui);
            }
        }
        public Task<List<SeguimientoEmpleados>> GetSeguimientoEmpleadosAsync()
        {
            return db.Table<SeguimientoEmpleados>().ToListAsync();
        }
        public Task<SeguimientoEmpleados> GetSeguimientoEmpleadoByIdAsync(int IdSeg)
        {
            return db.Table<SeguimientoEmpleados>().Where(a => a.IDSeg == IdSeg).FirstOrDefaultAsync();
        }
        public Task<int> DeleteSeguimientoEmpleadosAsync(SeguimientoEmpleados seguimientoo)
        {
            return db.DeleteAsync(seguimientoo);
        }


    }
}

