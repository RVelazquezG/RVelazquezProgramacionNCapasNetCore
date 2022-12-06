using DL;
using Microsoft.EntityFrameworkCore;
using ML;
using System.Data;
using System.Data.OleDb;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetAll(ML.Usuario usuario*)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {
                    usuario.Rol.IdRol = (usuario.Rol.IdRol == null) ? 0 : usuario.Rol.IdRol; //operador ternario
                    var usuarios = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}', '{usuario.ApellidoPaterno}', {usuario.Rol.IdRol}").ToList();
                    
                    result.Objects = new List<object>();

                    if (usuarios != null)
                    {
                        foreach (var obj in usuarios)
                        {
                            ML.Usuario objUsuario = new ML.Usuario();
                            objUsuario.IdUsuario = obj.IdUsuario;
                            objUsuario.UserName = obj.UserName;
                            objUsuario.Nombre = obj.Nombre;
                            objUsuario.ApellidoPaterno = obj.ApellidoPaterno;
                            objUsuario.ApellidoMaterno = obj.ApellidoMaterno;
                            objUsuario.Email = obj.Email;
                            objUsuario.Password = obj.Password;
                            objUsuario.FechaNacimiento = obj.FechaNacimiento.ToString("dd-MM-yyyy");
                            objUsuario.Sexo = obj.Sexo;
                            objUsuario.Telefono = obj.Telefono;
                            objUsuario.Celular = obj.Celular;
                            objUsuario.CURP = obj.Curp;

                            objUsuario.Rol = new ML.Rol();
                            objUsuario.Rol.IdRol = int.Parse(obj.IdRol.ToString());
                            objUsuario.Rol.Nombre = obj.NombreRol;
                            objUsuario.Imagen = obj.Imagen;

                            objUsuario.Direccion = new ML.Direccion();
                            objUsuario.Direccion.IdDireccion = int.Parse(obj.IdDireccion.ToString());
                            objUsuario.Direccion.Calle = obj.Calle;
                            objUsuario.Direccion.NumeroInterior = obj.NumeroInterior;
                            objUsuario.Direccion.NumeroExterior = obj.NumeroExterior;

                            objUsuario.Direccion.Colonia = new ML.Colonia();
                            objUsuario.Direccion.Colonia.IdColonia = int.Parse(obj.IdColonia.ToString());
                            objUsuario.Direccion.Colonia.Nombre = obj.NombreColonia;



                            objUsuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            objUsuario.Direccion.Colonia.Municipio.IdMunicipio = int.Parse(obj.IdMunicipio.ToString());
                            objUsuario.Direccion.Colonia.Municipio.Nombre = obj.NombreMunicipio;

                            objUsuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            objUsuario.Direccion.Colonia.Municipio.Estado.IdEstado = int.Parse(obj.IdEstado.ToString());
                            objUsuario.Direccion.Colonia.Municipio.Estado.Nombre = obj.NombreEstado;

                            objUsuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            objUsuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = int.Parse(obj.IdPais.ToString());
                            objUsuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = obj.NombrePais;

                            objUsuario.Status = obj.Status.Value;

                            result.Objects.Add(objUsuario);
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

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {

                    //var query = context.UsuarioAdd(usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno,
                    //    usuario.Email, usuario.Password, usuario.FechaNacimiento, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.CURP, usuario.Rol.IdRol, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);

                    var query = context.Database.ExecuteSqlRaw(($"UsuarioAdd '{usuario.UserName}','{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.Password}', '{usuario.FechaNacimiento}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.CURP}', {usuario.Rol.IdRol}, '{usuario.Imagen}', '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}"));


                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se registro el usuario";
                    }

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static Result Update(ML.Usuario usuario)
        {
            Result result = new Result();
            try
            {

                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {

                    {
                        var updateResult = context.Database.ExecuteSqlRaw(($"UsuarioUpdate {usuario.IdUsuario}, '{usuario.UserName}','{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.Password}', '{usuario.FechaNacimiento}', '{usuario.Sexo}', '{usuario.Telefono}', '{usuario.Celular}', '{usuario.CURP}', {usuario.Rol.IdRol}, '{usuario.Imagen}', '{usuario.Direccion.Calle}', '{usuario.Direccion.NumeroInterior}', '{usuario.Direccion.NumeroExterior}', {usuario.Direccion.Colonia.IdColonia}"));


                        if (updateResult >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No se actualizó el registro del usuario";
                        }
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

        public static Result GetById(int IdUsuario)
        {
            Result result = new Result();
            try
            {

                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {

                    {
                        //var updateResult = context.UsuarioUpdate(usuario.IdUsuario, usuario.UserName, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno,
                        //usuario.Email, usuario.Password, usuario.FechaNacimiento, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.CURP, usuario.Rol.IdRol, usuario.Imagen,
                        //usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);

                        var objUsuario = context.Usuarios.FromSqlRaw(($"UsuarioGetById {IdUsuario}")).AsEnumerable().FirstOrDefault();

                        result.Objects = new List<object>();

                        if (objUsuario != null)
                        {

                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = objUsuario.IdUsuario;
                            usuario.UserName = objUsuario.UserName;
                            usuario.Nombre = objUsuario.Nombre;
                            usuario.ApellidoPaterno = objUsuario.ApellidoPaterno;
                            usuario.ApellidoMaterno = objUsuario.ApellidoMaterno;
                            usuario.Email = objUsuario.Email;
                            usuario.Password = objUsuario.Password;
                            usuario.FechaNacimiento = objUsuario.FechaNacimiento.ToString("dd-MM-yyyy");
                            usuario.Sexo = objUsuario.Sexo;
                            usuario.Telefono = objUsuario.Telefono;
                            usuario.Celular = objUsuario.Celular;
                            usuario.CURP = objUsuario.Curp;

                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = int.Parse(objUsuario.IdRol.ToString());
                            result.Object = usuario;

                            usuario.Imagen = objUsuario.Imagen;

                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = int.Parse(objUsuario.IdDireccion.ToString());
                            usuario.Direccion.Calle = objUsuario.Calle;
                            usuario.Direccion.NumeroInterior = objUsuario.NumeroInterior;
                            usuario.Direccion.NumeroExterior = objUsuario.NumeroExterior;

                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = int.Parse(objUsuario.IdColonia.ToString());
                            usuario.Direccion.Colonia.Nombre = objUsuario.NombreColonia;

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = int.Parse(objUsuario.IdMunicipio.ToString());
                            usuario.Direccion.Colonia.Municipio.Nombre = objUsuario.NombreMunicipio;

                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = int.Parse(objUsuario.IdEstado.ToString());
                            usuario.Direccion.Colonia.Municipio.Estado.Nombre = objUsuario.NombreEstado;

                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = int.Parse(objUsuario.IdPais.ToString());
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.Nombre = objUsuario.NombrePais;

                            usuario.Status = objUsuario.Status.Value;

                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Aseguradora";
                        }

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

        public static Result Delete(int IdUsuario)
        {
            Result result = new Result();

            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {

                    var query = context.Database.ExecuteSqlRaw(($"UsuarioDelete {IdUsuario}"));
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se eliminó el registro";
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public static Result ConvertirExcelDataTable(string connString)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    string query = "SELECT * FROM [Sheet1$]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter adapter = new OleDbDataAdapter();
                        adapter.SelectCommand = cmd;

                        DataTable tableUsuario = new DataTable();
                        adapter.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object> ();

                            foreach (DataRow row in tableUsuario.Rows)
                            {

                                ML.Usuario usuario = new ML.Usuario();

                                usuario.UserName = row[0].ToString();
                                usuario.Nombre = row[1].ToString();
                                usuario.ApellidoPaterno = row[2].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.Password = row[5].ToString();
                                usuario.FechaNacimiento = row[6].ToString();
                                usuario.Sexo = row[7].ToString();
                                usuario.Telefono = row[8].ToString();
                                usuario.Celular = row[9].ToString();
                                usuario.CURP = row[10].ToString();

                                usuario.Rol = new ML.Rol();
                                usuario.Rol.IdRol = int.Parse(row[11].ToString());

                                usuario.Imagen = null;

                                usuario.Direccion = new ML.Direccion();
                                usuario.Direccion.Calle = row[12].ToString();
                                usuario.Direccion.NumeroInterior = row[13].ToString();
                                usuario.Direccion.NumeroExterior = row[14].ToString();

                                usuario.Direccion.Colonia = new ML.Colonia();
                                usuario.Direccion.Colonia.IdColonia = int.Parse(row[15].ToString());


                                result.Objects.Add(usuario);
                            }

                            result.Correct = true;

                        }

                        if (tableUsuario.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "No existen registros en el excel";
                        }
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

        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();
                //DataTable  //Rows //Columns
                int i = 1;
                foreach (ML.Usuario usuario in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;


                    usuario.UserName = (usuario.UserName == "") ? error.Mensaje += "Ingresar el username  " : usuario.UserName; //operador ternario
                    usuario.Nombre = (usuario.Nombre == "") ? error.Mensaje += "Ingresar el nombre  " : usuario.Nombre; //operador ternario
                    usuario.ApellidoPaterno = (usuario.ApellidoPaterno == "") ? error.Mensaje += "Ingresar el apellido paterno  " : usuario.ApellidoPaterno; //operador ternario
                    usuario.ApellidoMaterno = (usuario.ApellidoMaterno == "") ? error.Mensaje += "Ingresar el apellido materno  " : usuario.ApellidoMaterno; //operador ternario
                    usuario.Email = (usuario.Email == "") ? error.Mensaje += "Ingresar el email  " : usuario.Email;
                    usuario.Password = (usuario.Password == "") ? error.Mensaje += "Ingresar el password  " : usuario.Password;
                    usuario.FechaNacimiento = (usuario.FechaNacimiento == "") ? error.Mensaje += "Ingresar la fecha de nacimiento  " : usuario.FechaNacimiento;
                    usuario.Sexo = (usuario.Sexo == "") ? error.Mensaje += "Ingresar el sexo  " : usuario.Sexo;
                    usuario.Telefono = (usuario.Telefono == "") ? error.Mensaje += "Ingresar el telefono  " : usuario.Telefono;
                    usuario.Celular = (usuario.Celular == "") ? error.Mensaje += "Ingresar el celular  " : usuario.Celular;
                    usuario.CURP = (usuario.CURP == "") ? error.Mensaje += "Ingresar el CURP " : usuario.CURP;

                    if (usuario.Rol.IdRol.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el semestre ";
                    }
                    if (usuario.Direccion.Calle.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el semestre ";
                    }
                    if (usuario.Direccion.NumeroInterior.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el semestre ";
                    }
                    if (usuario.Direccion.NumeroExterior.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el semestre ";
                    }
                    if (usuario.Direccion.Colonia.IdColonia.ToString() == "")
                    {
                        error.Mensaje += "Ingresar el semestre ";
                    }



                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }


                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        }

        public static ML.Result ChangeStatus(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.RvelazquezProgramacionNcapasContext context = new DL.RvelazquezProgramacionNcapasContext())
                {

                    var query = context.Database.ExecuteSqlRaw(($"UsuarioChangeStatus {usuario.IdUsuario}, {usuario.Status}"));


                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se registro el usuario";
                    }

                    result.Correct = true;

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