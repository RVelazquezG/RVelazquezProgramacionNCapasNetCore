using DL;
using Microsoft.EntityFrameworkCore;
using ML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empleado
    {

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {
                    var empleados = context.Empleados.FromSqlRaw($"EmpleadoGetAll");

                    result.Objects = new List<object>();

                    if (empleados != null)
                    {
                        foreach (var objEmp in empleados)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.NumeroEmpleado = objEmp.NumeroEmpleado;
                            empleado.RFC = objEmp.Rfc;
                            empleado.Nombre = objEmp.Nombre;
                            empleado.ApellidoPaterno = objEmp.ApellidoPaterno;
                            empleado.ApellidoMaterno = objEmp.ApellidoMaterno;
                            empleado.Email = objEmp.Email;
                            empleado.Telefono = objEmp.Telefono;
                            empleado.FechaNacimiento = objEmp.FechaNacimiento.ToString("dd-MM-yyyy");
                            empleado.NSS = objEmp.Nss;
                            empleado.FechaIngreso = objEmp.FechaIngreso.ToString("dd-MM-yyyy");
                            empleado.Foto = objEmp.Foto;
                           
                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = int.Parse(objEmp.IdEmpresa.ToString());
                            empleado.Empresa.Nombre = objEmp.NombreEmpresa;

                            result.Objects.Add(empleado);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw(($"EmpleadoAdd '{empleado.NumeroEmpleado}',{empleado.RFC},'{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', '{empleado.Email}', '{empleado.Telefono}', '{empleado.FechaNacimiento}', '{empleado.NSS}', '{empleado.FechaIngreso}', '{empleado.Foto}', {empleado.Empresa.IdEmpresa}"));


                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se registro el empleado";
                    }

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static Result Update(ML.Empleado empleado)
        {
            Result result = new Result();
            try
            {

                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {

                    {
                        var updateResult = context.Database.ExecuteSqlRaw(($"EmpleadoUpdate '{empleado.RFC}','{empleado.Nombre}', '{empleado.ApellidoPaterno}', '{empleado.ApellidoMaterno}', '{empleado.Email}', '{empleado.Telefono}', '{empleado.FechaNacimiento}', '{empleado.NSS}', '{empleado.FechaIngreso}', '{empleado.Foto}', {empleado.Empresa.IdEmpresa}"));


                        if (updateResult >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se actualizó el registro del empleado";
                        }
                    }
                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static Result GetById(string NumeroEmpleado)
        {
            Result result = new Result();
            try
            {

                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {

                    {
                        var objEmpleado = context.Empleados.FromSqlRaw(($"EmpleadoGetById '{NumeroEmpleado}'")).AsEnumerable().FirstOrDefault();

                        result.Objects = new List<object>();

                        if (objEmpleado != null)
                        {

                            ML.Empleado empleado = new ML.Empleado();
                            empleado.NumeroEmpleado = objEmpleado.NumeroEmpleado;
                            empleado.RFC = objEmpleado.Rfc;
                            empleado.Nombre = objEmpleado.Nombre;
                            empleado.ApellidoPaterno = objEmpleado.ApellidoPaterno;
                            empleado.ApellidoMaterno = objEmpleado.ApellidoMaterno;
                            empleado.Email = objEmpleado.Email;
                            empleado.Telefono = objEmpleado.Telefono;
                            empleado.FechaNacimiento = objEmpleado.FechaNacimiento.ToString();
                            empleado.NSS = objEmpleado.Nss;
                            empleado.FechaIngreso = objEmpleado.FechaIngreso.ToString();
                            empleado.Foto = objEmpleado.Foto;


                            empleado.Empresa = new ML.Empresa();
                            empleado.Empresa.IdEmpresa = int.Parse(objEmpleado.IdEmpresa.ToString());
                            empleado.Empresa.Nombre = objEmpleado.NombreEmpresa;
                            result.Object = empleado;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Empleado";
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static Result Delete(string NumeroEmpleado)
        {
            Result result = new Result();

            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {

                    var query = context.Database.ExecuteSqlRaw(($"EmpleadoDelete '{NumeroEmpleado}'"));
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se eliminó el registro";
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

    }
}
