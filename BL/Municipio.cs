using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Municipio
    {
        public static ML.Result GetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {
                    var usuarios = context.Municipios.FromSqlRaw($"[MunicipioGetByIdEstado] {IdEstado}");
                    result.Objects = new List<object>();

                    if (usuarios != null)
                    {
                        foreach (var objMunicipios in usuarios)
                        {

                            ML.Municipio municipio = new ML.Municipio();
                            municipio.IdMunicipio = objMunicipios.IdMunicipio;
                            municipio.Nombre = objMunicipios.Nombre;

                            municipio.Estado = new ML.Estado();
                            municipio.Estado.IdEstado = IdEstado;

                            result.Objects.Add(municipio);

                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se ha podido realizar la consulta";

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
