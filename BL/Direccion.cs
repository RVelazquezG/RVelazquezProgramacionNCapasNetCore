using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Direccion
    {
        public static ML.Result GetByIdPais(int IdPais)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {
                    var usuarios = context.Pais.FromSqlRaw($"[EstadoGetByIdPais] {IdPais}").ToList();
                    result.Objects = new List<object>();
                    if (usuarios != null)
                    {
                        foreach (var objSemestre in usuarios)
                        {

                            ML.Estado estado = new ML.Estado();
                            estado.IdEstado = objSemestre.IdPais;
                            estado.Nombre = objSemestre.Nombre;

                            estado.Pais = new ML.Pais();
                            estado.Pais.IdPais = IdPais;

                            result.Objects.Add(estado);

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
