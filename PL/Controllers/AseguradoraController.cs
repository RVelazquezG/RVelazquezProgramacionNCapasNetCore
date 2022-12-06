using Microsoft.AspNetCore.Mvc;
using NuGet.DependencyResolver;

namespace PL.Controllers
{
    public class AseguradoraController : Controller
    {

        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            ML.Result result = new ML.Result();
    
            result = BL.Aseguradora.GetAll();


            if (result.Correct)
            {
                aseguradora.Aseguradoras = result.Objects;
                return View(aseguradora);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
                return View();
            }
        }


        [HttpGet]
        public ActionResult Form(int? IdAseguradora)
         {

            ML.Aseguradora aseguradora = new ML.Aseguradora();
            ML.Usuario usuario = new ML.Usuario();

            //ML.Result resultRol = BL.Rol.GetAll();
            //ML.Result resultUsuario = BL.Usuario.GetAll(usuario);



            if (IdAseguradora == null)
            {
                //aseguradora.Usuario.Usuarios = resultUsuario.Objects;
                return View(aseguradora);
            }
            else
            {
                ML.Result result = BL.Aseguradora.GetById(IdAseguradora.Value);
                if (result.Correct)
                {

                    aseguradora = (ML.Aseguradora)result.Object;
                    //aseguradora.Usuario.Usuarios = resultUsuario.Objects;

                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar el usuario seleccionado";
                }

                return View(aseguradora);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Aseguradora aseguradora)
        {
            if (aseguradora.IdAseguradora == 0)
            {
                //add
                //ml.result = agregar
                ML.Result result = BL.Aseguradora.Add(aseguradora);
                if (result.Correct)
                {
                    aseguradora = (ML.Aseguradora)result.Object;
                    ViewBag.Message = " El usuario ha sido agregado con exito";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al agregar el usuario seleccionado";
                    return PartialView("Modal");
                }

            }
            else
            {

                ML.Result result = BL.Aseguradora.Update(aseguradora);
                if (result.Correct)
                {
                    aseguradora = (ML.Aseguradora)result.Object;
                    ViewBag.Message = "El usuario seleccionado ha sido actualizado con exito";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al actualizar el usuario seleccionado";
                    return PartialView("Modal");
                }
            }

        }


        [HttpGet]
        public ActionResult Delete(int? IdAseguradora)
        {

            ML.Result result = new ML.Result();


            if (IdAseguradora == null)
            {
                ViewBag.Message = "Ocurrio un error al eliminar el usuario seleccionado";
                return PartialView("Modal");
            }
            else
            {
                result = BL.Aseguradora.Delete(IdAseguradora.Value);
                ViewBag.Message = "El usuario seleccionado ha sido eliminado";
                return PartialView("Modal");
            }
        }

    }
}
