using Microsoft.AspNetCore.Mvc;
using ML;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        // GET: api/<UsuarioController>
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();
            ML.Result result = BL.Usuario.GetAll(usuario);


            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpPost("GetAll/{IdUsuario}")]
        public IActionResult GetAll(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = new ML.Result();
            usuario.Rol = new ML.Rol();

            ML.Result resultRol = BL.Rol.GetAll();
            result = BL.Usuario.GetAll(usuario);

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                usuario.Rol.Roles = resultRol.Objects;
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        // GET api/<UsuarioController>/5
        [HttpGet("GetById")]
        public IActionResult GetById(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();

            usuario.Rol = new ML.Rol();
            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            ML.Result resultPais = BL.Pais.GetAll();
            ML.Result resultRol = BL.Rol.GetAll();

            if (IdUsuario == null)
            {
                return BadRequest();
            }
            else
            {
                ML.Result result = BL.Usuario.GetById(IdUsuario);
                if (result.Correct)
                {
            

                }

                return Ok(result);
            }


        }
        // POST api/<UsuarioController>
        [HttpPost]
        [Route("Usuario/Add")]
        public IActionResult Add([FromBody] ML.Usuario usuario)
        {
            var result = BL.Usuario.Add(usuario);

            if (result.Correct)
            {

                return Ok(result);

            }
            else
            {
                return BadRequest();
            }


        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("Delete/{IdUsuario}")]
        public ActionResult Delete(int? IdUsuario)
        {
            ML.Result result = new ML.Result();

            if (IdUsuario == null)
            {
                return BadRequest();
            }
            else
            {
                result = BL.Usuario.Delete(IdUsuario.Value);
                return Ok();
            }
        }
    }
}
