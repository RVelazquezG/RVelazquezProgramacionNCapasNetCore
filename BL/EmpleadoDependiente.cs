using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class EmpleadoDependiente
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

    }
}
