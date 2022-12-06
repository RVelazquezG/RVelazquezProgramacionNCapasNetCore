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
    public class Aseguradora
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {
                    var aseguradoras = context.Aseguradoras.FromSqlRaw("[AseguradoraGetAll]").ToList();


                    result.Objects = new List<object>();

                    if (aseguradoras != null)
                    {
                        foreach (var obj in aseguradoras)
                        {
                            ML.Aseguradora aseguradora = new ML.Aseguradora();
                            aseguradora.IdAseguradora = obj.IdAseguradora;
                            aseguradora.Nombre = obj.Nombre;
                            aseguradora.FechaCreacion = obj.FechaCreacion.ToString();
                            aseguradora.FechaModificacion = obj.FechaModIficacion.ToString();

                            aseguradora.Usuario = new ML.Usuario();
                            aseguradora.Usuario.IdUsuario = int.Parse(obj.IdUsuario.ToString());
                            aseguradora.Usuario.Nombre = obj.NombreUsuario;
                            result.Objects.Add(aseguradora);
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

        public static ML.Result Add(ML.Aseguradora aseguradora)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {

                    var query = context.Database.ExecuteSqlRaw($"AseguradoraAdd '{aseguradora.Nombre}', {aseguradora.Usuario.IdUsuario}");


                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se registro el aseguradora";
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

        public static Result Update(ML.Aseguradora aseguradora)
        {
            Result result = new Result();
            try
            {

                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {
                    {
                        var updateResult = context.Database.ExecuteSqlRaw(($"AseguradoraUpdate '{aseguradora.Nombre}', {aseguradora.FechaModificacion}, {aseguradora.Usuario.IdUsuario},"));
                        if (updateResult >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se actualizó el registro del aseguradora";
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
        public static ML.Result GetById(int IdAseguradora)
        {
            Result result = new Result();
            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {
                    var objAseguradora = context.Aseguradoras.FromSqlRaw(($"AseguradoraGetById {IdAseguradora}")).AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();
                    if (objAseguradora != null)
                    {
                        ML.Aseguradora aseguradora = new ML.Aseguradora();
                        aseguradora.IdAseguradora = objAseguradora.IdAseguradora;
                        aseguradora.Nombre = objAseguradora.Nombre;
                        aseguradora.FechaCreacion = objAseguradora.FechaCreacion.ToString();
                        aseguradora.FechaModificacion = objAseguradora.FechaModIficacion.ToString();

                        aseguradora.Usuario = new ML.Usuario();
                        aseguradora.Usuario.IdUsuario = int.Parse(objAseguradora.IdUsuario.ToString());
                        aseguradora.Usuario.Nombre = objAseguradora.NombreUsuario;
                        result.Object = aseguradora;

                        result.Correct = true;

                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Aseguradora";
                    }
                }
            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static Result Delete(int IdAseguradora)
        {
            Result result = new Result();

            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {

                    var query = context.Database.ExecuteSqlRaw(($"AseguradoraDelete {IdAseguradora}"));
                    if (query > 1)
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
