using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpleadoController : Controller
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
        public ActionResult Form(string NumeroEmpleado)
        {

            ML.Empleado empleado = new ML.Empleado();
            empleado.Empresa = new ML.Empresa();
            ML.Empresa empresa = new ML.Empresa();

            ML.Result resultEmpresa = BL.Empresa.GetAll(empresa);


            if (NumeroEmpleado == null)
            {
                empleado.Empresa.Empresas = resultEmpresa.Objects;
                return View(empleado);
            }
            else
            {
                ML.Result result = BL.Empleado.GetById(NumeroEmpleado);
                if (result.Correct)
                {

                    empleado = (ML.Empleado)result.Object;
                    empleado.Empresa.Empresas = resultEmpresa.Objects;

                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar el empleado seleccionado";
                    return PartialView("Modal");
                }

                return View(empleado);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Empleado empleado)
        {


            IFormFile image = Request.Form.Files["IFImage"];


            //valido si traigo imagen
            if (image != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(image);
                //convierto a base 64 la imagen y la guardo en la propiedad de imagen en el objeto alumno
                empleado.Foto = Convert.ToBase64String(ImagenBytes);
            }


            if (empleado.NumeroEmpleado == null)
            {
                //add
                //ml.result = agregar
                ML.Result result = BL.Empleado.Add(empleado);
                if (result.Correct)
                {
                    empleado = (ML.Empleado)result.Object;
                    ViewBag.Message = " El empleado ha sido agregado con exito";
                    return PartialView("Modal");
                }
                else
                {
                    empleado.Empresa = new ML.Empresa();
                    ML.Empresa empresa = new ML.Empresa();
                    ML.Result resultEmpresa = BL.Empresa.GetAll(empresa);

                    empleado.Empresa.Empresas = resultEmpresa.Objects;
                    return View(empleado);
                }


            }
            else
            {
                ML.Result result = BL.Empleado.Update(empleado);
                if (result.Correct)
                {
                    empleado = (ML.Empleado)result.Object;
                    ViewBag.Message = "El empleado seleccionado ha sido empleado con exito";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al actualizar el empleado seleccionado";
                    return PartialView("Modal");
                }
            }

        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {

            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }
    }
}
