using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class DependienteTipo
    {
        public static ML.Result GetAll()
        {

            ML.Result result = new ML.Result();

            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {
                    var tipos = context.DependienteTipos.FromSqlRaw("[DependienteTipoGetAll]").ToList();

                    result.Objects = new List<object>();

                    if (tipos != null)
                    {
                        foreach (var objTipos in tipos)
                        {
                            ML.DependienteTipo tipo = new ML.DependienteTipo();
                            tipo.IdDependienteTipo = int.Parse(objTipos.IdDependienteTipo.ToString());
                            tipo.Nombre = objTipos.Nombre;

                            result.Objects.Add(tipo);
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
