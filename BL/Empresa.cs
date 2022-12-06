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
    public class Empresa
    {
        public static ML.Result GetAll(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {
                    var empresas = context.Empresas.FromSqlRaw($"EmpresaGetAll '{empresa.Nombre}'").ToList();

                    result.Objects = new List<object>();

                    if (empresas != null)
                    {
                        foreach (var obj in empresas)
                        {
                            ML.Empresa objEmpresa = new ML.Empresa();
                            objEmpresa.IdEmpresa = obj.IdEmpresa;
                            objEmpresa.Nombre = obj.Nombre;
                            objEmpresa.Telefono = obj.Telefono;
                            objEmpresa.Email = obj.Email;
                            objEmpresa.DireccionWeb = obj.DireccionWeb;
                            objEmpresa.Logo = obj.Logo;

                            result.Objects.Add(objEmpresa);
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
    

    public static ML.Result Add(ML.Empresa empresa)
    {
        ML.Result result = new ML.Result();
        try
        {
            using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
            {

                //var query = context.UsuarioAdd(usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno,
                //    usuario.Email, usuario.Password, usuario.FechaNacimiento, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.CURP, usuario.Rol.IdRol, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);

                var query = context.Database.ExecuteSqlRaw(($"EmpresaAdd '{empresa.Nombre}','{empresa.Telefono}', '{empresa.Email}', '{empresa.DireccionWeb}', '{empresa.Logo}'"));


                if (query >= 1)
                {
                    result.Correct = true;
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "No se registro la empresa";
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

    public static Result Update(ML.Empresa empresa)
    {
        Result result = new Result();
        try
        {

            using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
            {

                {

                    var updateResult = context.Database.ExecuteSqlRaw(($"EmpresaUpdate '{empresa.Nombre}','{empresa.Telefono}', '{empresa.Email}', '{empresa.DireccionWeb}', '{empresa.Logo}'"));
                        if (updateResult >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se actualizó el registro de la empresa";
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
        public static Result GetById(int IdEmpresa)
        {
            Result result = new Result();
            try
            {

            using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
            {

                {
                    //var updateResult = context.UsuarioUpdate(usuario.IdUsuario, usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno,
                    //usuario.Email, usuario.Password, usuario.FechaNacimiento, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.CURP, usuario.Rol.IdRol, usuario.Imagen,
                    //usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);

                    var objEmpresa = context.Empresas.FromSqlRaw(($"EmpresaGetById {IdEmpresa}")).AsEnumerable().FirstOrDefault();

                    result.Objects = new List<object>();

                    if (objEmpresa != null)
                    {

                        ML.Empresa empresa = new ML.Empresa();
                        empresa.IdEmpresa = objEmpresa.IdEmpresa;
                        empresa.Nombre = objEmpresa.Nombre;
                        empresa.Telefono = objEmpresa.Telefono;
                        empresa.Email = objEmpresa.Email;
                        empresa.DireccionWeb = objEmpresa.DireccionWeb;
                        empresa.Logo = objEmpresa.Logo;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Empresa";
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

    public static Result Delete(int IdEmpresa)
    {
        Result result = new Result();

        try
        {
            using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
            {

                var query = context.Database.ExecuteSqlRaw(($"EmpresaDelete {IdEmpresa}"));
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
