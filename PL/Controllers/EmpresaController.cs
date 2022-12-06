using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class EmpresaController : Controller
    {

        [HttpGet]
        public ActionResult GetAll()
        {

            ML.Empresa empresa = new ML.Empresa();
            ML.Result result = new ML.Result();

            result = BL.Empresa.GetAll(empresa);


            if (result.Correct)
            {

                empresa.Empresas = result.Objects;
                return View(empresa);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
                return View();
            }
        }

        [HttpPost]
        public ActionResult GetAll(ML.Empresa empresa)
        {

            ML.Result result = new ML.Result();
            result = BL.Empresa.GetAll(empresa);


            if (result.Correct)
            {
                empresa.Empresas = result.Objects;
                return View(empresa);
            }
            else
            {
                ViewBag.Message = "Ocurrio un error al realizar la consulta";
                return View();
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdEmpresa)
        {
           
            ML.Empresa empresa = new ML.Empresa();
  

            if (IdEmpresa == null)
            {
             
                return View(empresa);
            }
            else
            {
                ML.Result result = BL.Empresa.GetById(IdEmpresa.Value);

                if (result.Correct)
                {
                    empresa = (ML.Empresa)result.Object;
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al consultar el usuario seleccionado";
                    return PartialView("Modal");
                }

                return View(empresa);
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Empresa empresa)
        {
            IFormFile logo = Request.Form.Files["IFImage"];


            //valido si traigo imagen
            if (logo != null)
            {
                //llamar al metodo que convierte a bytes la imagen
                byte[] ImagenBytes = ConvertToBytes(logo);
                //convierto a base 64 la imagen y la guardo en la propiedad de imagen en el objeto alumno
                empresa.Logo = Convert.ToBase64String(ImagenBytes);
            }

            if (empresa.IdEmpresa == 0)
            {
                //add
                //ml.result = agregar
                ML.Result result = BL.Empresa.Add(empresa);
                if (result.Correct)
                {
                    empresa = (ML.Empresa)result.Object;
                    ViewBag.Message = "La aseguradora ha sido agregada con exito";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al agregar la aseguradora";
                    return PartialView("Modal");
                }

            }
            else
            {

                ML.Result result = BL.Empresa.Update(empresa);
                if (result.Correct)
                {
                    empresa = (ML.Empresa)result.Object;
                    ViewBag.Message = "La aseguradora seleccionada ha sido actualizada con exito";
                    return PartialView("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al actualizar la aseguradora seleccionada";
                    return PartialView("Modal");
                }
            }

        }


        [HttpGet]
        public ActionResult Delete(int? IdEmpresa)
        {

            ML.Result result = new ML.Result();


            if (IdEmpresa == null)
            {
                ViewBag.Message = "Ocurrio un error al eliminar el usuario seleccionado";
            }
            else
            {
                result = BL.Empresa.Delete(IdEmpresa.Value);
                ViewBag.Message = "El usuario seleccionado ha sido eliminado";
            }
            return PartialView("Modal");
        }

        public static byte[] ConvertToBytes(IFormFile Logo)
        {

            using var fileStream = Logo.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);

            return bytes;
        }

    }
}
