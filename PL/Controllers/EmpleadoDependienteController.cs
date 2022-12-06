using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoDependienteController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = new ML.Result();
            empleado.Empresa = new ML.Empresa();
            ML.Empresa empresa = new ML.Empresa();

            ML.Result resultEmpresa = BL.Empresa.GetAll(empresa);
            result = BL.Empleado.GetAll();


            if (result.Correct)
            {
                empleado.Empresa.Empresas = resultEmpresa.Objects;
                empleado.Empleados = result.Objects;
                return View(empleado);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
                return PartialView("Modal");
            }
        }

        [HttpGet]
        public ActionResult DependienteGetAll()
        {
            ML.Dependiente dependiente = new ML.Dependiente();
            ML.Result result = new ML.Result();
            dependiente.DependienteTipo = new ML.DependienteTipo();
            

            ML.Result resultEmpleado = BL.Empleado.GetAll();

            ML.Result resultTipoDependiente = BL.Dependiente.DependienteGetByNumeroEmpleado(dependiente.Empleado.NumeroEmpleado);
            dependiente.Empleado.Empleados = resultTipoDependiente.Objects;



            if (result.Correct)
            {
                //dependiente.Empleado.Empleados = resultEmpleado.Objects;
                //dependiente.Empleados = result.Objects;
                return View(dependiente);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
                return PartialView("Modal");
            }
        }

        //[HttpGet]
        //public ActionResult FormDependiente(string NumeroEmpleado)
        //{

        //    ML.Dependiente dependiente = new ML.Dependiente();
        //    ML.Result result = new ML.Result();
        //    dependiente.Empleado = new ML.Empleado();
        //    ML.Empleado empleado = new ML.Empleado();

        //    //ML.Result resultDependienteEmpleado = BL.EmpleadoDependiente.GetByIdNumeroEmpleado(Numero);
        //    result = BL.DependienteTipo.GetAll();
        //    result = BL.Empleado.GetAll();


        //    if (result.Correct)
        //    {
        //    //    dependiente.Empresa.Empresas = resultEmpresa.Objects;
        //    //    dependiente.Empleados = result.Objects;
        //        return View(dependiente);
        //    }
        //    else
        //    {
        //    ////    ViewBag.Message = "Ocurrio un error al realizar la consulta";
        //    //    return PartialView("Modal");
        //    }

        //    return View(dependiente);
        //}


        [HttpGet]
        public ActionResult Form(string NumeroEmpleado)
        {
            ML.Dependiente dependiente = new ML.Dependiente();
            dependiente.Empleado = new ML.Empleado();
            dependiente.DependienteTipo = new ML.DependienteTipo();

            ML.Result resultEmpleado = BL.Empleado.GetAll();
            ML.Result resultDependienteTipo = BL.DependienteTipo.GetAll();


            if (NumeroEmpleado == null)
            {
                dependiente.Empleado.Empleados = resultEmpleado.Objects;
                dependiente.DependienteTipo.DependienteTipos = resultDependienteTipo.Objects;
                return View(dependiente);
            }
            else
            {
                ML.Result result = BL.Dependiente.DependienteGetByNumeroEmpleado(NumeroEmpleado);

                if (result.Correct)
                {
                    dependiente = (ML.Dependiente)result.Object;
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar el dependiente seleccionado";
                    return PartialView("Modal");
                }

                return View(dependiente);
            }
        }

        public ActionResult Form(ML.Dependiente dependiente)
        {
            ML.Empleado empleado = new ML.Empleado();

            if (!ModelState.IsValid)
            {
                if (empleado.NumeroEmpleado == "")
                {
 
                    ML.Result result = BL.Dependiente.Add(dependiente);
                    if (result.Correct)
                    {
                        dependiente = (ML.Dependiente)result.Object;
                        ViewBag.Message = " El dependiente ha sido agregado con exito";
                        return PartialView("Modal");
                    }
                    else
                    {
                        //usuario.Rol = new ML.Rol();
                        //usuario.Direccion = new ML.Direccion();
                        //usuario.Direccion.Colonia = new ML.Colonia();
                        //usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        //usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        //usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

                        //ML.Result resultPais = BL.Pais.GetAll();
                        //ML.Result resultRol = BL.Rol.GetAll();

                        //usuario.Rol.Roles = resultRol.Objects;
                        //usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                        return View(dependiente);
                    }

                }
                else
                {

                    //ML.Result result = BL.Usuario.Update(usuario);
                    //if (result.Correct)
                    //{
                    //    usuario = (ML.Usuario)result.Object;
                    //    ViewBag.Message = "El usuario seleccionado ha sido actualizado con exito";
                    //    return PartialView("Modal");
                    //}
                    //else
                    //{
                    //    ViewBag.Message = "Ocurrio un error al actualizar el usuario seleccionado";
                    //    return PartialView("Modal");
                    //}
                }


            }
            return View(dependiente);
        }
    }
}
