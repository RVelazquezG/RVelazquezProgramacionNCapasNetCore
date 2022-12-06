using Microsoft.AspNetCore.Mvc;
using ML;
using System.Data;
using System.IO;
using System.Data.OleDb;
using System.Data;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        private readonly IConfiguration _configuration;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public CargaMasivaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public ActionResult CargaMasiva()
        {
            ML.Result result = new ML.Result();

            return View(result);
        }

        [HttpPost]
        public ActionResult CargaTXT()
        {
            IFormFile fileTXT = Request.Form.Files["fileTXT"];


            if (fileTXT != null)
            {

                StreamReader Textfile = new StreamReader(fileTXT.OpenReadStream());

                string line;
                line = Textfile.ReadLine();
                while ((line = Textfile.ReadLine()) != null)
                {
                    string[] lines = line.Split('|');

                    ML.Usuario usuario = new ML.Usuario();

                    usuario.UserName = lines[0];
                    usuario.Nombre = lines[1];
                    usuario.ApellidoPaterno = lines[2];
                    usuario.ApellidoMaterno = lines[3];
                    usuario.Email = lines[4];
                    usuario.Password = lines[5];
                    usuario.FechaNacimiento = lines[6];
                    usuario.Sexo = lines[7];
                    usuario.Telefono = lines[8];
                    usuario.Celular = lines[9];
                    usuario.CURP = lines[10];

                    usuario.Rol = new ML.Rol();
                    usuario.Rol.IdRol = int.Parse(lines[11]);

                    usuario.Imagen = null;

                    usuario.Direccion = new ML.Direccion();
                    usuario.Direccion.Calle = lines[12];
                    usuario.Direccion.NumeroInterior = lines[13];
                    usuario.Direccion.NumeroExterior = lines[14];

                    usuario.Direccion.Colonia = new ML.Colonia();
                    usuario.Direccion.Colonia.IdColonia = int.Parse(lines[15]);

                    ML.Result result = BL.Usuario.Add(usuario);

                    if (result.Correct)
                    {
                        Console.WriteLine("Correcto");
                        Console.ReadKey();
                    }
                    else
                    {
                        string filePath = @"C:\Users\digis\Documents\RobertoVelazquezGonzalez\BlocDeNotas\ErroresLayout.txt";
                        using (StreamWriter writer = new StreamWriter(filePath, true))
                        {
                            writer.WriteLine("-----------------------------------------------------------------------------");
                            writer.WriteLine("Date : " + DateTime.Now.ToString());
                            writer.WriteLine(result.ErrorMessage);
                        }
                    }

                }
            }

            return PartialView("Modal");
        }

        [HttpPost]
        public ActionResult UsuarioCargaMasiva(ML.Usuario usuario)
        {

            IFormFile excelCargaMasiva = Request.Form.Files["FileExcel"];
            //Session 

            if (HttpContext.Session.GetString("PathArchivo") == null) //sesion nula o que no exista 
            {
                //validar el excel

                if (excelCargaMasiva.Length > 0)
                {
                    //que sea .xlsx
                    string fileName = Path.GetFileName(excelCargaMasiva.FileName);
                    string folderPath = _configuration["PathFolder:value"];
                    string extensionArchivo = Path.GetExtension(excelCargaMasiva.FileName).ToLower();
                    string extensionModulo = _configuration["TipoExcel"];

                    if (extensionArchivo == extensionModulo)
                    {
                        //crear copia
                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                excelCargaMasiva.CopyTo(stream);
                            }
                            //leer
                            string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;

                            ML.Result resultConvertExcel = BL.Usuario.ConvertirExcelDataTable(connectionString);

                            if (resultConvertExcel.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultConvertExcel.Objects);
                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);
                                }

                                return View("CargaMasiva", resultValidacion);
                            }
                            else
                            {
                                //error al leer el archivo
                                ViewBag.Message = "Ocurrio un error al leer el arhivo";
                                return View("Modal");
                            }
                        }
                    }

                }
                //verificar que no tenga errores 
                //le avisamos al usuario que el excel esta mal ML.ErrorExcel 
                //crea la sesion 
            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.ConvertirExcelDataTable(connectionString);
                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Usuario usuarioItem in resultData.Objects)
                    {

                        ML.Result resultAdd = BL.Usuario.Add(usuarioItem);
                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se insertó el Usuario con nombre: " + usuarioItem.Nombre + " Error: " + resultAdd.ErrorMessage);
                        }
                    }
                    if (resultErrores.Objects.Count > 0)
                    {

                        string fileError = Path.Combine(_hostingEnvironment.WebRootPath, @"C:\Users\digis\Documents\RobertoVelazquezGonzalez\BlocDeNotas\logErrores.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Los Usuarios No han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Los Usuarios han sido registrados correctamente";

                    }

                }

            }
            return PartialView("Modal");

        }

    }
}
