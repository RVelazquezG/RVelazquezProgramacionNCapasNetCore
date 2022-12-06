﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Colonia
    {
        public static ML.Result GetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {
                    var usuarios = context.Colonia.FromSqlRaw($"[ColoniaGetByIdMunicipio] {IdMunicipio}");
                    result.Objects = new List<object>();
                    if (usuarios != null)
                    {
                        foreach (var objMunicipios in usuarios)
                        {

                            ML.Colonia colonia = new ML.Colonia();
                            colonia.IdColonia = objMunicipios.IdColonia;
                            colonia.Nombre = objMunicipios.Nombre;

                            colonia.Municipio = new ML.Municipio();
                            colonia.Municipio.IdMunicipio = IdMunicipio;

                            result.Objects.Add(colonia);

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
